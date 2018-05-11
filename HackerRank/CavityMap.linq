<Query Kind="Program" />

void Main()
{
	var a = new string[] {
		"11121",
		"19191",
		"11381",
		"12941",
		"11111"
	};
	
	cavityMap(a).Dump();
}

// Define other methods and classes here
static string[] cavityMap(string[] grid) {
    // Complete this function
	Func<char, int> Int = (c) => { return c == 'X' ? 999 : (int)Char.GetNumericValue(c);};
    for(int i = 1; i < grid.Length - 1; i++)
    {
        for(int j = 1; j < grid[i].Length - 1; j++)
        {
			var thisVal = grid[i][j];
			var isCavity = true;
			
			if(Int(thisVal) <= Int(grid[i-1][j])) isCavity = false;
			if(Int(thisVal) <= Int(grid[i][j-1])) isCavity = false;
			if(Int(thisVal) <= Int(grid[i + 1][j])) isCavity = false;
			if(Int(thisVal) <= Int(grid[i][j + 1])) isCavity = false;
				
			if(isCavity)
				grid[i] = grid[i].Remove(j, 1).Insert(j, "X");
		}	
    }
	return grid;
}
