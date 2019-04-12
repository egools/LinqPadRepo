<Query Kind="Program" />

void Main()
{
	var odds = new List<int> {7,10,12,14,18,22,25,28,33,40,50,60,66,80};
	var winnings = new List<int> 
	{
		2160000,1296000,816000,576000,480000,432000,402000,372000,348000,324000,300000,276000,252000,228000,216000,204000,192000,180000,168000,156000,144000,134400,124800,115200,
		105600,96000,92400,88800,85200,81600,78000,74400,70800,67800,64800,61800,58800,56400,54000,51600,49200,46800,44400,42000,39600,37200,34800,32880,31200,30240
	};
	
	var expected = new double[winnings.Count,odds.Count];
	
	for(int o = 0; o < odds.Count; o++)
	{
		for(int w = 0; w < winnings.Count; w++)
		{
			var chance = 1f / (odds[o] + 1);
			expected[w,o] = Math.Round((winnings[w] / 1000f) * odds[o] * chance);
		}
	}
	
	expected.Dump();
}
