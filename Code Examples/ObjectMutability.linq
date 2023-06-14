<Query Kind="Program" />

void Main()
{
	var things = new List<Thing>();
	things.Add(new Thing(true, "a", 1));
	things.Add(new Thing(true, "b", 2));
	things.Add(new Thing(true, "c", 3));
	things.Add(new Thing(true, "d", 4));
	things.Add(new Thing(true, "e", 5));
	things.Add(new Thing(true, "f", 6));
	things.Add(new Thing(true, "g", 7));
	things.Add(new Thing(true, "h", 8));
	things.Add(new Thing(true, "i", 9));
	
	var toCheck = things.FindAll(t => t.index % 2 == 0); //list of references to original objects
	
	foreach(var t in toCheck)
	{
		t.isGood = false;
		t.name = "adasaddsa";
	}
	
	things.Dump();
}

// Define other methods and classes here
public class Thing
{
	public Thing(bool a, string b, int c)
	{	
		isGood = a;
		name = b;
		index = c;
	}
	public bool isGood {get; set;}
	public string name {get; set;}
	public int index {get; set;}
}