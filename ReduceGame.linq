<Query Kind="Program" />

void Main()
{
	for(int i = 1; i <= 100000; i++)
	{
		var numSteps = Reduce(i, 0);
		KnownSteps.Add(i, numSteps);
	}	
	//KnownSteps.Dump();
	var max = KnownSteps.Aggregate((l, r) => l.Value > r.Value ? l : r);
	max.Dump();
}

// Define other methods and classes here
public int Reduce(int num, int steps)
{
	
//	var output = $"Step: {steps}; Number: {num}";
//	output.Dump();
	if(num == 1)
		return steps;
	else if(KnownSteps.ContainsKey(num))
		return KnownSteps[num] + steps;
	else if (num % 2 == 0)
		return Reduce(num / 2, ++steps);
	else
		return Reduce((num * 3) + 1, ++steps);;
}

public Dictionary<int, int> KnownSteps = new Dictionary<int, int>();