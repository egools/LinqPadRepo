<Query Kind="Program" />

void Main()
{
	var words = new [] {"blah blah", "big game (deer&comma; elk&comma; buffalo&comma; etc.)", "varmint/small game (prairie dogs&comma; squirrels&comma; etc.)"};
	Andify(words).Dump();
}
// Define other methods and classes here
Func<IEnumerable<string>, string> Andify = delegate (IEnumerable<string> list)
{
    var text = string.Join(", ", list);
    var regex = new Regex("(?:,)([^,]*$)");
    return regex.Replace(text, " and$1");
};