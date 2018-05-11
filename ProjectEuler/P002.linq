<Query Kind="Statements" />

var evenFib = new List<int>();

var n0 = 1;
var n1 = 2;

while (n1 < 4000000)
{
	var newTerm = n0 + n1;
	n0 = n1;
	n1 = newTerm;
	if(newTerm % 2 == 0)
		evenFib.Add(newTerm);
}

var total = evenFib.Sum() + 2;

total.Dump();