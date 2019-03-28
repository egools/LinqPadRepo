<Query Kind="Statements" />

var num = "277777788888899";

var step = 0;
$"{step++.ToString("00")} - {num}".Dump();
while(num.Length > 1)
{
	long tot = 1;
	for(int i = 0; i < num.Length; i++)
	{
		tot *= (long)char.GetNumericValue(num[i]);
	}
	$"{step++.ToString("00")} - {tot}".Dump();
	num = tot.ToString();
}
