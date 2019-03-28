<Query Kind="Program" />

void Main()
{
	System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\egoolsby\Documents\QueensAttackTestCase1.txt"); 
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

    int result = queensAttack(n, k, r_q, c_q, obstacles);
	result.Dump();
}

// Define other methods and classes here
    public static int queensAttack(int n, int k, int r_q, int c_q, int[][] obstacles) 
	{
		var spaces = 0;
		
		return spaces;
    }