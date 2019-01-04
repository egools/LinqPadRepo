<Query Kind="Program" />

void Main()
{
	System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\egoolsby\Documents\LINQPad Queries\LinqPadRepo\HackerRank\DNATestCase0.txt");
	int n = Convert.ToInt32(file.ReadLine());

	string[] genes = file.ReadLine().Split(' ');

    int[] health = Array.ConvertAll(file.ReadLine().Split(' '), healthTemp => Convert.ToInt32(healthTemp));
    int s = Convert.ToInt32(file.ReadLine());
	
	//Custom Code		
	var tree = new AhoTree(genes, health);
	
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
		tree.Score(d, first, last);
		
		if(score > max)
			max = score;
		if(score < min)
			min = score;
			
		//score.Dump();
		//End Custom
	}
	
	Console.WriteLine($"{min} {max}");
	
	//master.Dump();
}

// Define other methods and classes here
public class AhoTree
{	
	private readonly Node _root = new Node();
	
	public AhoTree(string[] sequences, int[] health)
	{
		
	}
	
	public int Score(string text, int start, int end)
	{
		var score = 0;
		return score;
	}
	
	private void BuildFails()
	{
		
	}
}

public class Node
{
	public char Character;
	public Dictionary<char, Node> Children;
	public Dictionary<int, int> Scores;
	public Node Parent;
	public Node this[char c]
	{
		get { return Children.ContainsKey(c) ? Children[c] : null; }
		set { if(!Children.ContainsKey(c)) Children[c] = value; }
	}
	
	public Node()
	{
		Children = new Dictionary<char, Node>();
	}
	
	public Node(char c, Node parent)
	{
		Character = c;
		Parent = parent;
		Children = new Dictionary<char, Node>();
		Scores = new Dictionary<int, int>();
	}
	
	public void BuildGenes(string gene, int score, int validIndex)
	{		
		var c = gene[0];
		this[c] = this[c] ?? new Node(c, this);
		
		if(gene.Length > 1)
			this[c].BuildGenes(gene.Substring(1, gene.Length - 1), score, validIndex);
		else
			this[c].Scores.Add(validIndex, score);
	}
	
	public int FindScore(string gene, int mindex, int maxdex)
	{
		var returnVal = 0;		
			
		return returnVal;
	}
}