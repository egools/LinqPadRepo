<Query Kind="Statements" />

var counts = new List<int>  {3, 4, 4, 5, 6, 8};

var avg = counts.Average();
var variance = (counts.Sum(i => Math.Pow(i, 2)) / counts.Count) - Math.Pow(avg, 2);
var variance2 = (counts.Sum(i => Math.Pow(i - avg, 2)) / counts.Count);
var stdDev = Math.Sqrt(variance);

variance.Dump();
variance2.Dump();
stdDev.Dump();