<Query Kind="Program" />

void Main()
{
	System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\egoolsby\Documents\LINQPad Queries\LinqPadRepo\HackerRank\DNATestCase0.txt");
	int n = Convert.ToInt32(file.ReadLine());

	string[] genes = file.ReadLine().Split(' ');

    int[] health = Array.ConvertAll(file.ReadLine().Split(' '), healthTemp => Convert.ToInt32(healthTemp));
    int s = Convert.ToInt32(file.ReadLine());

	//Custom Code
	var master = new Node("*");			
	for(int i = 0; i < genes.Length; i++)
	{
		master.BuildGenes(genes[i], health[i], i);
	}
	//master.Dump();
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
		for(int i = 0; i < d.Length; i++)
		{
			score += master.FindScore(d.Substring(i, d.Length - i), first, last);
		}
		
		if(score > max)
			max = score;
		if(score < min)
			min = score;
			
		score.Dump();
		//End Custom
	}
	
	Console.WriteLine($"{min} {max}");
	
	//master.Dump();
}

// Define other methods and classes here
public class Node
{
	public char Character;
	public Dictionary<char, Node> SubNodes;
	public List<int> Scores;
	public List<int> ValidIndeces;
	public List<int> TerminalIndeces;
	
	public Node(string c)
	{
		Character = c[0];
		SubNodes = new Dictionary<char, Node>();
		Scores = new List<int>();
		ValidIndeces = new List<int>();
		TerminalIndeces = new List<int>();
	}
	
	public void BuildGenes(string gene, int score, int validIndex)
	{		
		var c = gene[0];
		Node nextNode;
		if(SubNodes.ContainsKey(c))
		 	nextNode = SubNodes[c];
		else
		{
			nextNode = new Node(c.ToString());
			SubNodes.Add(c, nextNode);
		}
		
		if(gene.Length > 1)
			nextNode.BuildGenes(gene.Substring(1, gene.Length - 1), score, validIndex);
		else
		{
			nextNode.Scores.Add(score);
			nextNode.TerminalIndeces.Add(validIndex);
		}
			
		nextNode.ValidIndeces.Add(validIndex);
	}
	
	public int FindScore(string gene, int minIndex, int maxIndex)
	{
		var returnVal = 0;
		if(gene.Length > 0 && SubNodes.ContainsKey(gene[0]) && SubNodes[gene[0]].ValidIndeces.Any(i => i >= minIndex && i <= maxIndex))
			returnVal = Scores.Sum() + SubNodes[gene[0]].FindScore(gene.Substring(1, gene.Length - 1), minIndex, maxIndex);
		else if(TerminalIndeces.Any(i => i >= minIndex && i <= maxIndex))
			returnVal = Scores.Sum();
			
		return returnVal;
	}
}