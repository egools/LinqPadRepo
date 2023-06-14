<Query Kind="Statements" />

Random r = new Random();
var chars = "abcdefghijklmnopqrstuvwxyz ";

//int paraLen = int.MaxValue;
int paraLen = 100000;
var dict = new Dictionary<string, int>();

for(int i = 0; i <= paraLen; i++)
{
	char thisChar = 'a';
	var thisWord = "";
	while(thisChar != ' ')
	{
		thisChar = chars[r.Next(chars.Length)];
		if(thisChar != ' ')
			thisWord += thisChar;
		if(thisWord == "")
			thisChar = 'a';
	}
	if(!dict.ContainsKey(thisWord))
		dict.Add(thisWord, 1);
	else
		dict[thisWord]++;
}

var items = from pair in dict
    orderby pair.Value descending
    select pair;

items.Dump();