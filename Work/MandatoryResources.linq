<Query Kind="Program">
  <Reference Relative="..\..\repos\zen\Zen.DataModels\bin\Azure-Beta-Debug\Zen.DataModels.dll">C:\source\repos\zen\Zen.DataModels\bin\Azure-Beta-Debug\Zen.DataModels.dll</Reference>
  <Reference Relative="..\..\repos\zen\Zen.Utilities\bin\Azure-Beta-Debug\Zen.Utilities.dll">C:\source\repos\zen\Zen.Utilities\bin\Azure-Beta-Debug\Zen.Utilities.dll</Reference>
  <NuGetReference>FluentAssertions</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Zen.DataModels</Namespace>
  <Namespace>Zen.Utilities</Namespace>
  <Namespace>FluentAssertions</Namespace>
  <Namespace>Zen.Utilities.Attributes</Namespace>
</Query>

void Main()
{
	var topLevelErrors = new List<string> {
		"GRIDNUMERICMISSINGOTHER", "VERBATIMNOANSWER", "CHECKONENOANSWER", "CHECKONEMISSINGOTHER", "CHECKALLNOANSWER", "CHECKALLMISSINGOTHER", "CHECKALLCONFLICTINGANSWERS",
		"GRIDCHECKALLNOANSWER", "GRIDCHECKALLCONFLICTINGANSWERS", "GRIDCHECKALLMISSINGOTHER", "GRIDCHECKONENOANSWER", "GRIDCHECKONENONNUMERIC", "GRIDCHECKONEMISSINGOTHER",
		"STOPNOANSWER", "TEXTNOANSWER", "TEXTNONNUMERIC", "GRIDNUMERICNOANSWER", "NUMERICMIN", "GRIDNUMERICMIN", "NUMERICMAX", "GRIDNUMERICMAX", "NUMERICMINMAX", "GRIDNUMERICMINMAX"
	};
	var newSpecificErrors = new List<string> { "NOANSWER", "MISSINGOTHER", "NONNUMERIC", "NUMERICMIN2", "NUMERICMINMAX2", "NUMERICMAX2", "CONFLICTINGANSWERS" };
	var q = new SurveyQuestion();
	for (int genType = 2; genType <= 3; genType++)
	{
		foreach (SurveyQuestionType questionType in Enum.GetValues(typeof(SurveyQuestionType)))
		{
			var validGenerationTypesQ = StringEnum.GetStringValue(questionType).Split(':')[0].Split(',');
			if (!validGenerationTypesQ.Contains(genType.ToString()))
				continue;

			foreach (SurveyQuestionDisplayType displayType in Enum.GetValues(typeof(SurveyQuestionDisplayType)))
			{
				//StringEnum.GetStringValue(displayType).Dump();
				var svParts = StringEnum.GetStringValue(displayType).Split(':');
				var validGenerationTypesD = svParts[0].Split(',');
				var validQuestionTypes = svParts[1].Split(',');
				if (!validGenerationTypesD.Contains(genType.ToString()) || !validQuestionTypes.Contains(((int)questionType).ToString()) || displayType == SurveyQuestionDisplayType.NONE)
					continue;
				$"{(SurveyGenerationType)genType} - {StringEnum.GetStringValue(questionType).Split(':')[1]} - {svParts[2]}".Dump();
				q.SurveyQuestionType = questionType;
				q.SurveyQuestionDisplayType = displayType;
				var resourcesOld = GetMandatoryResourceTypesBySurveyQuestionOld(q);
				resourcesOld.Sort();
				var resourcesNew = GetMandatoryResourceTypesBySurveyQuestionNew(q);
				if (genType == 2)
				{
					resourcesNew.AddRange(GetMandatoryResourceTopLevelErrors(q));
					resourcesNew.Sort();
					resourcesNew.Should().Equal(resourcesOld);
					resourcesNew.Should().NotIntersectWith(newSpecificErrors);
				}
				else
				{
					resourcesNew.AddRange(GetMandatoryResourceSpecificErrors(q));
					resourcesNew.Sort();
					resourcesNew.Should().NotIntersectWith(topLevelErrors);
					resourcesNew.Except(newSpecificErrors).Should().Equal(resourcesOld.Except(topLevelErrors));
				}
			}
		}
	}
}

// You can define other methods, fields, classes and namespaces here

