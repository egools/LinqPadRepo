<Query Kind="Program" />

void Main()
{
	var words = new List<string>();
	string line;
	System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\Eric\Documents\LINQPad Queries\LinqPadRepo\ProjectEuler\p098_words.txt");
	while((line = file.ReadLine()) != null)
	{
   		var wordLine = line.Split(',');
		words.AddRange(wordLine);
	}
	file.Close();
	
	var anagrams = new Dictionary<string, Anagram>();
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
	
	anagrams.Where(kv => kv.Value.Words.Count() > 1).GroupBy(kv => kv.Value.Signature).Dump();
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