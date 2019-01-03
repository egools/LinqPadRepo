<Query Kind="Program" />

void Main()
{
	System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\egoolsby\Documents\LINQPad Queries\LinqPadRepo\HackerRank\DNATestCase2.txt");
	int n = Convert.ToInt32(file.ReadLine());

	string[] genes = file.ReadLine().Split(' ');

    int[] health = Array.ConvertAll(file.ReadLine().Split(' '), healthTemp => Convert.ToInt32(healthTemp));
    int s = Convert.ToInt32(file.ReadLine());

	//Custom Code
	long min = int.MaxValue;
	long max = 0;
	//End Custom
	
    for (int sItr = 0; sItr < s; sItr++) 
	{
        string[] firstLastd = file.ReadLine().Split(' ');

        int first = Convert.ToInt32(firstLastd[0]);
        int last = Convert.ToInt32(firstLastd[1]);
        string d = firstLastd[2];
		
		//Custom Code
		long score = 0;

		for(int g = first; g <= last; g++)
		{
			if(Regex.IsMatch(d, genes[g]))
				score += health[g] * Regex.Matches(d, genes[g]).Count;
		}
		
		sItr.Dump();
		//score.Dump();
		
		if(score > max)
			max = score;
		if(score < min)
			min = score;
		//End Custom
	}
	
	Console.WriteLine($"{min} {max}");
	
	//master.Dump();
}

// Define other methods and classes here