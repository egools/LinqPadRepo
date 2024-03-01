<Query Kind="Program">
  <NuGetReference>Flurl</NuGetReference>
  <NuGetReference>Flurl.Http</NuGetReference>
  <NuGetReference>HtmlAgilityPack</NuGetReference>
  <NuGetReference>MathNet.Numerics</NuGetReference>
  <Namespace>Flurl</Namespace>
  <Namespace>HtmlAgilityPack</Namespace>
  <Namespace>MathNet.Numerics.Distributions</Namespace>
  <Namespace>System.Web</Namespace>
  <Namespace>Flurl.Http</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

async Task Main()
{
	var request = new SurveyTakerRespondentRequest
	{
		SurveyID = 12410,
		SurveyUrl = "https://surveybeta.square-panel.com/yfkg?s=f52e&IgnoreIP=1",
		PID = Guid.NewGuid().ToString()
	};
	var surveyBaseUrl = new Url(request.SurveyUrl).RemoveQuery();

	using (var httpClient = new FlurlClient(surveyBaseUrl)
				.WithAutoRedirect(false)
				.WithTimeout(300)
				.WithHeader("Pragma", "no-cache")
				.WithHeader("Content-type", "application/x-www-form-urlencoded")
				.WithHeader("User-agent", "Gongos Survey Checker"))
	{
		var result = await RunSurveyTakerRespondentAsync(request, httpClient, new CancellationToken());
	}
}

public const int maxRedundantFormActions = 10;
public const int maxPages = 500;

public async Task<SurveyTakerRespondentResult> RunSurveyTakerRespondentAsync(SurveyTakerRespondentRequest request, FlurlClient httpClient, CancellationToken cancellationToken)
{
	#region Validations

	if (string.IsNullOrEmpty(request.SurveyUrl))
		return new SurveyTakerRespondentResult();

	#endregion

	var myFormActions = new List<string>();
	var uniqueFormActions = new List<string>();
	var logs = new List<string>();
	var stopwatch = new Stopwatch();

	bool foundEnd = false, successful = true, hasCompleted = false;
	string responseBody = string.Empty, formAction = string.Empty, previousFormAction = string.Empty;

	try
	{
		logs.Add("Starting");
		stopwatch.Start();

		var redundantFormActionCount = 0;
		var totalPageCount = 0;
		var cookieJar = new CookieJar();

		var httpRequest = httpClient
			.Request()
			.SetQueryParam("pid", request.PID.Trim())
			.SetQueryParam("st", 1)
			.SetQueryParam("s", new Url(request.SurveyUrl).QueryParams.TryGetFirst("s", out object sampleSource) ? sampleSource : null, NullValueHandling.Remove)
			.WithCookies(cookieJar);

		cancellationToken.ThrowIfCancellationRequested();

		try
		{
			var response = await httpRequest.GetAsync();

			while (foundEnd == false)
			{
				while ((HttpStatusCode)response.StatusCode == HttpStatusCode.Found)
				{
					if (!response.Headers.TryGetFirst("Location", out string locationHeader))
						throw new InvalidOperationException();

					Url location = locationHeader;

					// let's remove the survey's key
					location.PathSegments.RemoveAt(0);

					response = await httpClient
						.Request()
						.AppendPathSegments(location.PathSegments)
						.SetQueryParams(location.QueryParams)
						.WithCookies(cookieJar)
						.GetAsync();
				}

				responseBody = await response.GetStringAsync();

				if (responseBody.IndexOf("thankyou.aspx", StringComparison.OrdinalIgnoreCase) > -1 || responseBody.IndexOf("thankyou/pagepost", StringComparison.OrdinalIgnoreCase) > -1)
				{
					logs.Add("Found end page");

					foundEnd = true;
				}

				if (responseBody.IndexOf("studyclosed.aspx", StringComparison.OrdinalIgnoreCase) > -1)
				{
					logs.Add("Found closed page");

					foundEnd = true;
					successful = false;
				}

				if (responseBody.IndexOf("error.aspx", StringComparison.OrdinalIgnoreCase) > -1 || responseBody.IndexOf("an error occurred", StringComparison.OrdinalIgnoreCase) > -1)
				{
					logs.Add($"Found error page with HTML body: {responseBody.Replace("\t", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty)}");

					foundEnd = true;
					successful = false;
				}

				if (!foundEnd && !string.IsNullOrEmpty(responseBody))
				{
					var htmlDoc = new HtmlDocument();
					htmlDoc.LoadHtml(responseBody);
					var formNode = htmlDoc.DocumentNode.SelectSingleNode("//form");
					Url location = HttpUtility.HtmlDecode(formNode.Attributes["action"].Value);

					// let's remove the survey's key
					location.PathSegments.RemoveAt(0);

					formAction = location;

					if (!uniqueFormActions.Contains(formAction))
					{
						uniqueFormActions.Add(formAction);
					}

					if (!myFormActions.Contains(formAction))
					{
						myFormActions.Add(formAction);
					}

					var formData = GetFormData(htmlDoc);

					response = await httpClient
							.Request()
							.AppendPathSegments(location.PathSegments)
							.SetQueryParams(location.QueryParams)
							.WithCookies(cookieJar)
							.PostStringAsync(formData);

					logs.Add($"Page: {formAction} with post: {formData}");
				}

				if (formAction == previousFormAction) { redundantFormActionCount += 1; totalPageCount--; }
				else { redundantFormActionCount = 0; previousFormAction = formAction; }

				if (redundantFormActionCount >= maxRedundantFormActions)
				{
					logs.Add($"Redundant Form Action: {formAction}");

					foundEnd = true;
					successful = false;
				}

				totalPageCount++;
				if (totalPageCount >= maxPages)
				{
					logs.Add($"More than {maxPages} pages!");

					foundEnd = true;
				}
			}
		}
		catch (FlurlHttpException ex)
		{
			responseBody = await ex.GetResponseStringAsync() ?? ex.Message;

			logs.Add($"Web Response Error: {ex.Message} with HTML body: {responseBody.Replace("\t", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty)}");

			foundEnd = true;
			successful = false;
		}
		finally
		{
			//var sqlParameters = new DynamicParameters();
			//sqlParameters.Add("PID", request.PID.Trim());

			hasCompleted = foundEnd; //&& await surveyDatabaseManager
									 //.GetRespondentCountAsync(request.SurveyID, $"IsSurveyTaker = 1 AND Status = 5 AND PID = @PID", sqlParameters) > 0;
			logs.Dump();
		}
	}
	catch (OperationCanceledException)
	{
		throw;
	}
	catch (Exception ex)
	{
		logs.Add($"Found error: {ex.Message.Replace("\n", string.Empty).Replace("\r", string.Empty)} with source: {responseBody.Replace("\t", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty)}");
		successful = false;
	}

	stopwatch.Stop();

	return new SurveyTakerRespondentResult
	{
		EllapsedMilliseconds = stopwatch.ElapsedMilliseconds,
		HasCompleted = hasCompleted,
		Successful = successful,
		Logs = logs
	};

	string GetFormData(HtmlDocument htmlDoc)
	{
		var descendants = htmlDoc.DocumentNode.Descendants();

		var valuesToPost = descendants
			.Where(n => n.NodeType == HtmlNodeType.Element)
			.Select(n =>
			{
				SurveyTakerQuestion question = null;

				if (n.Name.Equals("div", StringComparison.InvariantCultureIgnoreCase) && n.GetAttributeValue("class", string.Empty).Contains("st-question-container"))
					question = new SurveyTakerQuestionMvc(n, htmlDoc.DocumentNode);

				if (n.Name.Equals("section", StringComparison.InvariantCultureIgnoreCase) && n.Attributes.Contains("st-name"))
					question = new SurveyTakerQuestionCore(n, htmlDoc.DocumentNode);
				question?.SurveyTakerResponses.Dump();
				return question?.ProcessValueToPost() ?? default;
			})
			.Where(vtp => !string.IsNullOrEmpty(vtp))
			.Select(vtp => vtp)
			.ToList();

		return string.Join("&", valuesToPost);
	}
}

