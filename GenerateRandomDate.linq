<Query Kind="Statements" />

Random r = new Random();
var start = new DateTime(1935, 1, 1).Ticks;
var end = new DateTime(1999, 12, 31).Ticks;

var s = Convert.ToInt32(start / 1000000000);
var e = Convert.ToInt32(end / 1000000000);

var rand = Convert.ToInt64(r.Next(s, e)) * 1000000000;
var dt = new DateTime(rand);
dt.ToString("M/d/yyyy").Dump();