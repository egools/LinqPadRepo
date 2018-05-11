<Query Kind="Program" />

void Main()
{
	var s = new int[][] 
	{
		new int[]{4,8,2},
		new int[]{4,5,7},
		new int[]{6,1,6}
	};
	
	var sq = new MagicSquare(s);
	
	//sq.Verify().Dump();
	sq.Solve();
}

// Define other methods and classes here
public class MagicSquare
{
	private int [][] _square;
	private int _moves;
	public MagicSquare(int [][] s)
	{	
		_square = s;
		_moves = 0;
	}
	
	public bool Verify()
	{
		var ret = true;
		for(int i = 0; i < 3; i++)
			for(int j = 0; j < 3; j++)
				if(!CheckLocation(i, j)) 
					ret = false;
				
		return ret;
	}
	
	public void Solve()
	{		
		_moves += Math.Abs(5 - _square[1][1]);
		Console.WriteLine(_moves);
	}
	
	public List<int[]> Find(int num)
	{
		var locations = new List<int[]>();
		for(int i = 0; i < 3; i++)
		{
			for(int j = 0; j < 3; j++)
			{
				if(_square[i][j] == num)
					locations.Add(new []{i, j});
			}
		}
		return locations;
	}
	
	public bool CheckLocation(int row, int col)
	{
		var ret = true;
		if(_square[row].Sum() != 15) ret = false;
		if(_square[0][col] + _square[1][col] + _square[2][col] != 15) ret = false;
		
		if(row == col)
			if(_square[0][0] + _square[1][1] + _square[2][2] != 15) 
				ret = false;			
		
		if((row + col) == 2)
			if(_square[0][2] + _square[1][1] + _square[2][0] != 15) 
				ret = false;
		
		return ret;
	}

}