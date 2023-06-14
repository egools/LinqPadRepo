<Query Kind="Statements" />

var numIDs = 133952;
var idList = new List<string>();

while(idList.Count < numIDs)
{
	var tempID = "RO18" + Guid.NewGuid().ToString().GetHashCode().ToString("x").Substring(0, 6); 	
	try
	{
		if(!idList.Contains(tempID))
			idList.Add(tempID);
	}
	catch
	{
		("error: " + tempID).Dump();
	}
}

idList.Count().Dump();
idList.Dump();