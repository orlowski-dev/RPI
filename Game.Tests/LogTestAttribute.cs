using System.Reflection;
using Xunit.Sdk;

public class LogTestAttribute : BeforeAfterTestAttribute
{
    public override void Before(MethodInfo method)
    {
        var msg = $"{method.DeclaringType?.Name}:{method.Name} Starting..";
        Start(msg);
    }

    public override void After(MethodInfo method)
    {
        var msg = $"{method.DeclaringType?.Name}:{method.Name} OK.";
        Success(msg);
    }

    public static void Success(string msg)
    {
        Console.WriteLine($"\u001b[32m{msg}\u001b[0m");
    }

    public static void Start(string msg)
    {
        Console.WriteLine($"\u001b[33m{msg}\u001b[0m");
    }

    public static void Error(string msg)
    {
        Console.WriteLine($"\u001b[31m{msg}\u001b[0m");
    }
}
