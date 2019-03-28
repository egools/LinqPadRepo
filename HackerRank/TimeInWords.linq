<Query Kind="Program" />

void Main()
{
	timeInWords(3, 15).Dump();
}

// Define other methods and classes here
static string[] nums = new string[] {"", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "quarter", "sixteen", "seventeen", "eighteen", "nineteen"};
static string timeInWords(int h, int m) 
{
	var outStr = "";	
	var hour = nums[h];
	
	if(m == 0)
	{
		outStr = $"{hour} o' clock";
	}
	else if(m < 30)
	{
		var minutes = "";
		if(m > 19)
		{
			var mStr = m.ToString();
			minutes = $"twenty {nums[(int)char.GetNumericValue(mStr[1])]}";
		}
		else
			minutes = nums[m];
		
		outStr = $"{minutes} minute{(m > 1 ? "s" : "")} past {hour}";
	}
	else if(m > 30)
	{
		if(h == 12)
			hour = "one";
		else
			hour = nums[h + 1];
		m = 60 - m;
		var minutes = "";
		if(m > 19)
		{
			var mStr = m.ToString();
			minutes = $"twenty {nums[(int)char.GetNumericValue(mStr[1])]}";
		}
		else
			minutes = nums[m];
		outStr = $"{minutes} minute{(m > 1 ? "s" : "")} to {hour}";
	}
	else
	{
		outStr = $"half past {hour}";
	}
	outStr = outStr.Replace("quarter minutes", "quarter");
	
	return outStr;
}