public List<string> GetMandatoryResourceTypesBySurveyQuestionOld(SurveyQuestion surveyQuestion)
{
	var mandatoryResourceTypes = new List<string>();
	switch (surveyQuestion.SurveyQuestionType)
	{
		case SurveyQuestionType.CheckOne:
			{
				#region CheckOne

				mandatoryResourceTypes.Add("CHECKONENOANSWER");
				mandatoryResourceTypes.Add("CHECKONEMISSINGOTHER");

				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.DROPDOWN)
				{
					mandatoryResourceTypes.Add("CHECKONEDROPDOWNDEFAULT");
				}

				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.SEMANTIC)
				{
					mandatoryResourceTypes.Add("SEMANTICMIDDLESCALEPOINTTEXT");
				}

				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.STARRATING)
				{
					mandatoryResourceTypes.Add("LEGENDLEFT");
					mandatoryResourceTypes.Add("LEGENDMIDDLE");
					mandatoryResourceTypes.Add("LEGENDRIGHT");
				}

				mandatoryResourceTypes.Add("OTHERSRLABEL");

				break;

				#endregion
			}
		case SurveyQuestionType.CheckAll:
			{
				#region CheckAll

				mandatoryResourceTypes.Add("CHECKALLNOANSWER");
				mandatoryResourceTypes.Add("CHECKALLMISSINGOTHER");
				mandatoryResourceTypes.Add("CHECKALLCONFLICTINGANSWERS");
				mandatoryResourceTypes.Add("CHECKALLSELECTEDCOUNT");
				mandatoryResourceTypes.Add("CHECKALLSELECTEDCOUNTRANGE");
				mandatoryResourceTypes.Add("CHECKALLTOTALREQUIREDVALUE");
				mandatoryResourceTypes.Add("CHECKALLTOTALREQUIREDVALUERANGE");
				mandatoryResourceTypes.Add("GROUPSELECTEDRESPONSECOUNT");

				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.HORIZMARKETBASKET1 || surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.HORIZMARKETBASKET2 || surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.VERTICALMARKETBASKET1 || surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.VERTICALMARKETBASKET2 || surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.MARKETBASKET)
				{
					mandatoryResourceTypes.Add("CHECKALLTOTALTEXT");
					mandatoryResourceTypes.Add("CHECKALLREMAININGTEXT");
					mandatoryResourceTypes.Add("CHECKALLUNUSEDATTRIBUTES");
					mandatoryResourceTypes.Add("CHECKALLUSEDATTRIBUTES");
				}

				mandatoryResourceTypes.Add("OTHERSRLABEL");

				break;

				#endregion
			}
		case SurveyQuestionType.Verbatim:
			{
				#region	Verbatim

				mandatoryResourceTypes.Add("VERBATIMNOANSWER");
				mandatoryResourceTypes.Add("VERBATIMDEFAULT");
				mandatoryResourceTypes.Add("VERBATIMTRAFFICLIGHTRED");
				mandatoryResourceTypes.Add("VERBATIMTRAFFICLIGHTYELLOW");
				mandatoryResourceTypes.Add("VERBATIMTRAFFICLIGHTGREEN");
				mandatoryResourceTypes.Add("OPTOUTTEXT");
				mandatoryResourceTypes.Add("OTHERSRLABEL");

				switch (surveyQuestion.SurveyQuestionDisplayType)
				{
					case SurveyQuestionDisplayType.VERBHIGHLIGHT:
						mandatoryResourceTypes.Add("VERB_HIGHLIGHT_DELIMETER");
						mandatoryResourceTypes.Add("VERB_HIGHLIGHT_BUTTON_0");
						mandatoryResourceTypes.Add("VERB_HIGHLIGHT_BUTTON_1");
						mandatoryResourceTypes.Add("VERB_HIGHLIGHT_BUTTON_2");
						mandatoryResourceTypes.Add("VERB_HIGHLIGHT_BUTTON_3");
						mandatoryResourceTypes.Add("VERB_HIGHLIGHT_BUTTON_4");
						mandatoryResourceTypes.Add("VERB_HIGHLIGHT_BUTTON_5");
						mandatoryResourceTypes.Add("VERB_HIGHLIGHT_BUTTON_6");
						mandatoryResourceTypes.Add("VERBATIMHIGHLIGHTINGNOANSWER");
						break;
					case SurveyQuestionDisplayType.IMAGEHIGHLIGHT:
						mandatoryResourceTypes.Add("VERBATIMREQUIREDNUMBEROFCLICKSVALUE");
						mandatoryResourceTypes.Add("VERBATIMREQUIREDNUMBEROFCLICKSRANGE");
						break;
					case SurveyQuestionDisplayType.MEDIAUPLOAD:
						mandatoryResourceTypes.Add("VERBATIMREQUIREDNUMBEROFFILESVALUE");
						mandatoryResourceTypes.Add("VERBATIMREQUIREDNUMBEROFFILESRANGE");
						break;
					case SurveyQuestionDisplayType.VIDEOCAPTURE:
						mandatoryResourceTypes.Add("VERBATIMWARNINGGONEXT");
						mandatoryResourceTypes.Add("VERBATIMALREADYRECORDED");
						mandatoryResourceTypes.Add("VERBATIMREACHEDTIMELIMIT");
						mandatoryResourceTypes.Add("INSTRUCTIONSTEXT_VIDEOCAPTURE");
						mandatoryResourceTypes.Add("VIDEOCAPTURE_FALLBACK");
						break;
					default:
						break;
				}

				break;

				#endregion
			}
		case SurveyQuestionType.Numeric:
			{
				#region Numeric

				mandatoryResourceTypes.Add("TEXTNOANSWER");
				mandatoryResourceTypes.Add("TEXTNONNUMERIC");
				mandatoryResourceTypes.Add("NUMERICBEFORETEXT");
				mandatoryResourceTypes.Add("NUMERICAFTERTEXT");
				mandatoryResourceTypes.Add("NUMERICMIN");
				mandatoryResourceTypes.Add("NUMERICMAX");
				mandatoryResourceTypes.Add("NUMERICMINMAX");
				mandatoryResourceTypes.Add("OTHERSRLABEL");
				mandatoryResourceTypes.Add("OPTOUTTEXT");

				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.BUTTONRATING)
				{
					mandatoryResourceTypes.Add("BUTTONRATINGSCALELEGEND");
					mandatoryResourceTypes.Add("LEGENDLEFT");
					mandatoryResourceTypes.Add("LEGENDMIDDLE");
					mandatoryResourceTypes.Add("LEGENDRIGHT");
				}

				break;

				#endregion
			}
		case SurveyQuestionType.GridCheckOne:
			{
				#region GridCheckOne

				mandatoryResourceTypes.Add("GRIDCHECKONENOANSWER");
				mandatoryResourceTypes.Add("GRIDCHECKONENONNUMERIC");
				mandatoryResourceTypes.Add("GRIDCHECKONEMISSINGOTHER");

				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.DROPDOWN)
				{
					mandatoryResourceTypes.Add("GRIDCHECKONEDROPDOWNDEFAULT");
				}

				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.SEMANTIC)
				{
					mandatoryResourceTypes.Add("SEMANTICMIDDLESCALEPOINTTEXT");
				}

				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.DRAGDROP)
				{
					mandatoryResourceTypes.Add("DRAGDROPNOITEMSREMAINING");
				}

				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.STARRATING)
				{
					mandatoryResourceTypes.Add("LEGENDLEFT");
					mandatoryResourceTypes.Add("LEGENDMIDDLE");
					mandatoryResourceTypes.Add("LEGENDRIGHT");
				}

				mandatoryResourceTypes.Add("OTHERSRLABEL");

				break;

				#endregion
			}
		case SurveyQuestionType.GridCheckAll:
			{
				#region GridCheckAll

				mandatoryResourceTypes.Add("GRIDCHECKALLNOANSWER");
				mandatoryResourceTypes.Add("GRIDCHECKALLCONFLICTINGANSWERS");
				mandatoryResourceTypes.Add("GRIDCHECKALLMISSINGOTHER");
				mandatoryResourceTypes.Add("GRIDCHECKALLSELECTEDCOUNT");
				mandatoryResourceTypes.Add("GRIDCHECKALLSELECTEDCOUNTRANGE");
				mandatoryResourceTypes.Add("GROUPSELECTEDRESPONSECOUNT");
				mandatoryResourceTypes.Add("OTHERSRLABEL");

				break;

				#endregion
			}
		case SurveyQuestionType.GridNumeric:
			{
				#region GridNumeric

				mandatoryResourceTypes.Add("GRIDNUMERICNOANSWER");
				mandatoryResourceTypes.Add("GRIDNUMERICMISSINGOTHER");
				mandatoryResourceTypes.Add("NUMERICBEFORETEXT");
				mandatoryResourceTypes.Add("NUMERICAFTERTEXT");
				mandatoryResourceTypes.Add("GRIDNUMERICMIN");
				mandatoryResourceTypes.Add("GRIDNUMERICMAX");
				mandatoryResourceTypes.Add("GRIDNUMERICMINMAX");
				mandatoryResourceTypes.Add("OPTOUTTEXT");
				mandatoryResourceTypes.Add("OTHERSRLABEL");

				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.CONSTANTSUM)
				{
					mandatoryResourceTypes.Add("GRIDNUMERICCONSTANTSUMTOTAL");
					mandatoryResourceTypes.Add("GRIDNUMERICCONSTANTSUMUSED");
					mandatoryResourceTypes.Add("GRIDNUMERICCONSTANTSUMREMAINING");
					mandatoryResourceTypes.Add("GRIDNUMERICCONSTANTSUMWRONGTOTAL");
				}
				else if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.PRESSURETASK)
				{
					mandatoryResourceTypes.Add("PRESSURETASKBEGINTEXT");
					mandatoryResourceTypes.Add("PRESSURETASKENDTEXT");
				}

				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.BUTTONRATING)
				{
					mandatoryResourceTypes.Add("LEGENDLEFT");
					mandatoryResourceTypes.Add("LEGENDMIDDLE");
					mandatoryResourceTypes.Add("LEGENDRIGHT");
				}

				break;

				#endregion
			}
		case SurveyQuestionType.Stop:
			{
				#region Stop

				mandatoryResourceTypes.Add("STOPNOANSWER");
				mandatoryResourceTypes.Add("STOPINVALIDCHECK");

				break;

				#endregion
			}
		case SurveyQuestionType.Information:
			{
				#region Information

				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.STOP)
				{
					mandatoryResourceTypes.Add("STOPNOANSWER");
					mandatoryResourceTypes.Add("STOPINVALIDCHECK");
				}
				else if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.TITLE)
				{
					mandatoryResourceTypes.Add("DEFAULTINFORMATIONTITLETEXT");
				}
				break;

				#endregion
			}
		case SurveyQuestionType.Ranking:
			{
				mandatoryResourceTypes.Add("RANKINGMINIMUMRANKSREQUIRED");
				mandatoryResourceTypes.Add("RANKINGRANKSOUTOFORDER");
				mandatoryResourceTypes.Add("OPTOUTTEXT");
				break;
			}
	}

	if (surveyQuestion.SurveyQuestionDisplayType != SurveyQuestionDisplayType.VIDEOCAPTURE)
	{
		mandatoryResourceTypes.Add("INSTRUCTIONSTEXT");
	}

	return mandatoryResourceTypes;
}


