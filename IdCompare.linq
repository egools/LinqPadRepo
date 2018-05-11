<Query Kind="Statements" />

int [] me = new int [] {1, 2, 3, 4, 6};
int [] chel = new int [] {1, 4, 5, 7};

me.Except(chel).Dump(); //Everything in first list that isnt in second list
chel.Except(me).Dump(); //everything in second list that isnt in first
me.Intersect(chel).Dump(); //everything the two lists have in commmon
me.Union(chel).Dump(); //combination of both lists with doubles removed