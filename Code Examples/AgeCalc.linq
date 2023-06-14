<Query Kind="Statements" />

DateTime date;
DateTime.TryParse("6/12/1991", out date);
var age = (DateTime.Now - date).TotalDays / 365.25;
var age2 = Math.Floor(age).ToString("0");
age.Dump();
age2.Dump();