<Query Kind="Program" />

void Main()
{
	string testCase = "5";
    System.IO.StreamReader file = new System.IO.StreamReader($@"C:\Users\egoolsby\Documents\LINQPad Queries\LinqPadRepo\HackerRank\MorganTestCase{testCase}.txt");
	System.IO.StreamWriter outFile = new System.IO.StreamWriter($@"C:\Users\egoolsby\Documents\LINQPad Queries\LinqPadRepo\HackerRank\MorganTestCase{testCase}Result.txt");

    int t = Convert.ToInt32(file.ReadLine());
    for (int tItr = 0; tItr < t; tItr++) 
	{
        string a = file.ReadLine();
        string b = file.ReadLine();
        //var resultSlow = morganAndStringSlow(a, b);
		var resultFast = morganAndStringFast(a, b);
		//$"results match: {resultSlow == resultFast}".Dump();
		outFile.WriteLine(resultFast);
		//resultFast.Dump();
    }
    file.Close();
	outFile.Close();
	
	morganAndStringFast("BBAABBCC", "BBAABBCC").Dump();
}

// Define other methods and classes here
static string morganAndStringSlow(string a, string b) 
{
	var start = DateTime.Now.Ticks;
	var outStr = "";
	
	a += "Z";
	b += "Z";
	while(a.Length > 1 &&  b.Length > 1)
	{
		if(a.CompareTo(b) < 0)
		{
			outStr += a[0];
			a = a.Substring(1);
		}
		else
		{
			outStr += b[0];
			b = b.Substring(1);
		}
	}
	
	if(a.Length > 1)
		outStr += a.Substring(0, a.Length - 1);
	if(b.Length > 1)
		outStr += b.Substring(0, b.Length - 1);;
		
	$"Slow: {(DateTime.Now.Ticks - start) / 10000f}ms".Dump();
	return outStr;
}

static string morganAndStringFast(string a, string b) 
{
	var start = DateTime.Now.Ticks;
	var outStr = "";
	
	a += "Z";
	b += "Z";
	var ai = 0;
	var bi = 0;
	while(ai < a.Length - 1 &&  bi < b.Length - 1)
	{
		var isA = false;
		if(a[ai] < b[bi])
		{
			outStr += a[ai];
			ai++;
			isA = true;
		}
		else if(a[ai] > b[bi])
		{
			outStr += b[bi];
			bi++;
		}
		else
		{
			
			if(a.Substring(ai + 1).CompareTo(b.Substring(bi + 1)) < 0)
			{
				outStr += a[ai];
				ai++;
				isA = true;
			}
			else
			{
				outStr += b[bi];
				bi++;
			}
		}
		
		if(isA)
			outStr += findChunk(ref a, ref ai, b[bi]);
		else
			outStr += findChunk(ref b, ref bi, a[ai]);
	}
	
	if(ai < a.Length - 1)
		outStr += a.Substring(ai, a.Length - ai - 1);
	if(bi < b.Length - 1)
		outStr += b.Substring(bi, b.Length - bi - 1);
	
	$"Fast: {(DateTime.Now.Ticks - start) / 10000f}ms".Dump();
	return outStr;
}

static string findChunk(ref string s, ref int si, char c)
{
	var toAdd = "";
	while(s[si] < c)
	{
		toAdd += s[si];
		si++;
	}
	return toAdd;
}