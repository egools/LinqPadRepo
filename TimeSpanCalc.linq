<Query Kind="Statements" />

TimeSpan bedTime = new TimeSpan(7, 0, 0);
TimeSpan wakeTime = new TimeSpan(8, 60, 0);
TimeSpan TimeInBed = wakeTime.Subtract(bedTime);

if (TimeInBed.Ticks < 0)
{
	wakeTime = wakeTime.Add(new TimeSpan(1, 0, 0, 0));
}
TimeInBed = wakeTime.Subtract(bedTime);

bedTime.Dump();
wakeTime.Dump();
TimeInBed.Dump();

bedTime.TotalMinutes.Dump();
wakeTime.TotalMinutes.Dump();
TimeInBed.TotalMinutes.Dump();

bedTime.TotalHours.ToString("0.000").Dump();
wakeTime.TotalHours.ToString("0.000").Dump();
TimeInBed.TotalHours.ToString("0.000").Dump();