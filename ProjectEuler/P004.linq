<Query Kind="Statements" />

var maxPalindrome = 0;
var palindromes = new List<int>();

for (int num1 = 100; num1 <= 999; num1++)
{
	for (int num2 = 100; num2 <= 999; num2++)
	{
		var prod = (num1 * num2);	
		var prodStr = prod.ToString();
		var prodStrRev = string.Join("", prodStr.Reverse());
		
		if(prodStr == prodStrRev)
		{
			palindromes.Add(prod);
			
			if(prod > maxPalindrome)
				maxPalindrome = prod;
		}
	}
}

maxPalindrome.Dump();
palindromes.Dump();