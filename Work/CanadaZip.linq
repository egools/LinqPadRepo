<Query Kind="Statements" />

var rex = @"^[ABCEGHJKLMNPRSTVXY]\d[A-Z]\d[A-Z]\d$";
var code = "a4f4d2";
Regex.IsMatch(code.ToUpper(), rex).Dump();          