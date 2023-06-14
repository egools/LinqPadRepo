<Query Kind="Program" />

void Main()
{
	foreach(var sv in surveyVals)
	{
		var ordered = historicVals.OrderBy(hv => LevenshteinDistance(sv.Item2, hv.Item2));
		var similar = ordered.First();
		if(LevenshteinDistance(sv.Item2, similar.Item2) / (double)Math.Max(sv.Item2.Length, similar.Item2.Length) < 0.25)
		{
			Console.WriteLine($"{sv.Item2} -> {similar.Item2} || Distance: {LevenshteinDistance(sv.Item2, similar.Item2)}");
			//Console.WriteLine($"{{ \"{sv.Item1}\", \"{similar.Item1}\"}}");
		}
	}
}

public static int LevenshteinDistance(string s, string t)
{
    int n = s.Length;
    int m = t.Length;
    int[,] distances = new int[n + 1, m + 1];

    if (n == 0)
        return m;
    if (m == 0)
        return n;

    for (int i = 0; i <= n; distances[i, 0] = i++);
    for (int j = 0; j <= m; distances[0, j] = j++);

    for (int i = 1; i <= n; i++)
    {
        for (int j = 1; j <= m; j++)
        {
            int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
            distances[i, j] = Math.Min(Math.Min(distances[i - 1, j] + 1, distances[i, j - 1] + 1), distances[i - 1, j - 1] + cost);
        }
    }
    return distances[n, m];
}

