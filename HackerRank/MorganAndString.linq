<Query Kind="Program" />

void Main()
{
	morganAndString("JACK", "DANIEL").Dump();
}

// Define other methods and classes here
static string morganAndString(string a, string b) {
	var outStr = "";
	
	var aQ = new Queue<char>(a.ToList());
	var bQ = new Queue<char>(b.ToList());
	
	char aChar = aQ.Dequeue();
	char bChar = bQ.Dequeue();
	while(aChar != '[' || bChar != '[')
	{
		if(aChar <= bChar)
		{
			outStr += aChar;
			aChar = aQ.Count > 0 ? aQ.Dequeue() : '[';
		}
		else
		{
			outStr += bChar;
			bChar = bQ.Count > 0 ? bQ.Dequeue() : '[';
		}
	}
	
	return outStr;
}