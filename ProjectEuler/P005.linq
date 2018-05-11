<Query Kind="Program" />

void Main()
{
	var num = 20;
	
	while(!isDiv(num))
		num++;
		
	num.Dump();
}

// Define other methods and classes here
public bool isDiv(int num)
{
	for(int i = 2; i <= 20; i++)
	{
		if(num % i != 0)
			return false;
	}
	return true;
}