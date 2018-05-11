<Query Kind="Statements" />

var counts = new List<int>  {3, 4, 4, 5, 6, 8};

var avg = counts.Average();
var variance = (counts.Sum(i => Math.Pow(i, 2)) / counts.Count) - Math.Pow(avg, 2);
var stdDev = Math.Sqrt(variance);

variance.Dump();
stdDev.Dump();