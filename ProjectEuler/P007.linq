<Query Kind="Program" />

void Main()
{
	var count = 1;
	var currentNum = 2;
	while(count != 10001)
	{
		currentNum++;
		if(isPrime(currentNum))
			count++;	
	}
	currentNum.Dump();
}

// Define other methods and classes here
public bool isPrime(int num)
{
	var div = 2;
	while(num % div != 0 && div <= num)
	{
		div++;
	}
	
	return div == num;
}