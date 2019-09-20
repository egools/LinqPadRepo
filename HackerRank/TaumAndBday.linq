<Query Kind="Program" />

void Main()
{
	//https://www.hackerrank.com/challenges/taum-and-bday/problem?utm_campaign=challenge-recommendation&utm_medium=email&utm_source=24-hour-campaign
	taumBday(10, 10, 1, 1, 1).Dump();
	taumBday(5, 9, 2, 3, 4).Dump();
	taumBday(3, 6, 9, 1, 1).Dump();
	taumBday(7, 7, 4, 2, 1).Dump();
	taumBday(3, 3, 1, 9, 2).Dump();
}

public static long taumBday(int b, int w, int bc, int wc, int z)
{
	return (w * Math.Min((long)bc + z, wc)) + (b * Math.Min((long)wc + z, bc));
}