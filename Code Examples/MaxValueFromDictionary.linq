<Query Kind="Statements" />

var a = new Dictionary<string, int>();
a.Add("a", 234);
a.Add("b", 23);
a.Add("c", 2364);
a.Add("d", 68);
a.Add("e", 678);
a.Add("f", 1);
a.Add("g", 324);
a.Add("h", 673);

var b = a.Aggregate((l, r) => l.Value > r.Value ? l : r);
b.Dump();