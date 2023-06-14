<Query Kind="Statements" />

double a = 1.111;
double b = 2.95;

int a2 = Convert.ToInt32(a);
int b2 = Convert.ToInt32(b);

int a3 = (int)a;
int b3 = (int)b;

$"Convert.ToInt32({a}) = {a2}".Dump();
$"Convert.ToInt32({b}) = {b2}".Dump();

$"(int)({a}) = {a3}".Dump();
$"(int)({b}) = {b3}".Dump();