public class SurveyTakerRespondentResult
{
	public List<string> Logs { get; set; } = new List<string>();
	public bool HasCompleted { get; set; }
	public bool Successful { get; set; }
	public long EllapsedMilliseconds { get; set; }
}

public class SurveyTakerRespondentRequest
{
	public string PID { get; set; }
	public string SurveyUrl { get; set; }
	public int SurveyID { get; internal set; }
}

public sealed class SurveyTakerQuestionCore : SurveyTakerQuestion
{
	public SurveyTakerQuestionCore(HtmlNode questionNode, HtmlNode documentNode) : base(questionNode, documentNode) { }

	protected override void BuildResponses(HtmlNode questionNode, HtmlNode _)
	{
		MakeResponsesExclusive = bool.TryParse(questionNode.GetAttributeValue("data-exclusiveresponses", string.Empty), out bool makeResponsesExclusive) && makeResponsesExclusive;
		SurveyTakerResponses = new List<SurveyTakerResponse>();

		var elements = questionNode
				.Descendants()
				.Where(n => n.NodeType == HtmlNodeType.Element);

		if (SurveyQuestionDisplayType == SurveyQuestionDisplayType.PRICESENSITIVITY)
		{
			SurveyTakerResponses.AddRange(elements.FirstOrDefault(e => e.HasClass("price-sensitivity-meter"))?
			.Descendants()
			.Where(e => e.HasClass("tick-container"))
			.Select(tick => new SurveyTakerPriceSensitvityResponseCore(tick))
			.ToList<SurveyTakerResponse>());
		}

		SurveyTakerResponses.AddRange(elements
			.Where(n => n.Name == "input")
			.SelectMany(input =>
			{
				if (input.GetAttributeValue("type", string.Empty) == "range")
				{
					return elements.FirstOrDefault(e => e.Id == input.GetAttributeValue("data-ticksid", null))
						?.Descendants("div")
						.Where(n => n.HasClass("form-range__tick"))
						.Select(option => new SurveyTakerSliderResponseCore(input, option))
						.ToList<SurveyTakerResponse>()
						?? new List<SurveyTakerResponse> { new SurveyTakerSliderResponseCore(input) };
				}
				else
					return new List<SurveyTakerResponse> { new SurveyTakerInputResponseCore(input) };
			}));

		SurveyTakerResponses.AddRange(elements
			.Where(n => n.Name == "select")
			.SelectMany(dropdown =>
				dropdown
					.Descendants("option")
					.Where(option => !string.IsNullOrEmpty(option.GetAttributeValue("value", string.Empty)))
					.Select(option => new SurveyTakerDropdownResponseCore(dropdown, option))));

		SurveyTakerResponses.AddRange(elements
			.Where(n => n.Name == "textarea")
			.Select(textarea => new SurveyTakerTextareaResponseCore(textarea)));
	}

