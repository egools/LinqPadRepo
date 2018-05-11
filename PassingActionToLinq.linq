<Query Kind="Statements" />

var fields = new Dictionary<string, string>();
Action<int> addFields = delegate (int i) 
{ 
	fields.Add($"Q245_{i}", ""); 
	fields.Add($"Q250_{i}", ""); 
};
Enumerable.Range(1, 43).ToList().ForEach(i => addFields(i));

fields.Dump();