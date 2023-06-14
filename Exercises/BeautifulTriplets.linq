<Query Kind="Program" />

void Main()
{
	//https://www.hackerrank.com/challenges/beautiful-triplets/problem?utm_campaign=challenge-recommendation&utm_medium=email&utm_source=24-hour-campaign
	var start = DateTime.Now;
	beautifulTriplets(3, new int[] {1, 2, 4, 5, 7, 8, 10}).Dump();
	(DateTime.Now - start).Dump();
}

// Define other methods and classes here
static int beautifulTriplets(int d, int[] arr) 
{
	var tripletCount = 0;
	
	var map = new Dictionary<int, List<int>>();
	
	for(int i = 0; i < arr.Length; i++)
	{
		if(map.ContainsKey(arr[i]))
			map[arr[i]].Add(i);
		else
			map.Add(arr[i], new List<int> { i });
	}
	
	var values = map.Keys.ToList();
	foreach(var value in values)
	{
		if(map.ContainsKey(value + d) && map.ContainsKey(value + d + d))
		{
			tripletCount += map[value].Count * map[value + d].Count * map[value + d + d].Count;
		}
	}		
	return tripletCount;
}