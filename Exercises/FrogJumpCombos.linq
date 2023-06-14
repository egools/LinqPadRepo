<Query Kind="Program" />

void Main()
{
	var distance = 45;
	//var j = new Jumper(distance);
	//j.Jumps.Dump();
	
	Fibo(distance + 1).Dump();
}

// Define other methods and classes here
public class Jumper
{
	public long Jumps {get; private set;}
	private int distance;
	
	public Jumper(int d)
	{
		Jumps = 0;
		distance = d;
		Jump(1, 0);
		Jump(2, 0);
	}
	
	public void Jump(int d, int loc)
	{		
		var newLoc = loc + d;
		
		if(newLoc > this.distance)
			return;
		else if(newLoc == this.distance)
		{
			this.Jumps++;
			return;
		}
		else
		{
			Jump(1, newLoc);
			Jump(2, newLoc);
			return;
		}	
	}
}

public static long Fibo(long loc)
{
	if (loc == 0) return 0;
	else if (loc == 1) return 1;
	else if (loc == 2) return 1;
	else
	{
		return Fibo(loc - 1) + Fibo(loc - 2);
	}
}