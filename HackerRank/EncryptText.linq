<Query Kind="Statements" />

var text = "feedthedog";
text = text.Replace(" ", "");
int min = Convert.ToInt32(Math.Floor(Math.Sqrt(text.Length)));
int max =  Convert.ToInt32(Math.Ceiling(Math.Sqrt(text.Length)));

int rows, cols;
if (min * min >= text.Length)
{
	rows = min;
	cols = min;
}
else if (min * max >= text.Length)
{
	rows = min;
	cols = max;
}
else 
{
	rows = max;
	cols = max;
}

text = text.PadRight(rows * cols, ' ');

var cursor = 0;
var outText = "";
for(int i = 0; i < text.Length; i++)
{
	if(cursor >= text.Length)
	{
		if(outText[outText.Length - 1] != ' ')
			outText += " ";
		cursor = cursor % text.Length + 1;
	}
	
	outText += text[cursor];
	cursor += cols;
}

outText.Dump();
