<Query Kind="Statements" />

var ans = 0;

for(int a = 1; a <= 998; a++)
{
	var remaining = 1000 - a;
	if(ans > 0) break;
	for(int b = 1; b < remaining - 1; b++)
	{
		var c = 1000 - a - b;
		if(Math.Pow(a, 2) + Math.Pow(b, 2) == Math.Pow(c, 2))
		{
			ans = a * b * c;
			$"{a}^2 + {b}^2 = {c}^2".Dump();
			break;
		}
	}
}

ans.Dump();