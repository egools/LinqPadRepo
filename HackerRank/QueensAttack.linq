<Query Kind="Program" />

void Main()
{
	System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\egoolsby\Documents\LINQPad Queries\LinqPadRepo\HackerRank\QueensAttackTestCase3.txt"); 
    string[] nk = file.ReadLine().Split(' ');

    int n = Convert.ToInt32(nk[0]);

    int k = Convert.ToInt32(nk[1]);

    string[] r_qC_q = file.ReadLine().Split(' ');

    int r_q = Convert.ToInt32(r_qC_q[0]);

    int c_q = Convert.ToInt32(r_qC_q[1]);

    int[][] obstacles = new int[k][];

    for (int i = 0; i < k; i++) {
        obstacles[i] = Array.ConvertAll(file.ReadLine().Split(' '), obstaclesTemp => Convert.ToInt32(obstaclesTemp));
    }

	//n.Dump(); k.Dump(); r_q.Dump(); c_q.Dump(); obstacles.Dump();
    int result = queensAttack(n, k, r_q, c_q, obstacles);
	result.Dump();
	
	//PrintBoard(n, r_q, c_q, obstacles);
	
}

// Define other methods and classes here
    public static int queensAttack(int n, int k, int r_q, int c_q, int[][] obstacles) 
	{
		var spaces = 0;		
		Action<int, int, int, int, Func<int[], bool>> CountSpaces = (obIndex, q_locaction, minDistance, maxDistance, predicate) =>
		{		
			//ob index: handles vertical vs horizontal (0 vertical, 1 horizontal)
			//q_location: row or cal index of queen
			//minDistance: # spaces between queen and top/left boundry 
			//maxDistance: # spaces between queen and bottom/right boundry
			//predicate: function to find the obstacles between queen and boundry
			var obs = (from o in obstacles
				where predicate(o)
				select o[obIndex]).Distinct().ToList();
				
			obs.Add(q_locaction);
			obs.Sort();
			
			var q_index = obs.IndexOf(q_locaction);
				
			if(q_index == 0)
				spaces += minDistance;
			else 
				spaces += obs[q_index] - obs[q_index - 1] - 1;
				
			if(q_index == obs.Count - 1)
				spaces += maxDistance;
			else 
				spaces += obs[q_index + 1] - obs[q_index] - 1;
		};
		
		CountSpaces(0, r_q, r_q - 1, n - r_q, (int[] o) => o[1] == c_q); //vertical			
		CountSpaces(1, c_q,  c_q - 1, n - c_q, (int[] o) => o[0] == r_q); //horizontal
		CountSpaces(1, c_q, Math.Min(r_q - 1, c_q - 1), Math.Min(n - r_q, n - c_q), (int[] o) => o[0] == o[1] + r_q - c_q); //diag rising
		CountSpaces(1, c_q, Math.Min(n - r_q, c_q - 1), Math.Min(r_q - 1, n - c_q), (int[] o) => o[0] == -o[1] + r_q + c_q); //diag falling
		
		return spaces;
    }
	
	static void PrintBoard(int n, int r_q, int c_q, int[][] obstacles)
	{
		var board = new string[n,n];
		for(int i = n - 1; i >= 0; i--)
		{
			for(int j = n - 1; j >= 0; j--)
			{
				board[i,j] = "-";
			}
		}
		board[r_q - 1, c_q - 1] = "Q";
		foreach(var ob in obstacles)
		{
			board[ob[0] - 1, ob[1] - 1] = "x";
		}
		
		board.Dump();
	}