	public override string ProcessValueToPost()
	{
		if (!GetAllValidSurveyTakerResponses(out var allValidSurveyTakerResponses))
			return string.Empty;

		#region Process Krusty Value/NotValue | Numeric Min/Max

		switch (SurveyQuestionType)
		{
			case SurveyQuestionType.CheckOne: ProcessKrustyValueNotValue_CheckOne(ref allValidSurveyTakerResponses); break;
			case SurveyQuestionType.GridCheckOne:
				{
					if (SurveyQuestionDisplayType != SurveyQuestionDisplayType.RANKSORT) ProcessKrustyValueNotValue_GridCheckOne(ref allValidSurveyTakerResponses);
					break;
				}
			case SurveyQuestionType.Numeric:
				{
					switch (SurveyQuestionDisplayType)
					{
						case SurveyQuestionDisplayType.BUTTONRATING: ProcessKrustyValueNotValue_CheckOne(ref allValidSurveyTakerResponses); break;
						default: ProcessKrustyValueNotValue_Numeric(ref allValidSurveyTakerResponses); break;
					}

					break;
				}
			case SurveyQuestionType.GridNumeric:
				{
					switch (SurveyQuestionDisplayType)
					{
						case SurveyQuestionDisplayType.BUTTONRATING: ProcessKrustyValueNotValue_GridCheckOne(ref allValidSurveyTakerResponses); break;
						case SurveyQuestionDisplayType.PRICESENSITIVITY: break;
						default: ProcessKrustyValueNotValue_GridNumeric(ref allValidSurveyTakerResponses); break;
					}

					break;
				}
		}

		#endregion

		#region Compute Values to Post

		var valuesToPost = allValidSurveyTakerResponses
			.GroupBy(str => str.Type ?? string.Empty)
			.SelectMany(gstr =>
			{
				var thisGroupedValidSurveyTakerResponses = gstr.ToList();

				switch (gstr.Key)
				{
					case "radio":
					case "dropdown":
						{
							var selectedSurveyTakerResponses = new List<SurveyTakerResponse>();

							switch (SurveyQuestionType)
							{
								case SurveyQuestionType.GridCheckOne:
									{
										radio_dropdown_GridCheckOne();
										break;
									}
								case SurveyQuestionType.GridNumeric:
									{
										switch (SurveyQuestionDisplayType)
										{
											case SurveyQuestionDisplayType.BUTTONRATING: radio_dropdown_GridCheckOne(); break;
											default: radio_dropdown_Default(); break;
										}

										break;
									}
								default: radio_dropdown_Default(); break;
							}

							return selectedSurveyTakerResponses
								.Select(vstr => EscapeValueToPost(vstr.DataPointName, vstr.Value, vstr.MaxLength))
								.ToList();

							void radio_dropdown_GridCheckOne()
							{
								selectedSurveyTakerResponses.AddRange(MakeResponsesExclusive
										? SurveyTakerExclusiveStatementsRandomizer(thisGroupedValidSurveyTakerResponses)
										: thisGroupedValidSurveyTakerResponses
										.GroupBy(vstr => vstr.DataPointName)
										.Select(gp => SurveyTakerResponseRandomizer(gp.ToList()))
										.ToList());
							}

							void radio_dropdown_Default() => selectedSurveyTakerResponses.Add(SurveyTakerResponseRandomizer(thisGroupedValidSurveyTakerResponses));
						}
					case "checkbox":
						{
							var toPost = new List<string>();

							toPost.AddRange(thisGroupedValidSurveyTakerResponses
								.Where(vstr => vstr.DataPointName.EndsWith("OptOut", StringComparison.OrdinalIgnoreCase))
								.Where(vstr => ZenRandom.Next(1, 101) > 50)
								.Select(vstr => EscapeValueToPost(vstr.DataPointName, vstr.Value, vstr.MaxLength)));

							toPost.AddRange(thisGroupedValidSurveyTakerResponses
								.Where(vstr => !vstr.DataPointName.EndsWith("OptOut", StringComparison.OrdinalIgnoreCase))
								.GroupBy(str => str.VirtualGroupName)
								.SelectMany(gp => SurveyTakerResponsesRandomizer(gp.ToList(), SelectedResponseCount))
								.Select(vstr => EscapeValueToPost(vstr.DataPointName, vstr.Value, vstr.MaxLength)));

							return toPost;
						}
					case "hidden":
					case "password":
						{
							var toPost = new List<string>();
							if (SurveyQuestionType == SurveyQuestionType.Stop && !string.IsNullOrEmpty(StopCode))
							{
								toPost.Add(EscapeValueToPost(Name, StopCode));
							}
							else if (SurveyQuestionDisplayType == SurveyQuestionDisplayType.RANKING || SurveyQuestionDisplayType == SurveyQuestionDisplayType.RANKSORT)
							{
								toPost.AddRange(Enumerable.Zip(
									first: ShuffleCollection(thisGroupedValidSurveyTakerResponses),
									second: thisGroupedValidSurveyTakerResponses.First().KrustyValue.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries),
									resultSelector: (first, second) => (DataPointName: first.DataPointName, Value: second))
								.OrderBy(r => r.DataPointName)
								.Select(r => EscapeValueToPost(r.DataPointName, r.Value)));
							}
							else if (SurveyQuestionDisplayType == SurveyQuestionDisplayType.DATEPICKER)
							{
								var boundFormat = "yyyy-MM-dd";
								var minSet = DateTime.TryParseExact(MinDate, boundFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var min);
								var maxSet = DateTime.TryParseExact(MaxDate, boundFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var max);

								if (!minSet)
								{
									min = (max != DateTime.MinValue ? max : DateTime.UtcNow).AddYears(-1);
								}
								if (!maxSet)
								{
									max = (min != DateTime.MinValue ? min : DateTime.UtcNow).AddYears(1);
								}

								var randDays = ZenRandom.Next(0, (max - min).Days + 1);
								var randDate = min.AddDays(randDays);
								toPost.Add(EscapeValueToPost(Name, randDate.ToString(DateFormat)));
							}
							else
							{
								var selectedSurveyTakerResponse = SurveyTakerResponseRandomizer(thisGroupedValidSurveyTakerResponses);
								toPost.Add(EscapeValueToPost(selectedSurveyTakerResponse.DataPointName, selectedSurveyTakerResponse.Value));
							}

							return toPost;
						}
					case "text":
					case "range":
					case "number":
						{
							var toPost = new List<string>();
							var otherSurveyTakerResponse = thisGroupedValidSurveyTakerResponses.FirstOrDefault(vstr => vstr.DataPointName.IndexOf("other", StringComparison.OrdinalIgnoreCase) > -1);
							if (otherSurveyTakerResponse != null)
							{
								toPost.Add(
									EscapeValueToPost(
										otherSurveyTakerResponse.DataPointName,
										ZenRandom.Next(1, 101) > 100 - otherSurveyTakerResponse.PercentageChance ? $"ST {otherSurveyTakerResponse.DataPointName}" : string.Empty,
										otherSurveyTakerResponse.MaxLength));
							}

							var numericSurveyTakerResponses = thisGroupedValidSurveyTakerResponses.Where(vstr => vstr.TextType == SurveyTakerResponseTextType.Numeric || vstr.TextType == SurveyTakerResponseTextType.Decimal).ToList();
							if (numericSurveyTakerResponses.Any())
							{
								var totalRequiredValueString = RandomCustomValuePicker(TotalRequiredValue);

								if (!string.IsNullOrEmpty(TotalRequiredValue) && int.TryParse(totalRequiredValueString, out int totalRequiredValue))
								{
									var sumValues = new Dirichlet(1, numericSurveyTakerResponses.Count, ZenRandom.GetInstance()).Sample().Select(p => (int)(p * totalRequiredValue)).ToList();

									if (sumValues.Sum() < totalRequiredValue)
									{
										sumValues[ZenRandom.Next(0, sumValues.Count)] += totalRequiredValue - sumValues.Sum();
									}

									toPost.AddRange(numericSurveyTakerResponses
										.Select((vstr, index) => EscapeValueToPost(vstr.DataPointName, sumValues[index].ToString(), vstr.MaxLength))
										.ToList());
								}
								else
								{
									toPost.AddRange(numericSurveyTakerResponses
										.Select(vstr => EscapeValueToPost(vstr.DataPointName, string.IsNullOrEmpty(vstr.Value) ? ZenRandom.Next().ToString() : vstr.Value, vstr.MaxLength))
										.ToList());
								}
							}

							var nonNumericSurveyTakerResponses = thisGroupedValidSurveyTakerResponses.Where(vstr => vstr.TextType != SurveyTakerResponseTextType.Numeric && vstr.TextType != SurveyTakerResponseTextType.Decimal && vstr.DataPointName.IndexOf("other", StringComparison.OrdinalIgnoreCase) <= -1).ToList();

							if (nonNumericSurveyTakerResponses.Any())
							{
								toPost.AddRange(nonNumericSurveyTakerResponses
									.Select(vstr => EscapeValueToPost(vstr.DataPointName, $"ST {vstr.DataPointName}", vstr.MaxLength))
									.ToList());
							}

							return toPost;
						}
					case "textarea":
						{
							return thisGroupedValidSurveyTakerResponses
								.Select(vstr =>
								{
									var fakeResponse = string.Empty;

									switch (SurveyQuestionDisplayType)
									{
										case SurveyQuestionDisplayType.IMAGEHIGHLIGHT:
											{
												fakeResponse = "[\"1 ,1\"]";
												break;
											}
										default:
											{
												fakeResponse = $"ST {vstr.DataPointName}";
												break;
											}
									}

									return EscapeValueToPost(vstr.DataPointName, fakeResponse, vstr.MaxLength);
								})
								.ToList();
						}
					case "file":
						{
							return thisGroupedValidSurveyTakerResponses
									.Select(vstr =>
									{
										var fakeResponse = string.Join(",", Enumerable.Range(1, RequiredNumberOfFiles).Select(i => $"ST_{i}.txt"));
										return EscapeValueToPost(vstr.DataPointName, fakeResponse);
									})
									.ToList();
						}
					case "slider":
						{
							return thisGroupedValidSurveyTakerResponses
									.GroupBy(r => r.VirtualGroupName)
									.Select(gp => SurveyTakerResponseRandomizer(gp.ToList()))
									.Select(response => EscapeValueToPost(response.VirtualGroupName, response.Value));
						}
					case "pricesensitivity":
						{
							var priceCount = thisGroupedValidSurveyTakerResponses.Count;
							var point1 = ZenRandom.Next(0, priceCount);
							var point2 = ZenRandom.Next(Math.Min(point1 + 1, priceCount), priceCount);
							var point3 = ZenRandom.Next(Math.Min(point2 + 1, priceCount), priceCount);
							var point4 = ZenRandom.Next(0, point1);
							return new List<string>
							{
								EscapeValueToPost($"{Name}_1", thisGroupedValidSurveyTakerResponses[point1].Value),
								EscapeValueToPost($"{Name}_5", thisGroupedValidSurveyTakerResponses[point1].NameIndex),
								EscapeValueToPost($"{Name}_2", thisGroupedValidSurveyTakerResponses[point1].Value),
								EscapeValueToPost($"{Name}_6", thisGroupedValidSurveyTakerResponses[point1].NameIndex),
								EscapeValueToPost($"{Name}_3", thisGroupedValidSurveyTakerResponses[point1].Value),
								EscapeValueToPost($"{Name}_7", thisGroupedValidSurveyTakerResponses[point1].NameIndex),
								EscapeValueToPost($"{Name}_4", thisGroupedValidSurveyTakerResponses[point1].Value),
								EscapeValueToPost($"{Name}_8", thisGroupedValidSurveyTakerResponses[point1].NameIndex)
							};
						}
					default:
						{
							return new List<string>();
						}
				}
			})
			.ToList();

