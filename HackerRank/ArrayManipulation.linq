<Query Kind="Program" />

void Main()
{
	var a = new int[][] 
	{
	new int[] { 1, 2, 100 },
	new int[] { 2, 5, 100 },
	new int[] { 3, 4, 100 }
	};
	arrayManipulation(5, a).Dump();
}

// Define other methods and classes here
static long arrayManipulation(int n, int[][] queries) {
    var arr = new long[n + 2];
	var results = new long[n + 1];

    for(long i = 0; i < queries.Length; i++)
    {
        var start = queries[i][0];
        var next = queries[i][1] + 1;
		arr[start] += queries[i][2];
		arr[next] -= queries[i][2];
    }
	
	for(long i = 1; i < arr.Length - 1; i++)
    {
        results[i] = results[i - 1] + arr[i];
    }

	results.Dump();

    return results.Max();
}