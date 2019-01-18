<Query Kind="Program" />

void Main()
{
	climbingLeaderboard(new int[] {100, 90, 90, 80, 75, 60}, new int[] {50, 65, 77, 90, 102}).Dump();	
}

// Define other methods and classes here
static int[] climbingLeaderboard(int[] scores, int[] alice) {
	var ranks = new int[alice.Length];
	
	var board = scores.Distinct().ToList();
	var rank = board.Count;
	for(int aliceIndex = 0; aliceIndex < alice.Length; aliceIndex++)
	{
		//$"{alice[aliceIndex]} < {board[rank - 1]}".Dump();
		while(rank > 0 && alice[aliceIndex] >= board[rank - 1])
		{
			rank--;
		}		
		ranks[aliceIndex] = rank + 1;
	}		
	return ranks;
}