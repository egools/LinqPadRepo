<Query Kind="Program" />

void Main()
{
	strangeCounter(1000000000000).Dump();
}

// Define other methods and classes here
static long strangeCounter(long t) 
{
	long num = 0;
	long max = 3;
	while(num < t)
	{
		num += max;
		max += max;
	}
	return num - t + 1;
}