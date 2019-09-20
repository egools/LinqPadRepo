<Query Kind="Program" />

void Main()
{
	
}
public class Resp
{
	public List<Quota> Quotas;
}

public class Quota
{
	public string Name;
	public List<QuotaValue> Values;
}

public class QuotaValue
{
	public int Value;
	public int Max;
	public int Completes;
	public bool IsOpen => Completes < Max;
}