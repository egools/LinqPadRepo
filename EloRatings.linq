<Query Kind="Program" />

void Main()
{
	var expected = 0.8;
	WonBigOverPerformedTest(expected);
	WonSmallOverPerformedTest(expected);
	WonBigUnderPerformedTest(expected);
	WonSmallUnderPerformedTest(expected);
}

public void WonBigOverPerformedTest(double expected)
{
	"**BIG OVER**".Dump();
	var LeftTeam = new Team
	{
		ActualScore = 135,
		ProjectedScore = 100,
		Expected = expected
	};
	var RightTeam = new Team
	{
		ActualScore = 170,
		ProjectedScore = 135,
		Expected = 1 - expected
	};
	LeftTeam.SetMarginMod(RightTeam.ActualScore);
	RightTeam.SetMarginMod(LeftTeam.ActualScore);
	
	var lWin = LeftTeam.ActualScore > RightTeam.ActualScore;
	LeftTeam.CalcChange(lWin);
    RightTeam.CalcChange(!lWin);
	
	LeftTeam.Dump();
	RightTeam.Dump();
}

public void WonSmallOverPerformedTest(double expected)
{
	"**SMALL OVER**".Dump();
	var LeftTeam = new Team
	{
		ActualScore = 118,
		ProjectedScore = 90,
		Expected = expected
	};
	var RightTeam = new Team
	{
		ActualScore = 115,
		ProjectedScore = 90,
		Expected = 1 - expected
	};
	LeftTeam.SetMarginMod(RightTeam.ActualScore);
	RightTeam.SetMarginMod(LeftTeam.ActualScore);
	
	var lWin = LeftTeam.ActualScore > RightTeam.ActualScore;
	LeftTeam.CalcChange(lWin);
    RightTeam.CalcChange(!lWin);
	
	LeftTeam.Dump();
	RightTeam.Dump();
}

public void WonBigUnderPerformedTest(double expected)
{
	"**BIG UNDER**".Dump();
	var LeftTeam = new Team
	{
		ActualScore = 95,
		ProjectedScore = 115,
		Expected = expected
	};
	var RightTeam = new Team
	{
		ActualScore = 60,
		ProjectedScore = 90,
		Expected = 1 - expected
	};
	LeftTeam.SetMarginMod(RightTeam.ActualScore);
	RightTeam.SetMarginMod(LeftTeam.ActualScore);
	
	var lWin = LeftTeam.ActualScore > RightTeam.ActualScore;
	LeftTeam.CalcChange(lWin);
    RightTeam.CalcChange(!lWin);
	
	LeftTeam.Dump();
	RightTeam.Dump();
}

public void WonSmallUnderPerformedTest(double expected)
{
	"**SMALL UNDER**".Dump();
	var LeftTeam = new Team
	{
		ActualScore = 92,
		ProjectedScore = 100,
		Expected = expected
	};
	var RightTeam = new Team
	{
		ActualScore = 90,
		ProjectedScore = 90,
		Expected = 1 - expected
	};
	LeftTeam.SetMarginMod(RightTeam.ActualScore);
	RightTeam.SetMarginMod(LeftTeam.ActualScore);
	
	var lWin = LeftTeam.ActualScore > RightTeam.ActualScore;
	LeftTeam.CalcChange(lWin);
    RightTeam.CalcChange(!lWin);
	
	LeftTeam.Dump();
	RightTeam.Dump();
}

public class Team
{
	public double ActualScore { get; set;}
	public double ProjectedScore { get; set; }
	public double Expected { get; set; }
	public double Change;
	public double ChangeModified;
	public double ScoreMarginMod { get; set; }
	public double ProjectedMod {
		get 
		{
			var coef = Math.Pow(Math.Abs(ActualScore - ProjectedScore) / 35, 1.5);
			if(coef >= 1) coef = 1;
			return coef * 5 * ActualScore.CompareTo(ProjectedScore);
		}
	}
	
	public void SetMarginMod(double otherScore)
	{
		var diff = Math.Abs(ActualScore - otherScore);
		double marginCoef;
		if(diff <= 5)
			marginCoef = (diff - 5) / 5;
		else
			marginCoef = Math.Pow((diff - 5) / 40, 1.5);
			
		if(marginCoef >= 1)
			ScoreMarginMod = 5;
		else
			ScoreMarginMod = 5 * marginCoef * ActualScore.CompareTo(otherScore);
	}
	public void CalcChange(bool won)
	{
		ChangeModified = Convert.ToInt32(32 * ((won ? 1 : 0) - Expected) + ProjectedMod + ScoreMarginMod);
		Change = Convert.ToInt32(32 * ((won ? 1 : 0) - Expected));
	}
}