		#endregion
		return string.Join("&", valuesToPost);
	}
}

public sealed class SurveyTakerQuestionMvc : SurveyTakerQuestion
{
	public SurveyTakerQuestionMvc(HtmlNode questionNode, HtmlNode documentNode) : base(questionNode, documentNode) { }

	protected override void BuildResponses(HtmlNode questionNode, HtmlNode documentNode)
	{
		var validMakeResponsesExclusive = bool.TryParse(documentNode
			.Descendants()
			.Where(n => n.NodeType == HtmlNodeType.Element)
			.Select(n => n.GetAttributeValue("makeresponsesexclusive", string.Empty))
			.Where(s => !s.IsNullOrEmpty())
			.FirstOrDefault(),
			out bool makeResponsesExclusive);

		MakeResponsesExclusive = validMakeResponsesExclusive && makeResponsesExclusive;

		SurveyTakerResponses = new List<SurveyTakerResponse>();

		var inputs = documentNode
			.Descendants()
			.Where(n => n.NodeType == HtmlNodeType.Element)
			.Where(n => n.Name == "input" && n.GetAttributeValue("questionname", string.Empty).Equals(Name, StringComparison.OrdinalIgnoreCase))
			.Select(input => new SurveyTakerInputResponseMvc(input))
			.ToList();

		SurveyTakerResponses.AddRange(inputs);

		var dropdowns = documentNode
			.Descendants()
			.Where(n => n.NodeType == HtmlNodeType.Element)
			.Where(n => n.Name == "select" && n.GetAttributeValue("questionname", string.Empty).Equals(Name, StringComparison.OrdinalIgnoreCase))
			.SelectMany(dropdown =>
				dropdown.Descendants("option")
					.Where(option => !string.IsNullOrEmpty(option.GetAttributeValue("value", string.Empty)))
					.Select(option => new SurveyTakerDropdownResponseMvc(dropdown, option)))
			.ToList();

		SurveyTakerResponses.AddRange(dropdowns);

		var textareas = documentNode
			.Descendants()
			.Where(n => n.NodeType == HtmlNodeType.Element)
			.Where(n => n.Name == "textarea" && n.GetAttributeValue("questionname", string.Empty).Equals(Name, StringComparison.OrdinalIgnoreCase))
			.Select(textarea => new SurveyTakerTextareaResponseMvc(textarea))
			.ToList();

		SurveyTakerResponses.AddRange(textareas);
	}

	public override string ProcessValueToPost()
	{
		if (!GetAllValidSurveyTakerResponses(out var allValidSurveyTakerResponses))
			return string.Empty;

		#region Process Krusty Value/NotValue | Numeric Min/Max

		switch (SurveyQuestionType)
		{
			case SurveyQuestionType.CheckOne: ProcessKrustyValueNotValue_CheckOne(ref allValidSurveyTakerResponses); break;
			case SurveyQuestionType.GridCheckOne: ProcessKrustyValueNotValue_GridCheckOne(ref allValidSurveyTakerResponses); break;
			case SurveyQuestionType.Numeric: ProcessKrustyValueNotValue_Numeric(ref allValidSurveyTakerResponses); break;
			case SurveyQuestionType.GridNumeric: ProcessKrustyValueNotValue_GridNumeric(ref allValidSurveyTakerResponses); break;
		}

		#endregion

		#region Compute Values to Post

		var valuesToPost = allValidSurveyTakerResponses
			.GroupBy(str => str.Type ?? string.Empty)
			.SelectMany(gstr =>
			{
				var thisGroupedValidSurveyTakerResponses = gstr.ToList();

				switch (gstr.Key)
				{
					case "radio":
					case "dropdown":
						{
							var selectedSurveyTakerResponses = new List<SurveyTakerResponse>();

							switch (SurveyQuestionType)
							{
								case SurveyQuestionType.GridCheckOne:
									{
										selectedSurveyTakerResponses.AddRange(MakeResponsesExclusive
											? SurveyTakerExclusiveStatementsRandomizer(thisGroupedValidSurveyTakerResponses)
											: thisGroupedValidSurveyTakerResponses
											.GroupBy(vstr => vstr.DataPointName)
											.Select(gp => SurveyTakerResponseRandomizer(gp.ToList()))
											.ToList());

										break;
									}
								default:
									{
										selectedSurveyTakerResponses.Add(SurveyTakerResponseRandomizer(thisGroupedValidSurveyTakerResponses));
										break;
									}
							}

							return selectedSurveyTakerResponses
								.Select(vstr => EscapeValueToPost(vstr.DataPointName, vstr.Value, vstr.MaxLength))
								.ToList();
						}
					case "checkbox":
						{
							return thisGroupedValidSurveyTakerResponses
								.GroupBy(str => str.VirtualGroupName)
								.SelectMany(gp => SurveyTakerResponsesRandomizer(gp.ToList(), SelectedResponseCount))
								.Select(vstr => EscapeValueToPost(vstr.DataPointName, vstr.Value, vstr.MaxLength))
								.ToList();
						}
					case "hidden":
					case "password":
						{
							var toPost = new List<string>();

							if (SurveyQuestionType == SurveyQuestionType.Stop && !string.IsNullOrEmpty(StopCode))
							{
								toPost.Add(EscapeValueToPost(Name, StopCode));
							}
							else
							{
								var selectedSurveyTakerResponse = SurveyTakerResponseRandomizer(thisGroupedValidSurveyTakerResponses);

								toPost.Add(EscapeValueToPost(selectedSurveyTakerResponse.DataPointName, selectedSurveyTakerResponse.Value));
							}

							return toPost;
						}
					case "text":
						{
							var toPost = new List<string>();

							var otherSurveyTakerResponse = thisGroupedValidSurveyTakerResponses.FirstOrDefault(vstr => vstr.DataPointName.IndexOf("other", StringComparison.OrdinalIgnoreCase) > -1);

							if (otherSurveyTakerResponse != null)
							{
								toPost.Add(
									EscapeValueToPost(
										otherSurveyTakerResponse.DataPointName,
										ZenRandom.Next(1, 101) > 100 - otherSurveyTakerResponse.PercentageChance ? $"ST {otherSurveyTakerResponse.DataPointName}" : string.Empty,
										otherSurveyTakerResponse.MaxLength));
							}

							var numericSurveyTakerResponses = thisGroupedValidSurveyTakerResponses.Where(vstr => vstr.TextType == SurveyTakerResponseTextType.Numeric || vstr.TextType == SurveyTakerResponseTextType.Decimal).ToList();

							if (numericSurveyTakerResponses.Any())
							{
								var totalRequiredValueString = RandomCustomValuePicker(TotalRequiredValue);

								if (!string.IsNullOrEmpty(TotalRequiredValue) && int.TryParse(totalRequiredValueString, out int totalRequiredValue))
								{
									var sumValues = new Dirichlet(1, numericSurveyTakerResponses.Count, ZenRandom.GetInstance()).Sample().Select(p => (int)(p * totalRequiredValue)).ToList();

									if (sumValues.Sum() < totalRequiredValue)
									{
										sumValues[ZenRandom.Next(0, sumValues.Count)] += totalRequiredValue - sumValues.Sum();
									}

									toPost.AddRange(numericSurveyTakerResponses
										.Select((vstr, index) => EscapeValueToPost(vstr.DataPointName, sumValues[index].ToString(), vstr.MaxLength))
										.ToList());
								}
								else
								{
									toPost.AddRange(numericSurveyTakerResponses
										.Select(vstr => EscapeValueToPost(vstr.DataPointName, string.IsNullOrEmpty(vstr.Value) ? ZenRandom.Next().ToString() : vstr.Value, vstr.MaxLength))
										.ToList());
								}
							}

							var nonNumericSurveyTakerResponses = thisGroupedValidSurveyTakerResponses.Where(vstr => vstr.TextType != SurveyTakerResponseTextType.Numeric && vstr.TextType != SurveyTakerResponseTextType.Decimal && vstr.DataPointName.IndexOf("other", StringComparison.OrdinalIgnoreCase) <= -1).ToList();

							if (nonNumericSurveyTakerResponses.Any())
							{
								toPost.AddRange(nonNumericSurveyTakerResponses
									.Select(vstr => EscapeValueToPost(vstr.DataPointName, $"ST {vstr.DataPointName}", vstr.MaxLength))
									.ToList());
							}

							return toPost;
						}
					case "textarea":
						{
							return thisGroupedValidSurveyTakerResponses
								.Select(vstr =>
								{
									var fakeResponse = string.Empty;

									switch (SurveyQuestionDisplayType)
									{
										case SurveyQuestionDisplayType.IMAGEHIGHLIGHT:
											{
												fakeResponse = "[\"1 ,1\"]";
												break;
											}
										default:
											{
												fakeResponse = $"ST {vstr.DataPointName}";
												break;
											}
									}

									return EscapeValueToPost(vstr.DataPointName, fakeResponse, vstr.MaxLength);
								})
								.ToList();
						}
					default: return new List<string>();
				}
			})
			.ToList();

		#endregion

		return string.Join("&", valuesToPost);
	}
}

