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
	//tree.Dump();
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
		//tree.Score(d, first, last);
		
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
	public Node Root 
	{ 
		get { return _root; } 
	}
	
	public AhoTree(string[] genes, int[] health)
	{
		for(int i = 0; i < genes.Length; i++)
		{
			Root.Add(genes[i], health[i], i);
		}
	}
	
	public int Score(string text, int start, int end)
	{
		var score = 0;
		return score;
	}
	
	private void BuildFails()
	{
		var nodeQ = new Queue<Node>();
		nodeQ.Enqueue(_root);
		
		while(nodeQ.Count > 0)
		{
			var node = nodeQ.Dequeue();
			
			
		}
	}
}

public class Node
{
	public char Character;
	public Dictionary<char, Node> Children;
	public Dictionary<int, int> Scores;
	public Node Parent;
	public Node Fail;
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
	
	public void Add(string gene, int score, int index)
	{
		if(gene.Length > 0)
		{
			var c = gene[0];
			if(this[c] == null)
			{
				this[c] = new Node(c, this);
			}			
			
			this[c].Add(gene.Substring(1, gene.Length - 1), score, index);
		}
		else
		{
			Scores.Add(index, score);
		}
	}
	
	public int FindScore(string gene, int mindex, int maxdex)
	{
		var returnVal = 0;		
			
		return returnVal;
	}
}