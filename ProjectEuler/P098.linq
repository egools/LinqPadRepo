<Query Kind="Program" />

void Main()
{
	var words = new List<string>();
	string line;
	System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\egoolsby\Documents\LINQPad Queries\ProjectEuler\p098_words.txt");
	while((line = file.ReadLine()) != null)
	{
   		var wordLine = line.Split(',');
		words.AddRange(wordLine);
	}
	file.Close();
	
	var anagrams = new Dictionary<string, Anagram>();
	var squares = new Dictionary<string, List<string>>();
	foreach(var word in words)
	{
		var wordKey = String.Concat(word.OrderBy(c => c));
		if(!anagrams.ContainsKey(wordKey))
		{
			var charCounts = new Dictionary<char, int>();	
			foreach(var c in wordKey)
			{
				if(!charCounts.ContainsKey(c))
					charCounts[c] = 1;
				else
					charCounts[c]++;
			}
			
			var keySig = string.Join("", charCounts.Values);
			var sortedSig = String.Concat(keySig.OrderBy(w => w));
			anagrams.Add(wordKey, new Anagram(sortedSig));
		}			
		anagrams[wordKey].Words.Add(word);
		
	}
	
	for(int i = 2; i <= 32000; i++)
	{
		var sqStr = (i * i).ToString();
		
		var sqKey = String.Concat(sqStr.OrderBy(c => c));
		if(!squares.ContainsKey(sqKey))
		{
			var charCounts = new Dictionary<char, int>();	
			foreach(var c in sqKey)
			{
				if(!charCounts.ContainsKey(c))
					charCounts[c] = 1;
				else
					charCounts[c]++;
			}
			
			var keySig = string.Join("", charCounts.Values);
			var sortedSig = String.Concat(keySig.OrderBy(w => w));
			squares.Add(sqKey, new List<string>());
		}			
		squares[sqKey].Add(sqStr);
	}	
	
	var anagramSet = anagrams.Where(kv => kv.Value.Words.Count() > 1);
	var squareSet = squares.Where(kv => kv.Value.Count() > 1);
	
	//anagramSet.Dump();
	squareSet.Dump();
}

// Define other methods and classes here
public class Anagram
{
	public List<string> Words;
	public string Signature;
	
	public Anagram(string sig)
	{
		Words = new List<string>();
		Signature = sig;
	}
}