<Query Kind="Program" />

void Main()
{
	LetterChange("hello*3");
	LetterChange("Thousands of Moscatos!");
	LetterChange("fun times!");
}

// Define other methods and classes here
public static void LetterChange(string s)
{
	var bytes = s.Select(b => ShiftByte((byte)b)).ToArray();
	var output = Encoding.ASCII.GetString(bytes);
	
	Console.WriteLine(output);
}

public static byte ShiftByte(byte b)
{
	var vowels = new byte[] {97, 101, 105, 111, 117};
	var temp = (byte)(b + 1);
	
	if((b >= 65 && b <= 90) || b >= 97 && b <= 122)			
		return vowels.Contains(temp) ? (byte)(temp - 32) : temp;
	else
		return b;
	
}