// Define other methods and classes here
public static List<(string, string)> surveyVals = new List<(string, string)>
{
	( "Q205_1", "Qualitative"),
	( "Q205_2", "Quantitative/Analytics"),
	( "Q205_3", "Insight Integration"),
	( "Q205_4", "Community/Panel"),
	( "Q205_5", "Workshop + Implementation"),
	( "Q205_6", "Data Tool Deliverables"),
	( "Q205_7", "Deliverables/Socialization"),
	( "Q205_11", "Change Management"),
	( "Q205_12", "Consumer Experience (CX/EX)"),
	( "Q205_13", "Innovation Studio/Human Centered Innovation"),
	( "Q205_8", "Other Planning + Activation Consulting"),
	( "Q205_9", "Retainer"),
	( "Q205_10", "Other"),
	( "Q210_1", "Co-Creation" ),
	( "Q210_2", "Concept Development/Product Design" ),
	( "Q210_3", "Consumer Immersion" ),
	( "Q210_4", "Emotional Connections" ),
	( "Q210_5", "Exploration" ),
	( "Q210_6", "Usability (product/website/etc.)" ),
	( "Q210_68", "Eye-Tracking" ),
	( "Q210_65", "IDI/Focus Groups" ),
	( "Q210_20", "Online Bulletin Board/Journal" ),
	( "Q210_66", "Discussion Topic (dialogue)" ),
	( "Q210_67", "In-home/In-Store Ethnographies" ),
	( "Q210_69", "Consumer/Shopper Safari" ),
	( "Q210_70", "Remesh" ),
	( "Q210_7", "AA&U" ),
	( "Q210_8", "Concept/Product/Brand/Positioning/Media or Ad Design/Evaluation/Testing" ),
	( "Q210_9", "Market Landscape/Market Mapping" ),
	( "Q210_10", "MediaAd Testing" ),
	( "Q210_11", "Need States" ),
	( "Q210_12", "Pre/Post Analysis" ),
	( "Q210_13", "Product Positioning" ),
	( "Q210_14", "Sponsorship ROI" ),
	( "Q210_15", "Tracking Study or Pre/Post Analysis" ),
	( "Q210_21", "Cluster Analysis (not full segmentation)" ),
	( "Q210_22", "Conjoint/Discrete Choice" ),
	( "Q210_23", "Data Fusion/Imputation" ),
	( "Q210_24", "Weighting" ),
	( "Q210_25", "Decision Tree Analysis (Chaid/CART)" ),
	( "Q210_26", "Delphi Method" ),
	( "Q210_27", "Discrete Choice (non-Conjoint)" ),
	( "Q210_28", "Drivers and Barriers/Key Drivers Analysis" ),
	( "Q210_29", "Factor Analysis" ),
	( "Q210_30", "Highlighter Analysis" ),
	( "Q210_31", "KANO" ),
	( "Q210_32", "Market Basket/Bundling" ),
	( "Q210_33", "Market Mapping" ),
	( "Q210_34", "Market Sizing" ),
	( "Q210_35", "Max Diff" ),
	( "Q210_36", "Opportunity Analysis" ),
	( "Q210_37", "Path-to-Purchase/Consumer Journey" ),
	( "Q210_38", "Perceptual Map" ),
	( "Q210_39", "Modeling/Forecasting (Predictive, Key Drivers, Etc.)" ),
	( "Q210_40", "Segmentation" ),
	( "Q210_41", "Sequential Preferences" ),
	( "Q210_42", "Text Mining" ),
	( "Q210_43", "TURF" ),
	( "Q210_44", "Van Westendorp or Sequential Preferences" ),
	( "Q210_45", "VOC (Voice of the Customer)" ),
	( "Q210_46", "Web Scraping" ),
	( "Q210_64", "Passive Metering" ),
	( "Q210_72", "Excel Simulator/Typing Tool" ),
	( "Q210_49", "Shapley Value Regression" ),
	( "Q210_77", "Stated vs. Derived Importance" ),
	( "Q210_79", "Quant Intercepts" ),
	( "Q210_16", "Insights Integration" ),
	( "Q210_17", "Flex/DIY Community/Panel" ),
	( "Q210_18", "Flex+ Community/Panel (not full-service, but more than DIY)" ),
	( "Q210_19", "Full Service Community/Panel" ),
	( "Q210_47", "Automation (solution to a manual task)" ),
	( "Q210_48", "Online Dashboard" ),
	( "Q210_50", "Online Tabs" ),
	( "Q210_51", "Online Portal" ),
	( "Q210_52", "Online Simulator" ),
	( "Q210_53", "Topline Report" ),
	( "Q210_54", "Standard Report" ),
	( "Q210_55", "Infographic" ),
	( "Q210_56", "Interactive Infographic/Portal" ),
	( "Q210_57", "Commercialization Deck" ),
	( "Q210_58", "Executive Communications" ),
	( "Q210_71", "Online Report (Dynamic Report)" ),
	( "Q210_78", "Immersive Space/Exhibit (Experiential Gallery)" ),
	( "Q210_73", "Video Deliverable" ),
	( "Q210_59", "Alignment Workshop" ),
	( "Q210_60", "Activation Workshop" ),
	( "Q210_61", "Ideation/Ignitor Workshop" ),
	( "Q210_80", "Immersion Workshop" ),
	( "Q210_62", "Consumer Experience (CX/EX)" ),
	( "Q210_63", "Change Management" ),
	( "Q210_74", "Consumer Village" ),
	( "Q210_75", "Innovation Studio/Human Centered Innovation" ),
	( "Q210_76", "Recruiting/Sample Management" ),
	( "Q210_99", "Other" )
};

