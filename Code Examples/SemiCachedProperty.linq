<Query Kind="Program" />

void Main()
{
	someValue = "1";
	Country.Dump();
	Country.Dump();
}

// Define other methods and classes here
public static string someValue;
public static string GetData()
{
	"Accessed".Dump();
	return someValue;
}
public static void WriteOutData(string val)
{
	someValue = val;
}

private static string _country;
public static string Country
{
    get 
	{ 
		if(_country != null)
			return _country;
		else
		{
			_country = GetData();
			return _country;
		}
				
	}
    set
    {
        _country = value;
        WriteOutData(value);
    }
}