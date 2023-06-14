<Query Kind="Statements" />

Random r = new Random();

var lists = new List<List<int>>();
var counts = new List<int>  {0,0,0,0,0,0,0};
for(int j = 1; j <= 300; j++)
{
	var thisList = new List<int>();
	for(int i = 1; i <= 7; i++)
	{
		var val = r.Next(1,4);
		thisList.Add(val);
		if(val == 1) counts[i - 1]++;
	}
	lists.Add(thisList);
}

var avg = counts.Average();
var variance = (counts.Sum(i => Math.Pow(i, 2)) / 7) - Math.Pow(avg, 2);
var stdDev = Math.Sqrt(variance);


counts.Dump();
variance.Dump();
stdDev.Dump();