public abstract class SurveyTakerQuestion
{
	public List<SurveyTakerResponse> SurveyTakerResponses { get; set; }

	public SurveyTakerQuestion(HtmlNode questionNode, HtmlNode documentNode)
	{
		Name = questionNode.GetAttributeValue("st-name", string.Empty);
		SurveyQuestionType = Enum.TryParse(questionNode.GetAttributeValue("st-surveyquestiontype", string.Empty), true, out SurveyQuestionType surveyQuestionType) ? surveyQuestionType : throw new ArgumentException($"A {nameof(SurveyQuestionType)} was not found.", nameof(surveyQuestionType));
		SurveyQuestionDisplayType = Enum.TryParse(questionNode.GetAttributeValue("st-surveyquestiondisplaytype", string.Empty), true, out SurveyQuestionDisplayType surveyQuestionDisplayType) ? surveyQuestionDisplayType : default;

		SelectedResponseCount = int.TryParse(RandomCustomValuePicker(questionNode.GetAttributeValue("st-selectedresponsecount", "-1")), out int selectedResponseCount) && selectedResponseCount > -1
			? selectedResponseCount
			: (int?)null;

		TotalRequiredValue = questionNode.GetAttributeValue("st-totalrequiredvalue", string.Empty);
		StopCode = questionNode.GetAttributeValue("st-stopcode", string.Empty);
		KrustyValue = questionNode.GetAttributeValue("st-value", string.Empty);
		KrustyNotValue = questionNode.GetAttributeValue("st-notvalue", string.Empty);
		NumericMin = double.TryParse(questionNode.GetAttributeValue("st-numericmin", string.Empty), out double numericMin) ? numericMin : default;
		NumericMax = double.TryParse(questionNode.GetAttributeValue("st-numericmax", string.Empty), out double numericMax) ? numericMax : int.MaxValue - 1;
		RequiredNumberOfFiles = int.TryParse(questionNode.GetAttributeValue("st-requirednumberoffiles", string.Empty), out int requiredNumberOfFiles) ? requiredNumberOfFiles : 1;
		MinDate = questionNode.GetAttributeValue("st-mindate", string.Empty);
		MaxDate = questionNode.GetAttributeValue("st-maxdate", string.Empty);
		DateFormat = questionNode.GetAttributeValue("st-dateformat", "MM/dd/yyyy");

		BuildResponses(questionNode, documentNode);
	}

	protected abstract void BuildResponses(HtmlNode questionNode, HtmlNode documentNode);

	public string Name { get; set; }
	public SurveyQuestionType SurveyQuestionType { get; set; }
	public SurveyQuestionDisplayType SurveyQuestionDisplayType { get; set; }
	public string TotalRequiredValue { get; set; }
	public int? SelectedResponseCount { get; set; }
	public string StopCode { get; set; }
	public string KrustyValue { get; set; }
	public string KrustyNotValue { get; set; }
	public double? NumericMin { get; set; }
	public double? NumericMax { get; set; }
	public bool MakeResponsesExclusive { get; set; }
	public string MinMaxValues => NumericMin.HasValue || NumericMax.HasValue ? $"{NumericMin}:{NumericMax}" : string.Empty;
	public int RequiredNumberOfFiles { get; set; }
	public string MinDate { get; set; }
	public string MaxDate { get; set; }
	public string DateFormat { get; set; }


	public abstract string ProcessValueToPost();

	protected bool GetAllValidSurveyTakerResponses(out List<SurveyTakerResponse> allValidSurveyTakerResponses)
	{
		allValidSurveyTakerResponses = SurveyTakerResponses.Where(str => str.EligibilityType == SurveyTakerResponseEligibilityType.Eligible).ToList();

		if (!allValidSurveyTakerResponses.Any())
			allValidSurveyTakerResponses = SurveyTakerResponses.Where(str => str.EligibilityType == SurveyTakerResponseEligibilityType.NotSet).ToList();

		return allValidSurveyTakerResponses.Any();
	}

	protected string RandomCustomValuePicker(string customValues, bool checkMinMax = false)
	{
		if (string.IsNullOrEmpty(customValues))
			return checkMinMax && (NumericMin.HasValue || NumericMax.HasValue) ? RandomNext(NumericMin ?? 0, NumericMax ?? int.MaxValue).ToString() : ZenRandom.Next().ToString();

		var valuesToPick = customValues.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(v => v.Trim()).ToList();

		if (!valuesToPick.Any())
			return checkMinMax && (NumericMin.HasValue || NumericMax.HasValue) ? RandomNext(NumericMin ?? 0, NumericMax ?? int.MaxValue).ToString() : ZenRandom.Next().ToString();

		var pickedValue = valuesToPick[ZenRandom.Next(0, valuesToPick.Count)];

		if (pickedValue.IndexOf(":") > -1)
		{
			var rangeParts = pickedValue.Split(':');

			var rangeMin = double.TryParse(rangeParts[0], out double thisMin) ? thisMin : 0;
			var rangeMax = double.TryParse(rangeParts[1], out double thisMax) ? thisMax : int.MaxValue;

			// Random.Next excludes "maxValue" so we add 1 to it.
			return checkMinMax && (NumericMin.HasValue || NumericMax.HasValue)
				? RandomNext(Math.Max(rangeMin, NumericMin ?? 0), Math.Min(rangeMax, NumericMax ?? int.MaxValue)).ToString()
				: RandomNext(rangeMin, rangeMax).ToString();
		}

		return pickedValue;
	}

	protected bool CustomValueContains(string customValues, string value)
	{
		if (string.IsNullOrEmpty(customValues) || string.IsNullOrEmpty(value))
			return false;

		foreach (var customValue in customValues.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(v => v.Trim()))
		{
			if (customValue.IndexOf(":") > -1)
			{
				var rangeParts = customValue.Split(':');

				var minValue = double.TryParse(rangeParts[0], out double thisMin) ? (int)Math.Floor(thisMin) : int.MinValue;
				var maxValue = double.TryParse(rangeParts[1], out double thisMax) ? (int)Math.Ceiling(thisMax) : int.MaxValue;
				var intValue = int.TryParse(value, out int thisValue) ? thisValue : int.MinValue;

				return minValue <= intValue && intValue <= maxValue;
			}
			else
			{
				return customValue.Equals(value, StringComparison.OrdinalIgnoreCase);
			}
		}

		return false;
	}

