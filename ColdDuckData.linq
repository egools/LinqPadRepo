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
	var data = JsonConvert.DeserializeObject<ColdDuck>(text).Years.First();

	foreach (var player in data.Players)
	{
		if(player.NetScores is null)
			player.NetScores = new List<List<int>>();
		var playerBaseStrokes = player.Handicap / 18;
		for (int course = 0; course < data.Courses.Count; course++)
		{
			var netScore = new List<int>();
			for (int hole = 0; hole < 18; hole++)
			{
				if (player.Scores[course].Any())
				{
					var per18 = player.Handicap % 18;
					var additionalStrokes = data.Courses[course].Handicaps[hole] <= per18 ? 1 : 0;
					netScore.Add(player.Scores[course][hole] - (playerBaseStrokes + additionalStrokes));
				}
			}
			player.NetScores.Add(netScore);
		}
	}
	//data
	//.Players
	//.Select(p => (Name: p.Name, Scores: p.Scores.Where(score => score.Any()).Select(s => s.Sum() - 72 - p.Handicap)))
	//.Select(p => (p.Name, string.Join(',', p.Scores), p.Scores.Sum()))
	//.OrderBy(p => p.Item3)
	//.Dump();

	var holeResults = new List<(string Course, int Number, int Par, int Handicap, double Average, double AvgToPar)>();
	for (int courseIndex = 0; courseIndex < data.Courses.Count; courseIndex++)
	{
		var course = data.Courses[courseIndex];
		var scores = data.Players.Select(p => p.Scores[courseIndex]).Where(s => s.Any());
		for (int hole = 0; hole < 18; hole++)
		{
			var average = Math.Round(scores.Select(s => s[hole]).Average(), 2);
			holeResults.Add((course.Name, hole + 1, course.Par[hole], course.Handicaps[hole], average, Math.Round(average - course.Par[hole], 2)));
		}
	}
	holeResults.OrderBy(r => r.AvgToPar).Dump();
}

public record Course(
	string Name,
	IReadOnlyList<int> Handicaps,
	IReadOnlyList<int> Par,
	string Tees,
	int Distance
);

public class Player
{
	public string Name { get; init; }
	public IReadOnlyList<List<int>> Scores { get; init; }
	public List<List<int>> NetScores { get; set; }
	public int Handicap { get; init; }
}

public record ColdDuck(
	IReadOnlyList<DuckYear> Years
);

public record DuckYear(
	string Year,
	IReadOnlyList<Player> Players,
	IReadOnlyList<Course> Courses
);