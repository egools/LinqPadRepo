<Query Kind="Statements" />

var sum = 0;

for(int i = 1; i < 1000; i++)
{
	if(i % 3 == 0 || i % 5 == 0)
	{
		//i.Dump();
		sum += i;
	}
}

sum.Dump();