	protected bool CustomNotValueContains(string customValues, string value)
	{
		if (string.IsNullOrEmpty(customValues) || string.IsNullOrEmpty(value))
			return true;

		return customValues.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(v => v.Trim())
			.All(customValue =>
			{
				if (customValue.IndexOf(":") > -1)
				{
					var rangeParts = customValue.Split(':');

					var minValue = double.TryParse(rangeParts[0], out double thisMin) ? (int)Math.Floor(thisMin) : int.MinValue;
					var maxValue = double.TryParse(rangeParts[1], out double thisMax) ? (int)Math.Ceiling(thisMax) : int.MaxValue;
					var intValue = int.TryParse(value, out int thisValue) ? thisValue : int.MinValue;

					if (minValue <= intValue && intValue <= maxValue)
					{
						return false;
					}
				}
				else if (customValue.Equals(value, StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}

				return true;
			});
	}

	protected string RandomNext(double min, double max)
	{
		if ((min % 1) == 0 && (max % 1) == 0)
		{
			return ZenRandom.Next((int)min, (int)max + 1).ToString();
		}
		else
		{
			return ZenRandom.NextDouble(min, max + 1).ToString();
		}
	}

	protected List<T> ShuffleCollection<T>(IEnumerable<T> collection)
	{
		return collection
				.Select(str => new { Index = ZenRandom.Next(), Object = str })
				.OrderBy(no => no.Index)
				.Select(no => no.Object)
				.ToList();
	}

	protected List<SurveyTakerResponse> SurveyTakerResponsesTaker(List<SurveyTakerResponse> thisSurveyTakerResponses, int take)
	{
		if (take <= 0)
		{
			throw new ArgumentException($"You need to take something tho", nameof(take));
		}

		if (thisSurveyTakerResponses.Count < take)
		{
			throw new ArgumentException($"There are less responses to take ({thisSurveyTakerResponses.Count}) than required ({take})");
		}

		var result = new List<SurveyTakerResponse>();

		var shuffledSurveyTakerResponses = ShuffleCollection(thisSurveyTakerResponses);

		while (result.Count < take)
		{
			for (int i = 0; i < shuffledSurveyTakerResponses.Count; i++)
			{
				var thisSurveyTakerResponse = shuffledSurveyTakerResponses[i];

				if (ZenRandom.Next(1, 101) > 100 - thisSurveyTakerResponse.PercentageChance)
				{
					result.Add(thisSurveyTakerResponse);
					shuffledSurveyTakerResponses.RemoveAt(i);
					break;
				}
			}
		}

		return result;
	}

	protected List<SurveyTakerResponse> SurveyTakerResponsesRandomizer(List<SurveyTakerResponse> thisSurveyTakerResponses, int? selectedResponseCount = null)
	{
		var result = new List<SurveyTakerResponse>();

		var shuffledSurveyTakerResponses = ShuffleCollection(thisSurveyTakerResponses);

		if (selectedResponseCount.HasValue)
		{
			if (selectedResponseCount == 0)
			{
				throw new ArgumentException($"The provided value is not valid (0)", nameof(selectedResponseCount));
			}

			if (selectedResponseCount == 1)
			{
				return new List<SurveyTakerResponse> { SurveyTakerResponseRandomizer(thisSurveyTakerResponses) };
			}

			// If we are picking more than 1 response, let's remove the exclusives if any.
			shuffledSurveyTakerResponses = shuffledSurveyTakerResponses
				.Where(str => !str.Exclusive)
				.ToList();

			while (result.Count < selectedResponseCount)
			{
				for (int i = 0; i < shuffledSurveyTakerResponses.Count; i++)
				{
					var thisSurveyTakerResponse = shuffledSurveyTakerResponses[i];

					if (ZenRandom.Next(1, 101) > 100 - thisSurveyTakerResponse.PercentageChance)
					{
						if (thisSurveyTakerResponse.GroupExclusive)
						{
							if (selectedResponseCount <= result.Count + shuffledSurveyTakerResponses.Count(str => !str.GroupName.Equals(thisSurveyTakerResponse.GroupName, StringComparison.OrdinalIgnoreCase)) + 1)
							{
								result.Add(thisSurveyTakerResponse);
								// we just added a GroupExclusive to the collection so lets remove the entire group.
								shuffledSurveyTakerResponses = shuffledSurveyTakerResponses
									.Where(str => !str.GroupName.Equals(thisSurveyTakerResponse.GroupName, StringComparison.OrdinalIgnoreCase))
									.ToList();
							}
							else
							{
								// If we add this GroupExclusive we are going to run short in responses, so let's just smoke this one.
								shuffledSurveyTakerResponses.RemoveAt(i);
							}
						}
						else
						{
							result.Add(thisSurveyTakerResponse);
							shuffledSurveyTakerResponses.RemoveAt(i);
						}

						break;
					}
				}

				if (!shuffledSurveyTakerResponses.Any())
				{
					break;
				}
			}
		}
		else
		{
			foreach (var thisSurveyTakerResponse in shuffledSurveyTakerResponses)
			{
				if (ZenRandom.Next(1, 101) > 100 - thisSurveyTakerResponse.PercentageChance)
				{
					if (thisSurveyTakerResponse.Exclusive && !result.Any())
					{
						result.Add(thisSurveyTakerResponse);
						break;
					}

					if (thisSurveyTakerResponse.GroupExclusive && !result.Any(r => r.GroupName.Equals(thisSurveyTakerResponse.GroupName, StringComparison.OrdinalIgnoreCase)))
					{
						result.Add(thisSurveyTakerResponse);
					}

					if (!thisSurveyTakerResponse.Exclusive &&
						!thisSurveyTakerResponse.GroupExclusive &&
						!result.Any(r => r.GroupExclusive && r.GroupName.Equals(thisSurveyTakerResponse.GroupName, StringComparison.OrdinalIgnoreCase)))
					{
						result.Add(thisSurveyTakerResponse);
					}
				}
			}
		}

		if (!result.Any())
		{
			result.Add(SurveyTakerResponseRandomizer(thisSurveyTakerResponses));
		}

		return result;
	}

	protected SurveyTakerResponse SurveyTakerResponseRandomizer(List<SurveyTakerResponse> thisSurveyTakerResponses) =>
		SurveyTakerResponsesTaker(thisSurveyTakerResponses, 1)
		.FirstOrDefault();

	protected List<SurveyTakerResponse> SurveyTakerExclusiveStatementsRandomizer(List<SurveyTakerResponse> responses) =>
		Enumerable
			.Zip(
				first: ShuffleCollection(responses.Select(s => s.DataPointName).Distinct()),
				second: ShuffleCollection(responses.Select(s => s.Value).Distinct()),
				resultSelector: (first, second) => (DataPointName: first, Value: second))
			.Select(rs => responses.First(r => r.DataPointName == rs.DataPointName && r.Value == rs.Value))
			.OrderBy(str => str.DataPointName)
			.ToList();

	protected string EscapeValueToPost(string dataPointName, string value, int maxLength = int.MaxValue) =>
		$"{dataPointName}={Uri.EscapeDataString(value.Substring(0, Math.Min(value.Length, maxLength)))}";

	protected void ProcessCustomValues(SurveyTakerResponse str, string customValues, string customNotValues)
	{
		if (!string.IsNullOrEmpty(str.Value))
		{
			return;
		}

		if (string.IsNullOrEmpty(customValues) && str.MaxLength != SurveyTakerResponse.DefaultMaxLength)
		{
			customValues = $"0:{new string('9', str.MaxLength)}";
		}

		var backoff = 0;

		while (string.IsNullOrEmpty(str.Value) && backoff++ <= 500)
		{
			str.Value = RandomCustomValuePicker(customValues, checkMinMax: true);

			if (!CustomNotValueContains(customNotValues, str.Value))
			{
				str.Value = string.Empty;
			}
		}
	}

	protected void ProcessKrustyValueNotValue_CheckOne(ref List<SurveyTakerResponse> allValidSurveyTakerResponses)
	{
		// Krusty Value/NotValue works as a filter here

		if (!string.IsNullOrEmpty(KrustyValue))
			allValidSurveyTakerResponses = allValidSurveyTakerResponses.Where(str => CustomValueContains(KrustyValue, str.Value)).ToList();

		if (!string.IsNullOrEmpty(KrustyNotValue))
			allValidSurveyTakerResponses = allValidSurveyTakerResponses.Where(str => CustomNotValueContains(KrustyNotValue, str.Value)).ToList();
	}

	protected void ProcessKrustyValueNotValue_GridCheckOne(ref List<SurveyTakerResponse> allValidSurveyTakerResponses)
	{
		// Krusty Value/NotValue works as a filter here

		allValidSurveyTakerResponses = allValidSurveyTakerResponses
			.GroupBy(vstr => vstr.DataPointName)
			.SelectMany(gp =>
			{
				var thisGroupedValidSurveyTakerResponses = gp.ToList();

				var thisStatementKrustyValue = thisGroupedValidSurveyTakerResponses.First().KrustyValue;
				var thisStatementKrustyNotValue = thisGroupedValidSurveyTakerResponses.First().KrustyNotValue;

				if (!string.IsNullOrEmpty(thisStatementKrustyValue) || !string.IsNullOrEmpty(thisStatementKrustyNotValue))
				{
					if (!string.IsNullOrEmpty(thisStatementKrustyValue))
						thisGroupedValidSurveyTakerResponses = thisGroupedValidSurveyTakerResponses.Where(str => CustomValueContains(thisStatementKrustyValue, str.Value)).ToList();

					if (!string.IsNullOrEmpty(thisStatementKrustyNotValue))
						thisGroupedValidSurveyTakerResponses = thisGroupedValidSurveyTakerResponses.Where(str => CustomNotValueContains(thisStatementKrustyNotValue, str.Value)).ToList();
				}
				else if (!string.IsNullOrEmpty(KrustyValue) || !string.IsNullOrEmpty(KrustyNotValue))
				{
					if (!string.IsNullOrEmpty(KrustyValue))
						thisGroupedValidSurveyTakerResponses = thisGroupedValidSurveyTakerResponses.Where(str => CustomValueContains(KrustyValue, str.Value)).ToList();

					if (!string.IsNullOrEmpty(KrustyNotValue))
						thisGroupedValidSurveyTakerResponses = thisGroupedValidSurveyTakerResponses.Where(str => CustomNotValueContains(KrustyNotValue, str.Value)).ToList();
				}

				return thisGroupedValidSurveyTakerResponses;
			})
			.ToList();
	}

	protected void ProcessKrustyValueNotValue_Numeric(ref List<SurveyTakerResponse> allValidSurveyTakerResponses)
	{
		// Krusty Value/NotValue works as a value "picker"

		if (!string.IsNullOrEmpty(KrustyValue) || !string.IsNullOrEmpty(KrustyNotValue))
		{
			foreach (var str in allValidSurveyTakerResponses)
				ProcessCustomValues(str, KrustyValue, KrustyNotValue);
		}
		else if (!string.IsNullOrEmpty(MinMaxValues))
		{
			foreach (var str in allValidSurveyTakerResponses)
				str.Value = RandomCustomValuePicker(MinMaxValues);
		}
	}

	protected void ProcessKrustyValueNotValue_GridNumeric(ref List<SurveyTakerResponse> allValidSurveyTakerResponses)
	{
		// Krusty Value/NotValue works as a value "picker"

		foreach (var thisGroupedValidSurveyTakerResponses in allValidSurveyTakerResponses.GroupBy(vstr => vstr.DataPointName))
		{
			var thisStatementKrustyValues = thisGroupedValidSurveyTakerResponses.First().KrustyValue;
			var thisStatementKrustyNotValues = thisGroupedValidSurveyTakerResponses.First().KrustyNotValue;

			if (thisStatementKrustyValues.Any() || thisStatementKrustyNotValues.Any())
			{
				foreach (var str in thisGroupedValidSurveyTakerResponses)
					ProcessCustomValues(str, thisStatementKrustyValues, thisStatementKrustyNotValues);
			}
			else if (!string.IsNullOrEmpty(KrustyValue) || !string.IsNullOrEmpty(KrustyNotValue))
			{
				foreach (var str in thisGroupedValidSurveyTakerResponses)
					ProcessCustomValues(str, KrustyValue, KrustyNotValue);
			}
			else if (!string.IsNullOrEmpty(MinMaxValues))
			{
				foreach (var str in thisGroupedValidSurveyTakerResponses)
					str.Value = RandomCustomValuePicker(MinMaxValues);
			}
		}
	}
}

public abstract class SurveyTakerResponse
{
	public static readonly int DefaultMaxLength = 10;
	public static readonly int DefaultPercentageChance = 25;

