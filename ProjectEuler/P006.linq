<Query Kind="Statements" />

var nums = Enumerable.Range(1,100);

var sumSq = nums.Sum(i => Math.Pow(i, 2));
var sqSum = Math.Pow(nums.Sum(), 2);

(sqSum - sumSq).Dump();