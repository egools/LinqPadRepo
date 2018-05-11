<Query Kind="Program" />

void Main()
{
	var stacks = new List<Stack<int>> {
		new Stack<int>(new[] {int.MaxValue}),
		new Stack<int>(new[] {int.MaxValue}),
		new Stack<int>(new[] {int.MaxValue})
	};
	var height = 7;
	
	for (int i = height; i > 0; i--)
	{
		stacks[0].Push(i);
	}
	
	
	
	var testCount = 0;
	while(stacks[2].Count < height + 1)
	{
		var from = 0;
		var disk = stacks[from].Peek();
		
		if(disk > stacks[1].Peek() && disk > stacks[2].Peek())
		{
			Bump(from);	
			disk = stacks[from].Peek();
		}
		else if(disk > stacks[2].Peek() && disk > stacks[0].Peek())
		{
			Bump(from);	
			disk = stacks[from].Peek();
		}
		disk = stacks[from].Pop();
				
		var to = Bump(from);
		
		if(disk > stacks[to].Peek())
			to = Bump(to);
		if(disk > stacks[to].Peek())
			to = Bump(to);
	
		stacks[to].Push(disk);
		
		Console.WriteLine($"Disk #{disk}: {from} -> {to}.");
		
		if(testCount++ == 10) break;
	}
	
	stacks.Dump();
}

public static int Bump(int num)
{
	return (num + 1) % 3;
}