	public SurveyTakerResponse(HtmlNode response)
	{
		DataPointName = response.GetAttributeValue("name", string.Empty);
		Type = response.GetAttributeValue("type", string.Empty);
		Value = response.GetAttributeValue("value", string.Empty);
		MaxLength = int.TryParse(response.GetAttributeValue("maxlength", string.Empty), out int maxLength) ? maxLength : DefaultMaxLength;

		EligibilityType = Enum.TryParse(response.GetAttributeValue("st-eligibilitytype", string.Empty), true, out SurveyTakerResponseEligibilityType surveyTakerResponseEligibilityType) ? surveyTakerResponseEligibilityType : SurveyTakerResponseEligibilityType.NotSet;
		PercentageChance = int.TryParse(response.GetAttributeValue("st-perc", string.Empty), out int percentageChance) ? percentageChance : DefaultPercentageChance;
		KrustyValue = response.GetAttributeValue("st-value", string.Empty);
		KrustyNotValue = response.GetAttributeValue("st-notvalue", string.Empty);
	}

	public string Value { get; set; }
	public string NameIndex { get; set; }
	public bool Exclusive { get; set; }
	public bool GroupExclusive { get; set; }
	public string GroupName { get; set; }
	public string VirtualGroupName { get; set; }
	public string Type { get; set; }
	public int MaxLength { get; set; }
	public SurveyTakerResponseTextType TextType { get; set; }
	public string DataPointName { get; set; }
	public SurveyTakerResponseEligibilityType EligibilityType { get; set; }
	public int PercentageChance { get; set; }
	public bool OtherSpecify { get; set; }
	public string KrustyValue { get; set; }
	public string KrustyNotValue { get; set; }
}

public class SurveyTakerResponseMvc : SurveyTakerResponse
{
	public SurveyTakerResponseMvc(HtmlNode response) : base(response)
	{
		NameIndex = response.GetAttributeValue("nameindex", string.Empty);
		Exclusive = bool.TryParse(response.GetAttributeValue("isexclusive", string.Empty), out bool exclusive) && exclusive;
		GroupExclusive = bool.TryParse(response.GetAttributeValue("isgroupexclusive", string.Empty), out bool isgroupexclusive) && isgroupexclusive;
		GroupName = response.GetAttributeValue("groupname", string.Empty);
		TextType = Enum.TryParse(response.GetAttributeValue("texttype", string.Empty), true, out SurveyTakerResponseTextType surveyTakerResponseTextType) ? surveyTakerResponseTextType : SurveyTakerResponseTextType.Unknown;
		OtherSpecify = DataPointName.IndexOf("other", StringComparison.OrdinalIgnoreCase) > -1;

		var statementGroup = response.Ancestors().FirstOrDefault(a => a.GetAttributeValue("grouptype", string.Empty).Equals("statement", StringComparison.OrdinalIgnoreCase));

		if (statementGroup != null)
			VirtualGroupName = statementGroup.GetAttributeValue("statementname", string.Empty);
		else
		{
			var responseGroup = response.Ancestors().FirstOrDefault(a => a.GetAttributeValue("grouptype", string.Empty).Equals("response", StringComparison.OrdinalIgnoreCase));

			if (responseGroup != null)
				VirtualGroupName = responseGroup.GetAttributeValue("responsename", string.Empty);
		}
	}
}

public class SurveyTakerResponseCore : SurveyTakerResponse
{
	public SurveyTakerResponseCore(HtmlNode response) : base(response)
	{
		NameIndex = response.GetAttributeValue("data-nameindex", string.Empty);
		Exclusive = bool.TryParse(response.GetAttributeValue("data-exclusive", string.Empty), out bool exclusive) && exclusive;
		GroupExclusive = bool.TryParse(response.GetAttributeValue("data-groupexclusive", string.Empty), out bool groupexclusive) && groupexclusive;
		GroupName = response.GetAttributeValue("data-groupname", string.Empty);
		TextType = Enum.TryParse(response.GetAttributeValue("data-texttype", string.Empty), true, out SurveyTakerResponseTextType surveyTakerResponseTextType) ? surveyTakerResponseTextType : SurveyTakerResponseTextType.Unknown;
		OtherSpecify = DataPointName.IndexOf("data-otherspecify", StringComparison.OrdinalIgnoreCase) > -1;

		VirtualGroupName = response
			.Ancestors()
			.FirstOrDefault(a => !string.IsNullOrEmpty(a.GetAttributeValue("data-grouptype", string.Empty)))
			?.GetAttributeValue("data-groupname", string.Empty) ?? string.Empty;
	}
}

public sealed class SurveyTakerInputResponseMvc : SurveyTakerResponseMvc
{
	public SurveyTakerInputResponseMvc(HtmlNode htmlNode) : base(htmlNode) { }
}

public sealed class SurveyTakerInputResponseCore : SurveyTakerResponseCore
{
	public SurveyTakerInputResponseCore(HtmlNode htmlNode) : base(htmlNode) { }
}

public sealed class SurveyTakerDropdownResponseMvc : SurveyTakerResponseMvc
{
	public SurveyTakerDropdownResponseMvc(HtmlNode htmlNode, HtmlNode optionHtmlNode) : base(htmlNode)
	{
		NameIndex = optionHtmlNode.Attributes["nameindex"]?.Value ?? string.Empty;
		Value = optionHtmlNode.Attributes["value"]?.Value ?? string.Empty;
		EligibilityType = Enum.TryParse(optionHtmlNode.GetAttributeValue("st-eligibilitytype", string.Empty), true, out SurveyTakerResponseEligibilityType surveyTakerResponseEligibilityType) ? surveyTakerResponseEligibilityType : SurveyTakerResponseEligibilityType.NotSet;
		Type = "dropdown";
	}
}

public sealed class SurveyTakerSliderResponseCore : SurveyTakerResponseCore
{
	public SurveyTakerSliderResponseCore(HtmlNode sliderNode, HtmlNode tickNode) : base(sliderNode)
	{
		Value = tickNode.Attributes["data-response-value"]?.Value ?? string.Empty;
		EligibilityType = Enum.TryParse(tickNode.GetAttributeValue("st-eligibilitytype", string.Empty), true, out SurveyTakerResponseEligibilityType surveyTakerResponseEligibilityType) ? surveyTakerResponseEligibilityType : SurveyTakerResponseEligibilityType.NotSet;
		Type = "slider";
	}

