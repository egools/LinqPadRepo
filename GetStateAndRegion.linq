<Query Kind="Program" />

void Main()
{
	var zips = new List<string>{"82615",
"98422",
"82716",
"98953",
"99020",
"98109",
"99360",
"82727",
"98109",
"98682",
"82073",
"98357",
"99153",
"82433",
"99185",
"82842",
"98586",
"98859"};		
	
	zips.ForEach(z => isLegit(z));
}

// Define other methods and classes here
public void isLegit(string zip)
{	
	var state = "";
    if (zip.IsWithin("35000:35299,35400:36999")) state = ("1");
    else if (zip.IsWithin("99500:99999")) state = ("2");
    else if (zip.IsWithin("85000:85399,85500:85799,85900:86099,86300:86599")) state = ("3");
    else if (zip.IsWithin("71600:72999")) state = ("4");
    else if (zip.IsWithin("90000:90899,91000:92899,93000:96199")) state = ("5");
    else if (zip.IsWithin("80000:81699")) state = ("6");
    else if (zip.IsWithin("06000:06389,06391:06999")) state = ("7");
    else if (zip.IsWithin("19700:19999")) state = ("8");
    else if (zip.IsWithin("20000:20099,20200:20587,56900:56999,20589:20597,20599")) state = ("9");
    else if (zip.IsWithin("32000:33999,34100:34199,34200:34299,34400:34499,34600:34699,34700:34799,34900:34999")) state = ("10");
    else if (zip.IsWithin("30000:31999,39800:39999")) state = ("11");
    else if (zip.IsWithin("96701:96798,96800:96899")) state = ("12");
    else if (zip.IsWithin("83200:83413,83415:83899")) state = ("13");
    else if (zip.IsWithin("60000:62099,62200:62999")) state = ("14");
    else if (zip.IsWithin("46000:47999")) state = ("15");
    else if (zip.IsWithin("50000:51699,52000:52899")) state = ("16");
    else if (zip.IsWithin("66000:66299,66400:67999")) state = ("17");
    else if (zip.IsWithin("40000:41899,42000:42799")) state = ("18");
    else if (zip.IsWithin("70000:70199,70300:70899,71000:71499")) state = ("19");
    else if (zip.IsWithin("03900:04999")) state = ("20");
    else if (zip.IsWithin("20588,20600:21299,21400:21999")) state = ("21");
    else if (zip.IsWithin("01000:02799,05500:05599")) state = ("22");
    else if (zip.IsWithin("48000:49999")) state = ("23");
    else if (zip.IsWithin("55000:55199,55300:56799")) state = ("24");
    else if (zip.IsWithin("38600:39799")) state = ("25");
    else if (zip.IsWithin("63000:63199,63300:64199,64400:65899")) state = ("26");
    else if (zip.IsWithin("59000:59999")) state = ("27");
    else if (zip.IsWithin("68000:68199,68300:69399")) state = ("28");
    else if (zip.IsWithin("88900:89199,89300:89599,89700:89899")) state = ("29");
    else if (zip.IsWithin("03000:03899")) state = ("30");
    else if (zip.IsWithin("07000:08999")) state = ("31");
    else if (zip.IsWithin("87000:87199,87300:88499")) state = ("32");
    else if (zip.IsWithin("00500:00599,06390,10000:14999")) state = ("33");
    else if (zip.IsWithin("27000:28999")) state = ("34");
    else if (zip.IsWithin("58000:58899")) state = ("35");
    else if (zip.IsWithin("43000:45999")) state = ("36");
    else if (zip.IsWithin("73000:73199,73400:73959,73961:74199,74300:74999")) state = ("37");
    else if (zip.IsWithin("97000:97999")) state = ("38");
    else if (zip.IsWithin("15000:19699")) state = ("39");
    else if (zip.IsWithin("02800:02899,02900:02999")) state = ("40");
    else if (zip.IsWithin("29000:29999")) state = ("41");
    else if (zip.IsWithin("57000:57799")) state = ("42");
    else if (zip.IsWithin("37000:38599")) state = ("43");
    else if (zip.IsWithin("73300:73399,73960,75000:77099,77200:79999,88500:88599")) state = ("44");
    else if (zip.IsWithin("84000:84799")) state = ("45");
    else if (zip.IsWithin("05000:05499,05600:05999")) state = ("46");
    else if (zip.IsWithin("20100:20199,20598,22000:24699")) state = ("47");
    else if (zip.IsWithin("98000:98699,98800:99499")) state = ("48");
    else if (zip.IsWithin("24700:26899")) state = ("49");
    else if (zip.IsWithin("53000:53299,53400:53599,53700:54999")) state = ("50");
    else if (zip.IsWithin("82000:83199,83414")) state = ("51");
	
	var region = "";
	if(state.IsWithin("7,20,22,30,31,33,39,40,46")) region = "1";
	else if(state.IsWithin("1,4,8,9,10,11,18,19,21,25,34,37,41,43,44,47,49")) region = "2";
	else if(state.IsWithin("14,15,16,17,23,24,26,28,35,36,42,50")) region = "3";
	else if(state.IsWithin("2,3,5,6,12,13,27,29,32,38,45,48,51")) region = "4";
	
	$"Zip:{zip}; State:{state}; Region:{region}".Dump();
}