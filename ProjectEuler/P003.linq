<Query Kind="Program" />

void Main()
{
	
	var num = 600851475143;
	var theFacts = Factor(num);
	var max = theFacts.Max();
	
	theFacts.Dump();
	max.Dump();
}

// Define other methods and classes here
public static List<long> Factor(long num)
{
	var check = 2;
	
	var factors = new List<long>();
	var isPrime = true;
	
	while (check < num)
	{
		if (num % check == 0)
		{
			isPrime = false;
			var set1 = Factor(check);
			var set2 = Factor(num / check);
			
			factors.AddRange(set1);
			factors.AddRange(set2);
			num = num / check;
		}
		check++;		
	}
	
	if(isPrime)
		factors.Add(num);
		
	return factors;
}