	public SurveyTakerSliderResponseCore(HtmlNode sliderNode) : base(sliderNode)
	{
		DataPointName = sliderNode.Attributes["data-inputid"].Value;
		Type = "range";
	}
}

public sealed class SurveyTakerDropdownResponseCore : SurveyTakerResponseCore
{
	public SurveyTakerDropdownResponseCore(HtmlNode htmlNode, HtmlNode optionHtmlNode) : base(htmlNode)
	{
		NameIndex = optionHtmlNode.Attributes["data-nameindex"]?.Value ?? string.Empty;
		Value = optionHtmlNode.Attributes["value"]?.Value ?? string.Empty;
		EligibilityType = Enum.TryParse(optionHtmlNode.GetAttributeValue("st-eligibilitytype", string.Empty), true, out SurveyTakerResponseEligibilityType surveyTakerResponseEligibilityType) ? surveyTakerResponseEligibilityType : SurveyTakerResponseEligibilityType.NotSet;

		Type = "dropdown";
	}
}

public sealed class SurveyTakerTextareaResponseMvc : SurveyTakerResponseMvc
{
	public SurveyTakerTextareaResponseMvc(HtmlNode htmlNode) : base(htmlNode)
	{
		Type = "textarea";
	}
}

public sealed class SurveyTakerTextareaResponseCore : SurveyTakerResponseCore
{
	public SurveyTakerTextareaResponseCore(HtmlNode htmlNode) : base(htmlNode)
	{
		Type = "textarea";
	}
}

public sealed class SurveyTakerPriceSensitvityResponseCore : SurveyTakerResponseCore
{
	public SurveyTakerPriceSensitvityResponseCore(HtmlNode htmlNode) : base(htmlNode)
	{
		Type = "pricesensitivity";
		NameIndex = htmlNode.GetAttributeValue("data-index", string.Empty);
		Value = htmlNode.GetAttributeValue("data-price", string.Empty);
	}
}

public enum SurveyTakerResponseEligibilityType
{
	NotSet,
	Eligible,
	Ineligible
}

public enum SurveyTakerResponseTextType
{
	Unknown,
	Numeric,
	Decimal,
	AlphaNumeric
}

public class SurveyTakerJobData
{
	public int EnvironmentID { get; set; }

	// The naming was kept this way to have backwards compatibility.
	public int Status { get; set; }

	public SurveyTakerJobStatus SurveyTakerJobStatus
	{
		get => (SurveyTakerJobStatus)Status;
		set => Status = (int)value;
	}

	public int CompletedRuns { get; set; }

	public int CurrentCompletes { get; set; }

	public int ExpectedCompletes { get; set; }

	public int SecondsRemaining { get; set; }

	public int SurveyID { get; set; }

	public string SurveyUrl { get; set; }

	public string SurveySampleSourceName { get; set; }

	public string Message { get; set; }

	public int DataPlanID { get; set; }

	public string HangfireJobID { get; set; }
}

public enum SurveyTakerJobStatus
{
	NotSet = 0,
	Pending = 1,
	Processing = 2,
	Failed = 3,
	Killed = 4,
	Complete = 5
}