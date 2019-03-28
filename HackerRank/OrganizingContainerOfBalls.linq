<Query Kind="Program" />

void Main()
{
	var q = new int[][] {
		new int [] {1, 3, 1},
		new int [] {2, 1, 2},
		new int [] {3, 3, 3}
	};
	organizingContainers(q);
}

// Define other methods and classes here
static string organizingContainers(int[][] container) {	
	var containerCapacity = new int[container.Length];
	var ballCount = new int [container[0].Length];	
	
	for(int c = 0; c < container.Length; c++)
	{
		for(int ballNum = 0; ballNum < container[c].Length; ballNum++)
		{
			containerCapacity[c] += container[c][ballNum];
			ballCount[ballNum] += container[c][ballNum];
		}
	}
	
	var sortedContainers = containerCapacity.ToList();
	sortedContainers.Sort();
	var sortedBalls = ballCount.ToList();
	sortedBalls.Sort();
	
	return sortedContainers.SequenceEqual(sortedBalls) ? "Possible" : "Impossible";
}