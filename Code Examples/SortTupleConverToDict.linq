<Query Kind="Statements" />

var a = new List<Tuple<string, int>>();
a.Add(new Tuple<string, int>("a", 234));
a.Add(new Tuple<string, int>("b", 23));
a.Add(new Tuple<string, int>("c", 2364));
a.Add(new Tuple<string, int>("d", 68));
a.Add(new Tuple<string, int>("e", 678));
a.Add(new Tuple<string, int>("f", 1));
a.Add(new Tuple<string, int>("g", 324));
a.Add(new Tuple<string, int>("h", 673));

a.Sort((x, y) => x.Item2.CompareTo(y.Item2));

var first4 = a.Take(4);
first4.Dump();
var toWrite = first4.ToDictionary(kv => kv.Item1, kv => "1");
toWrite.Dump();