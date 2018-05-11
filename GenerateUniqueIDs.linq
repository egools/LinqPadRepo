<Query Kind="Statements" />

var fileLoc = @"C:\Users\egoolsby\Documents\UB_CardholderIDs.csv";
var numIDs = 10000;
var preList = new List<string>();

for(int i = 0; i < numIDs; i++)
{
	var a = Guid.NewGuid().ToString().GetHashCode().ToString("x"); 
	try
	{
		preList.Add("C" + a.Substring(0, 5));
	}
	catch
	{
		string output = "error: " + a;
		output.Dump();
	}
}

var dupes = preList.GroupBy(id => id).Where(id => id.Count() > 1).ToList();
var finalList = preList.Distinct().ToList();
finalList.Count().Dump();
finalList.Dump();

var csv = new StringBuilder();

finalList.ForEach(id => csv.AppendLine(id));
File.WriteAllText(fileLoc, csv.ToString());