<Query Kind="Statements" />

long sum = Enumerable.Range(1,100).Sum();



while(true)
{
	var divisors = 2;
	for(long i = 2; i < Math.Ceiling(Math.Sqrt(sum)) + 1; i++)
	{
		if(sum % i == 0) 
			divisors += 2;
		if(i*i == sum) 
			divisors -= 1;
	}
	$"{sum}: {divisors}".Dump();
	if(divisors > 500) 
	{
		sum.Dump();
		break;
	}
	sum += sum + 1;
}