<Query Kind="Program" />

void Main()
{
	var h = new int[] {1, 3, 1, 3, 1, 4, 1, 3, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 7};
	
	designerPdfViewer(h, "zaba").Dump();
}

// Define other methods and classes here
static int designerPdfViewer(int[] h, string word) {
	var i = 0;
	var dict = h.ToDictionary(height => Convert.ToChar(97 + i++), height => height);	
	return word.Max(c => dict[c]) * word.Length;
}
