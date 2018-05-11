<Query Kind="Program" />

void Main()
{
	var robber = Robberize("I'm speaking Robber's language!");
	robber.Dump();
	Derobberize(robber).Dump();
}

public static string cons = "[bcdfghjklmnpqrstvwxz]";
public static string Robberize(string text)
{
	var regex = new Regex($"({cons})", RegexOptions.IgnoreCase);
	var output = regex.Replace(text, "$1o$1");
	return output;
}

public static string Derobberize(string text)
{
	var regex = new Regex($"({cons})o(?:{cons})", RegexOptions.IgnoreCase);
	var output = regex.Replace(text, "$1");
	return output;
}