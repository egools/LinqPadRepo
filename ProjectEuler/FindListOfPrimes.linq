<Query Kind="Program" />

void Main()
{
	var s = BuildPrimes(2000000);
	s.Dump();
}

public static Dictionary<int, bool> BuildPrimes(int limit)
{
	Dictionary<int, bool> sieve = new Dictionary<int, bool>();
	int limitSqrt = (int)Math.Sqrt((double)limit);
    Enumerable.Range(0, limit).ToList().ForEach(s => sieve.Add(s, false));	
    sieve[0] = false;
    sieve[1] = false;
    sieve[2] = true;
    sieve[3] = true;

    for (int x = 1; x <= limitSqrt; x++) 
	{
        for (int y = 1; y <= limitSqrt; y++) 
		{
		
            int n = (4 * x * x) + (y * y);
            if (n <= limit && (n % 12 == 1 || n % 12 == 5)) 
                sieve[n] = !sieve[n];
			
            n = (3 * x * x) + (y * y);
            if (n <= limit && (n % 12 == 7)) 
                sieve[n] = !sieve[n];
			
            n = (3 * x * x) - (y * y);
            if (x > y && n <= limit && (n % 12 == 11)) 
                sieve[n] = !sieve[n];
        } 
    } 
    for (int n = 5; n <= limitSqrt; n++) 
	{
        if (sieve[n]) 
		{
            int x = n * n;
            for (int i = x; i <= limit; i += x)
                sieve[i] = false;
        } 
    } 
	return sieve;
}