public List<string> GetMandatoryResourceTypesBySurveyQuestionNew(SurveyQuestion surveyQuestion)
{
	var mandatoryResourceTypes = new List<string>();
	switch (surveyQuestion.SurveyQuestionType)
	{
		case SurveyQuestionType.CheckOne:
			{
				#region CheckOne
				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.DROPDOWN)
				{
					mandatoryResourceTypes.Add("CHECKONEDROPDOWNDEFAULT");
				}

				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.SEMANTIC)
				{
					mandatoryResourceTypes.Add("SEMANTICMIDDLESCALEPOINTTEXT");
				}

				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.STARRATING)
				{
					mandatoryResourceTypes.Add("LEGENDLEFT");
					mandatoryResourceTypes.Add("LEGENDMIDDLE");
					mandatoryResourceTypes.Add("LEGENDRIGHT");
				}

				mandatoryResourceTypes.Add("OTHERSRLABEL");

				break;

				#endregion
			}
		case SurveyQuestionType.CheckAll:
			{
				#region CheckAll
				mandatoryResourceTypes.Add("CHECKALLSELECTEDCOUNT");
				mandatoryResourceTypes.Add("CHECKALLSELECTEDCOUNTRANGE");
				mandatoryResourceTypes.Add("CHECKALLTOTALREQUIREDVALUE");
				mandatoryResourceTypes.Add("CHECKALLTOTALREQUIREDVALUERANGE");
				mandatoryResourceTypes.Add("GROUPSELECTEDRESPONSECOUNT");

				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.HORIZMARKETBASKET1 || surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.HORIZMARKETBASKET2 || surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.VERTICALMARKETBASKET1 || surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.VERTICALMARKETBASKET2 || surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.MARKETBASKET)
				{
					mandatoryResourceTypes.Add("CHECKALLTOTALTEXT");
					mandatoryResourceTypes.Add("CHECKALLREMAININGTEXT");
					mandatoryResourceTypes.Add("CHECKALLUNUSEDATTRIBUTES");
					mandatoryResourceTypes.Add("CHECKALLUSEDATTRIBUTES");
				}

				mandatoryResourceTypes.Add("OTHERSRLABEL");

				break;

				#endregion
			}
		case SurveyQuestionType.Verbatim:
			{
				#region	Verbatim

				mandatoryResourceTypes.Add("VERBATIMDEFAULT");
				mandatoryResourceTypes.Add("VERBATIMTRAFFICLIGHTRED");
				mandatoryResourceTypes.Add("VERBATIMTRAFFICLIGHTYELLOW");
				mandatoryResourceTypes.Add("VERBATIMTRAFFICLIGHTGREEN");
				mandatoryResourceTypes.Add("OPTOUTTEXT");
				mandatoryResourceTypes.Add("OTHERSRLABEL");

				switch (surveyQuestion.SurveyQuestionDisplayType)
				{
					case SurveyQuestionDisplayType.VERBHIGHLIGHT:
						mandatoryResourceTypes.Add("VERB_HIGHLIGHT_DELIMETER");
						mandatoryResourceTypes.Add("VERB_HIGHLIGHT_BUTTON_0");
						mandatoryResourceTypes.Add("VERB_HIGHLIGHT_BUTTON_1");
						mandatoryResourceTypes.Add("VERB_HIGHLIGHT_BUTTON_2");
						mandatoryResourceTypes.Add("VERB_HIGHLIGHT_BUTTON_3");
						mandatoryResourceTypes.Add("VERB_HIGHLIGHT_BUTTON_4");
						mandatoryResourceTypes.Add("VERB_HIGHLIGHT_BUTTON_5");
						mandatoryResourceTypes.Add("VERB_HIGHLIGHT_BUTTON_6");
						mandatoryResourceTypes.Add("VERBATIMHIGHLIGHTINGNOANSWER");
						break;
					case SurveyQuestionDisplayType.IMAGEHIGHLIGHT:
						mandatoryResourceTypes.Add("VERBATIMREQUIREDNUMBEROFCLICKSVALUE");
						mandatoryResourceTypes.Add("VERBATIMREQUIREDNUMBEROFCLICKSRANGE");
						break;
					case SurveyQuestionDisplayType.MEDIAUPLOAD:
						mandatoryResourceTypes.Add("VERBATIMREQUIREDNUMBEROFFILESVALUE");
						mandatoryResourceTypes.Add("VERBATIMREQUIREDNUMBEROFFILESRANGE");
						break;
					case SurveyQuestionDisplayType.VIDEOCAPTURE:
						mandatoryResourceTypes.Add("VERBATIMWARNINGGONEXT");
						mandatoryResourceTypes.Add("VERBATIMALREADYRECORDED");
						mandatoryResourceTypes.Add("VERBATIMREACHEDTIMELIMIT");
						mandatoryResourceTypes.Add("INSTRUCTIONSTEXT_VIDEOCAPTURE");
						mandatoryResourceTypes.Add("VIDEOCAPTURE_FALLBACK");
						break;
					default:
						break;
				}

				break;

				#endregion
			}
		case SurveyQuestionType.Numeric:
			{
				#region Numeric
				mandatoryResourceTypes.Add("NUMERICBEFORETEXT");
				mandatoryResourceTypes.Add("NUMERICAFTERTEXT");
				mandatoryResourceTypes.Add("OTHERSRLABEL");
				mandatoryResourceTypes.Add("OPTOUTTEXT");

				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.BUTTONRATING)
				{
					mandatoryResourceTypes.Add("BUTTONRATINGSCALELEGEND");
					mandatoryResourceTypes.Add("LEGENDLEFT");
					mandatoryResourceTypes.Add("LEGENDMIDDLE");
					mandatoryResourceTypes.Add("LEGENDRIGHT");
				}

				break;

				#endregion
			}
		case SurveyQuestionType.GridCheckOne:
			{
				#region GridCheckOne

				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.DROPDOWN)
				{
					mandatoryResourceTypes.Add("GRIDCHECKONEDROPDOWNDEFAULT");
				}

				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.SEMANTIC)
				{
					mandatoryResourceTypes.Add("SEMANTICMIDDLESCALEPOINTTEXT");
				}

				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.DRAGDROP)
				{
					mandatoryResourceTypes.Add("DRAGDROPNOITEMSREMAINING");
				}

				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.STARRATING)
				{
					mandatoryResourceTypes.Add("LEGENDLEFT");
					mandatoryResourceTypes.Add("LEGENDMIDDLE");
					mandatoryResourceTypes.Add("LEGENDRIGHT");
				}

				mandatoryResourceTypes.Add("OTHERSRLABEL");

				break;

				#endregion
			}
		case SurveyQuestionType.GridCheckAll:
			{
				#region GridCheckAll
				mandatoryResourceTypes.Add("GRIDCHECKALLSELECTEDCOUNT");
				mandatoryResourceTypes.Add("GRIDCHECKALLSELECTEDCOUNTRANGE");
				mandatoryResourceTypes.Add("GROUPSELECTEDRESPONSECOUNT");
				mandatoryResourceTypes.Add("OTHERSRLABEL");

				break;

				#endregion
			}
		case SurveyQuestionType.GridNumeric:
			{
				#region GridNumeric
				mandatoryResourceTypes.Add("NUMERICBEFORETEXT");
				mandatoryResourceTypes.Add("NUMERICAFTERTEXT");
				mandatoryResourceTypes.Add("OPTOUTTEXT");
				mandatoryResourceTypes.Add("OTHERSRLABEL");

				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.CONSTANTSUM)
				{
					mandatoryResourceTypes.Add("GRIDNUMERICCONSTANTSUMTOTAL");
					mandatoryResourceTypes.Add("GRIDNUMERICCONSTANTSUMUSED");
					mandatoryResourceTypes.Add("GRIDNUMERICCONSTANTSUMREMAINING");
					mandatoryResourceTypes.Add("GRIDNUMERICCONSTANTSUMWRONGTOTAL");
				}
				else if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.PRESSURETASK)
				{
					mandatoryResourceTypes.Add("PRESSURETASKBEGINTEXT");
					mandatoryResourceTypes.Add("PRESSURETASKENDTEXT");
				}

				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.BUTTONRATING)
				{
					mandatoryResourceTypes.Add("LEGENDLEFT");
					mandatoryResourceTypes.Add("LEGENDMIDDLE");
					mandatoryResourceTypes.Add("LEGENDRIGHT");
				}

				break;

				#endregion
			}
		case SurveyQuestionType.Stop:
			{
				#region Stop
				mandatoryResourceTypes.Add("STOPINVALIDCHECK");
				break;

				#endregion
			}
		case SurveyQuestionType.Information:
			{
				#region Information

				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.STOP)
				{
					mandatoryResourceTypes.Add("STOPINVALIDCHECK");
				}
				else if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.TITLE)
				{
					mandatoryResourceTypes.Add("DEFAULTINFORMATIONTITLETEXT");
				}
				break;

				#endregion
			}
		case SurveyQuestionType.Ranking:
			{
				mandatoryResourceTypes.Add("RANKINGMINIMUMRANKSREQUIRED");
				mandatoryResourceTypes.Add("RANKINGRANKSOUTOFORDER");
				mandatoryResourceTypes.Add("OPTOUTTEXT");
				break;
			}
	}

	if (surveyQuestion.SurveyQuestionDisplayType != SurveyQuestionDisplayType.VIDEOCAPTURE)
	{
		mandatoryResourceTypes.Add("INSTRUCTIONSTEXT");
	}

	return mandatoryResourceTypes;
}

