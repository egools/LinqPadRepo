<Query Kind="Statements" />

var n = 41;
var allGuys = new Queue<int>(Enumerable.Range(1, n));

while(allGuys.Count > 1)
{
	var currentGuy = allGuys.Dequeue();
	var toBeKilled = allGuys.Dequeue();
	
	string output = currentGuy + " kills " + toBeKilled;
	output.Dump();
	
	allGuys.Enqueue(currentGuy);
}

string winner = "n=" + n + ": " + allGuys.Peek() + " wins";
winner.Dump();