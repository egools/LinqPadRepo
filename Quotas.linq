<Query Kind="Program" />

void Main()
{
	var resps = new List<Resp>();
	var quotas = new List<Quota>
	{
		new Quota("Gender", new Dictionary<string, int> { {"1", 1837}, { "2", 1913 }}),
		new Quota("Age", new Dictionary<string, int> {{"1", 1350}, { "2", 1613 }, {"3", 787}}),
		new Quota("Region", new Dictionary<string, int> {{"1", 713}, { "2",  825}, {"3", 1350}, { "4", 862 }}),
		new Quota("Ethnicity", new Dictionary<string, int> {{"1", 750}, { "2", 750 }, { "3", 750 }, { "4", 750 }, { "5", 750 }}),
		new Quota("CFACustomer", new Dictionary<string, int> {{"1", 1500}, { "2", int.MaxValue }})
	};
	var version = new Quota("IRTVersion", new Dictionary<string, int> { {"1", 375},{"2", 375},{"3", 375},{"4", 375},{"5", 375},{"6", 375},{"7", 375},{"8", 375},{"9", 375},{"10", 375}});
	var versions = new Dictionary<string, int>();
	Random rand = new Random();
	for(int i = 1; i <= 3000; i++)
	{
		var r = new Resp();
		var isOpen = true;
		foreach(var q in quotas)
		{
			var val = rand.Next(1, q.QuotaValues.Count + 1).ToString();
			var qval = q[val];
			r.Quotas.Add(q.Name, qval.Value);
			if(!qval.IsOpen) isOpen = false;
		}
		
		if(isOpen)
		{
			foreach(var q in quotas)
			{
				q[r.Quotas[q.Name]].Completes++;
			}
			
			var open = version.QuotaValues.Where(qv => qv.IsOpen).Select(qv => qv.Value);
			var v = (from o in open
				select new 
				{
					Key = o,
					Count = resps.Count(resp => resp.Quotas["Gender"] == r.Quotas["Gender"] &&
						resp.Quotas["Age"] == r.Quotas["Age"] &&
						resp.Quotas["Region"] == r.Quotas["Region"] &&
						resp.Quotas["Ethnicity"] == r.Quotas["Ethnicity"] &&
						resp.Quotas["CFACustomer"] == r.Quotas["CFACustomer"] && resp.Quotas["IRTVersion"] == o), 
				});
				
			v = v.OrderBy(ver => ver.Count).ThenBy(ver => rand.Next());
			r.Quotas.Add("IRTVersion", v.First().Key);
			version[v.First().Key].Completes++;
				
			resps.Add(r);
		}
	}
	
	version.Dump();
	
	for(int i = 1; i <= 10; i++)
	{
		var sub = resps.Where(r => r.Quotas["IRTVersion"] == i.ToString());
		$@"IRTVersion={i} : {sub.Count()}".Dump();
		foreach(var q in quotas)
		{
			foreach(var qv in q.QuotaValues)
			{
				$"{q.Name}={qv.Value} : {sub.Count(r => r.Quotas[q.Name] == qv.Value)}".Dump();
			}
		}
		Console.WriteLine();
		
	}
}
public class Resp
{
	public Dictionary<string, string> Quotas;
	public Resp()
	{
		Quotas = new Dictionary<string, string>();
	}
}

public class Quota
{
	public string Name;
	public List<QuotaValue> QuotaValues;
	public QuotaValue this[string v] => QuotaValues.First(qv => qv.Value == v);
	
	public Quota(string name, Dictionary<string, int> valMaxes)
	{
		Name = name;
		QuotaValues = valMaxes.Select(v => new QuotaValue(v.Key, v.Value)).ToList();
	}
}

public class QuotaValue
{
	public string Value;
	public int Max;
	public int Completes;
	public bool IsOpen => Completes < Max;
	
	public QuotaValue(string val, int max)
	{
		Value = val;
		Max = max;
		Completes = 0;
	}
}