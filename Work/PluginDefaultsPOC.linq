<Query Kind="Program">
  <Reference Relative="..\..\repos\zen\Zen.DataModels\bin\Azure-Beta-Debug\Zen.DataModels.dll">C:\source\repos\zen\Zen.DataModels\bin\Azure-Beta-Debug\Zen.DataModels.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Zen.DataModels</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	var qAttrs = new
	{
		sqgw = "160px"
	};
	var responses = new List<SurveyResponse>
	{
		new SurveyResponse {
			Value = "1",
			DisplayOrder = 1,
			NameIndex = 1,
			SurveyResponseTexts = new List<SurveyResponseText>
			{
				new SurveyResponseText{
					LanguageID = 9,
					Text = "Far apart"
				}
			}
		},
		new SurveyResponse {
			Value = "2",
			DisplayOrder = 2,
			NameIndex = 2,
			SurveyResponseTexts = new List<SurveyResponseText>
			{
				new SurveyResponseText{
					LanguageID = 9,
					Text = "Close but different"
				}
			}
		},
		new SurveyResponse {
			Value = "3",
			DisplayOrder = 3,
			NameIndex = 3,
			SurveyResponseTexts = new List<SurveyResponseText>
			{
				new SurveyResponseText{
					LanguageID = 9,
					Text = "A very small overlap"
				}
			}
		},
		new SurveyResponse {
			Value = "4",
			DisplayOrder = 4,
			NameIndex = 4,
			SurveyResponseTexts = new List<SurveyResponseText>
			{
				new SurveyResponseText{
					LanguageID = 9,
					Text = "A small overlap"
				}
			}
		},
		new SurveyResponse {
			Value = "5",
			DisplayOrder = 5,
			NameIndex = 5,
			SurveyResponseTexts = new List<SurveyResponseText>
			{
				new SurveyResponseText{
					LanguageID = 9,
					Text = "A moderate overlap"
				}
			}
		},
		new SurveyResponse {
			Value = "6",
			DisplayOrder = 6,
			NameIndex = 6,
			SurveyResponseTexts = new List<SurveyResponseText>
			{
				new SurveyResponseText{
					LanguageID = 9,
					Text = "A large overlap"
				}
			}
		},
		new SurveyResponse {
			Value = "7",
			DisplayOrder = 7,
			NameIndex = 7,
			SurveyResponseTexts = new List<SurveyResponseText>
			{
				new SurveyResponseText{
					LanguageID = 9,
					Text = "A very large overlap"
				}
			}
		},
		new SurveyResponse {
			Value = "8",
			DisplayOrder = 8,
			NameIndex = 8,
			SurveyResponseTexts = new List<SurveyResponseText>
			{
				new SurveyResponseText{
					LanguageID = 9,
					Text = "A total overlap"
				}
			}
		}
	};
	var pluginDefaults = new Dictionary<string, (object questionAttributes, List<SurveyResponse> defaultResponses, List<SurveyStatement> statements)>();
	pluginDefaults.Add("BRANDFIT", (qAttrs, responses, new List<SurveyStatement>()));
	var b = JsonConvert.SerializeObject(pluginDefaults);
	var c = JsonConvert.DeserializeObject<Dictionary<string, (object questionAttributes, List<SurveyResponse> defaultResponses, List<SurveyStatement> statements)>>(b);
	c["BRANDFIT"].questionAttributes.Dump();
}

// You can define other methods, fields, classes and namespaces here