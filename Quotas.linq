<Query Kind="Program" />

void Main()
{
	
	
}
public class Resp
{
	public Dictionary<string, string> Quotas;
}

public class Quota
{
	public string Name;
	public List<QuotaValue> QuotaValues;
	public QuotaValue this[string val] => QuotaValues.FirstOrDefault(v => v.Value == val);
}

public class QuotaValue
{
	public string Value;
	public int Max;
	public int Completes;
	public bool IsOpen => Completes < Max;
}