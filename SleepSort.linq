<Query Kind="Statements">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

var input = new List<int> { 1, 9, 2, 1, 3 };
var tasks = new List<Task>();
var output = new List<int>();

input.ForEach(n => 
{
	Task t = new Task(() =>
	{
		Thread.Sleep(n * 100);
		output.Add(n);
	});
	t.Start();
	tasks.Add(t);
});

Task.WaitAll(tasks.ToArray());
output.Dump();