public List<string> GetMandatoryResourceTopLevelErrors(SurveyQuestion surveyQuestion)
{
	var mandatoryResourceTypes = new List<string>();
	switch (surveyQuestion.SurveyQuestionType)
	{
		case SurveyQuestionType.CheckOne:
			{
				mandatoryResourceTypes.Add("CHECKONENOANSWER");
				mandatoryResourceTypes.Add("CHECKONEMISSINGOTHER");
				break;
			}
		case SurveyQuestionType.CheckAll:
			{
				mandatoryResourceTypes.Add("CHECKALLNOANSWER");
				mandatoryResourceTypes.Add("CHECKALLMISSINGOTHER");
				mandatoryResourceTypes.Add("CHECKALLCONFLICTINGANSWERS");
				break;
			}
		case SurveyQuestionType.Verbatim:
			{
				mandatoryResourceTypes.Add("VERBATIMNOANSWER");
				break;
			}
		case SurveyQuestionType.Numeric:
			{
				mandatoryResourceTypes.Add("TEXTNOANSWER");
				mandatoryResourceTypes.Add("TEXTNONNUMERIC");
				mandatoryResourceTypes.Add("NUMERICMIN");
				mandatoryResourceTypes.Add("NUMERICMAX");
				mandatoryResourceTypes.Add("NUMERICMINMAX");
				break;
			}
		case SurveyQuestionType.GridCheckOne:
			{
				mandatoryResourceTypes.Add("GRIDCHECKONENOANSWER");
				mandatoryResourceTypes.Add("GRIDCHECKONENONNUMERIC");
				mandatoryResourceTypes.Add("GRIDCHECKONEMISSINGOTHER");
				break;
			}
		case SurveyQuestionType.GridCheckAll:
			{
				mandatoryResourceTypes.Add("GRIDCHECKALLNOANSWER");
				mandatoryResourceTypes.Add("GRIDCHECKALLCONFLICTINGANSWERS");
				mandatoryResourceTypes.Add("GRIDCHECKALLMISSINGOTHER");
				break;
			}
		case SurveyQuestionType.GridNumeric:
			{
				mandatoryResourceTypes.Add("GRIDNUMERICNOANSWER");
				mandatoryResourceTypes.Add("GRIDNUMERICMISSINGOTHER");
				mandatoryResourceTypes.Add("GRIDNUMERICMIN");
				mandatoryResourceTypes.Add("GRIDNUMERICMAX");
				mandatoryResourceTypes.Add("GRIDNUMERICMINMAX");
				break;
			}
		case SurveyQuestionType.Stop:
			{
				mandatoryResourceTypes.Add("STOPNOANSWER");
				break;
			}
		case SurveyQuestionType.Information:
			{
				if (surveyQuestion.SurveyQuestionDisplayType == SurveyQuestionDisplayType.STOP)
				{
					mandatoryResourceTypes.Add("STOPNOANSWER");
				}
				break;
			}
	}
	return mandatoryResourceTypes;
}
public List<string> GetMandatoryResourceSpecificErrors(SurveyQuestion surveyQuestion)
{
	var mandatoryResourceTypes = new List<string>();
	switch (surveyQuestion.SurveyQuestionType)
	{
		case SurveyQuestionType.CheckOne:
			{
				mandatoryResourceTypes.Add("NOANSWER");
				mandatoryResourceTypes.Add("MISSINGOTHER");
				break;
			}
		case SurveyQuestionType.CheckAll:
		case SurveyQuestionType.GridCheckAll:
			{
				mandatoryResourceTypes.Add("NOANSWER");
				mandatoryResourceTypes.Add("MISSINGOTHER");
				mandatoryResourceTypes.Add("CONFLICTINGANSWERS");
				break;
			}
		case SurveyQuestionType.Numeric:
			{
				mandatoryResourceTypes.Add("NOANSWER");
				mandatoryResourceTypes.Add("NONNUMERIC");
				mandatoryResourceTypes.Add("NUMERICMIN2");
				mandatoryResourceTypes.Add("NUMERICMAX2");
				mandatoryResourceTypes.Add("NUMERICMINMAX2");
				break;
			}
		case SurveyQuestionType.GridCheckOne:
			{
				mandatoryResourceTypes.Add("NOANSWER");
				mandatoryResourceTypes.Add("MISSINGOTHER");
				mandatoryResourceTypes.Add("NONNUMERIC");
				break;
			}
		case SurveyQuestionType.GridNumeric:
			{
				mandatoryResourceTypes.Add("NOANSWER");
				mandatoryResourceTypes.Add("MISSINGOTHER");
				mandatoryResourceTypes.Add("NUMERICMIN2");
				mandatoryResourceTypes.Add("NUMERICMAX2");
				mandatoryResourceTypes.Add("NUMERICMINMAX2");
				break;
			}
		case SurveyQuestionType.Information:
		case SurveyQuestionType.Verbatim:
		case SurveyQuestionType.Stop:
			{
				mandatoryResourceTypes.Add("NOANSWER");
				break;
			}
	}
	return mandatoryResourceTypes;
}