public static List<(string, string)> historicVals = new List<(string, string)>
{
	( "ATTRIBUTE_1", "Single-Sourced" ),
	( "ATTRIBUTE_2", "Global" ),
	( "ATTRIBUTE_3", "Qualitative" ),
	( "ATTRIBUTE_4", "Quantitative/Analytics" ),
	( "ATTRIBUTE_5", "InsightIntegration" ),
	( "ATTRIBUTE_6", "Community/Panel" ),
	( "ATTRIBUTE_7", "Workshop+Implementation" ),
	( "ATTRIBUTE_8", "DataToolDeliverables" ),
	( "ATTRIBUTE_9", "Deliverables/Socialization" ),
	( "ATTRIBUTE_10", "ChangeManagement" ),
	( "ATTRIBUTE_11", "ConsumerExperience CX/EX" ),
	( "ATTRIBUTE_12", "InnovationStudio/HumanCenteredInnovation" ),
	( "ATTRIBUTE_13", "OtherPlanning+ActivationConsulting" ),
	( "ATTRIBUTE_14", "Retainer" ),
	( "ATTRIBUTE_15", "Other" ),
	( "ATTRIBUTE_16", "AA&U" ),
	( "ATTRIBUTE_17", "ActivationWorkshop" ),
	( "ATTRIBUTE_18", "AlignmnentWorkshop" ),
	( "ATTRIBUTE_19", "Automation(solutiontoamanualtask)" ),
	( "ATTRIBUTE_20", "ClusterAnalysis(notfullsegmentation)" ),
	( "ATTRIBUTE_21", "CommercializationDeck" ),
	( "ATTRIBUTE_22", "Concept/Product/Brand/Positioning/MediaorAdDesign/Evaluation/Testing" ),
	( "ATTRIBUTE_23", "Conjoint/DiscreteChoice" ),
	( "ATTRIBUTE_24", "ConsumerVillage" ),
	( "ATTRIBUTE_25", "DataFusion/Imputation" ),
	( "ATTRIBUTE_26", "DecisionTreeAnalysis(Chaid/CART)" ),
	( "ATTRIBUTE_27", "DiscussionTopic(dialogue)" ),
	( "ATTRIBUTE_28", "EmotionalConnections" ),
	( "ATTRIBUTE_29", "ExecutiveCommunications" ),
	( "ATTRIBUTE_30", "Eye-Tracking" ),
	( "ATTRIBUTE_31", "FactorAnalysis" ),
	( "ATTRIBUTE_32", "Flex/DIYCommunity/Panel" ),
	( "ATTRIBUTE_33", "Flex+Community/Panel(notfull-service&#44;butmorethanDIY)" ),
	( "ATTRIBUTE_34", "FullServiceCommunity/Panel" ),
	( "ATTRIBUTE_35", "HighlighterAnalysis" ),
	( "ATTRIBUTE_36", "Ideation/IgnitorWorkshop" ),
	( "ATTRIBUTE_37", "IDI/FocusGroups" ),
	( "ATTRIBUTE_38", "ImmersionWorkshop" ),
	( "ATTRIBUTE_39", "ImmersiveSpace/Exhibit(ExperientialGallery)" ),
	( "ATTRIBUTE_40", "Infographic" ),
	( "ATTRIBUTE_41", "In-Home/In-StoreEthnographies" ),
	( "ATTRIBUTE_42", "InsightsIntegration" ),
	( "ATTRIBUTE_43", "InteractiveInfographic/Portal" ),
	( "ATTRIBUTE_44", "KANO" ),
	( "ATTRIBUTE_45", "MarketBasket/Bundling" ),
	( "ATTRIBUTE_46", "MarketLandscape/MarketMapping" ),
	( "ATTRIBUTE_47", "MaxDiff" ),
	( "ATTRIBUTE_48", "Modeling/Forecasting(Predictive&#44;KeyDrivers&#44;Etc.)" ),
	( "ATTRIBUTE_49", "OnlineBulletinBoard/Journal" ),
	( "ATTRIBUTE_50", "OnlineDashboard" ),
	( "ATTRIBUTE_51", "OnlinePortal" ),
	( "ATTRIBUTE_52", "OnlineReport(DynamicReport)" ),
	( "ATTRIBUTE_53", "OnlineTabs" ),
	( "ATTRIBUTE_54", "Other" ),
	( "ATTRIBUTE_55", "PassiveMetering" ),
	( "ATTRIBUTE_56", "PathtoPurchase/ConsumerJourney" ),
	( "ATTRIBUTE_57", "PerceptualMap" ),
	( "ATTRIBUTE_58", "QuantIntercepts" ),
	( "ATTRIBUTE_59", "Recruiting/SampleManagement" ),
	( "ATTRIBUTE_60", "Remesh" ),
	( "ATTRIBUTE_61", "Segmentation" ),
	( "ATTRIBUTE_62", "ExcelSimulator/TypingTool" ),
	( "ATTRIBUTE_63", "StandardReport" ),
	( "ATTRIBUTE_64", "TextMining" ),
	( "ATTRIBUTE_65", "ToplineReport" ),
	( "ATTRIBUTE_66", "TrackingStudyorPre/PostAnalysis" ),
	( "ATTRIBUTE_67", "TURF" ),
	( "ATTRIBUTE_68", "Usability(product/website/etc)" ),
	( "ATTRIBUTE_69", "VanWestendorporSequentialPreferences" ),
	( "ATTRIBUTE_70", "VideoDeliverable" ),
	( "ATTRIBUTE_71", "VOC(VoiceoftheCustomer)" ),
	( "ATTRIBUTE_72", "WebScraping" ),
	( "ATTRIBUTE_73", "Weighting" ),
};