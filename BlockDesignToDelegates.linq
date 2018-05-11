<Query Kind="Statements">
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>System.Linq</Namespace>
</Query>

var ls = new List<List<int>>();
var max = 47;
ls.Add((new int[] {20,22,10,18,36,7,4,16,24,39}).ToList());
ls.Add((new int[] {31,41,33,13,5,12,35,14,25,38}).ToList());
ls.Add((new int[] {4,18,35,12,44,17,6,21,42,2 }).ToList());
ls.Add((new int[] {17,36,12,38,15,45,19,1,23,34}).ToList());
ls.Add((new int[] {13,42,34,25,1,20,10,40,37,3}).ToList());
ls.Add((new int[] {5,7,36,10,23,33,43,9,11,30}).ToList());
ls.Add((new int[] {9,13,44,24,17,14,20,19,43,26}).ToList());
ls.Add((new int[] {6,21,37,11,3,26,36,43,28,32}).ToList());
ls.Add((new int[] {18,16,21,15,25,5,23,28,29,47}).ToList());
ls.Add((new int[] {32,47,19,22,14,16,38,8,27,28}).ToList());
ls.Add((new int[] {1,11,31,39,6,27,40,2,22,33}).ToList());
ls.Add((new int[] {8,45,29,44,40,9,42,30,47,27}).ToList());
ls.Add((new int[] {2,9,30,16,12,13,39,28,3,46}).ToList());
ls.Add((new int[] {41,32,46,30,4,3,7,35,8,34}).ToList());
ls.Add((new int[] {15,31,26,41,24,29,46,39,45,37}).ToList());


List<List<int>> output = new List<List<int>>();
for(int i = 0; i < max; i++)
{
	output.Add(new List<int>());
}

for(int i = 0; i < ls.Count; i++)
{
	for(int j = 0; j < ls[i].Count; j++)
	{
		int val = Convert.ToInt32(ls[i][j]) - 1;
		output[val].Add(i + 1);
	}
}
string dump = "";
output.ForEach(o => dump += string.Join(",", o) + Environment.NewLine);
dump.Dump();