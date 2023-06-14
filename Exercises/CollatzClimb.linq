<Query Kind="Program" />

public static void Main()
{
	var init = 1;
	var max = 32;
	var tree = BuildTree(init, max, new Node(init));
	
	tree.Dump();
}

public static Node BuildTree(int seedVal, int maxVal, Node n)
{
	if(seedVal > maxVal)
		return new Node(seedVal);
	else
	{
		var ob = GetOddBranch(seedVal);
		var eb = seedVal * 2;
		if(ob != -1)
		{
			//Console.WriteLine("Entering Odd Branch: " + ob);
			n.OddBranch = BuildTree(ob, maxVal, new Node(ob));
		}
		else 
			n.OddBranch = new Node(ob);
		//Console.WriteLine("Entering Even Branch:" + eb);
		n.EvenBranch = BuildTree(eb, maxVal, new Node(eb));
		return n;
	}
}

public class Node 
{
	public Node(int val)
	{
		Value = val;
	}
	public int Value;
	public Node OddBranch;
	public Node EvenBranch;
}
	   
public static int GetOddBranch(int value)
{
	var num = (value - 1) / 3.0;
	if(num % 1 == 0 && num > 1)
		return (int)num;
	else
		return -1;
}