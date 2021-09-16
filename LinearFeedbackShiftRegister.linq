<Query Kind="Program">
  <Reference Relative="..\..\..\.nuget\packages\newtonsoft.json\10.0.2\lib\netstandard1.3\Newtonsoft.Json.dll">&lt;NuGet&gt;\newtonsoft.json\10.0.2\lib\netstandard1.3\Newtonsoft.Json.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	var a = 0b1001;
	var b = a;
	Convert.ToString(b, 2).PadLeft(4, '0').Dump();
	do
	{
		var newbit = (b ^ (b >> 1)) & 1;
		b = (b >> 1) | (newbit << 3);
		Convert.ToString(b, 2).PadLeft(4, '0').Dump();
	} while(a != b);
}

// You can define other methods, fields, classes and namespaces here
