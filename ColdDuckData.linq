<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <RuntimeVersion>7.0</RuntimeVersion>
</Query>

void Main()
{
	var text = File.ReadAllText(
		Path.Combine(
			Path.GetDirectoryName(Util.CurrentQueryPath),
			"cold-duck-scores.json"
			));
	var data = JsonConvert.DeserializeObject<ColdDuck>(text)
	.Years
	.FirstOrDefault()
	.Players
	.Select(p => (Name: p.Name, Scores: p.Scores.Where(score => score.Any()).Select(s => s.Sum() - 72 - p.Handicap)))
	.Select(p => (p.Name, string.Join(',', p.Scores), p.Scores.Sum()))
	.OrderBy(p => p.Item3)
	.Dump();
	
}

public record Course(
	string Name,
	IReadOnlyList<int> Handicaps,
	IReadOnlyList<int> Par,
	string Tees,
	int Distance
);

public record Player(
	string Name,
	IReadOnlyList<List<int>> Scores,
	int Handicap
);

public record ColdDuck(
	IReadOnlyList<DuckYear> Years
);

public record DuckYear(
	string Year,
	IReadOnlyList<Player> Players,
	IReadOnlyList<Course> Courses
);