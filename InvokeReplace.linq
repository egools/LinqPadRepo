<Query Kind="Program" />

void Main()
{
	var q = new CheckOne();
	q.Text = "stuff [insert] stuff";
	
	Console.WriteLine(q.Text);
	q.ReplaceText2("[insert]", "and");
	Console.WriteLine(q.Text);
}

// Define other methods and classes here

public static class F {
	public static void ReplaceText(this IQuestion question, string oldVal, string newVal)
	{
	    PropertyInfo qtext = question.GetType().GetProperty("Text");
		object propertyVal = qtext.GetValue(question, null);
	    MethodInfo info = typeof(string).GetMethod("Replace", new Type[] {typeof(string), typeof(string)});
		object val = info.Invoke(propertyVal, new object[] {oldVal, newVal});
		qtext.SetValue(question, val);
	}
	
	public static void ReplaceText2(this IQuestion question, string oldVal, string newVal)
	{
	    ((dynamic)question).Text = ((dynamic)question).Text.Replace(oldVal, newVal);
	}

}
public interface IQuestion
{
}

public class CheckOne : IQuestion
{
	public string Text {get; set;}
}