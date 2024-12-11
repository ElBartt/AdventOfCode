using System.Reflection.Metadata;
using AdventOfCode;

internal class Program
{
    private const int DAY = 1;

    private static void Main(string[] args)
    {
        IDay day = DayFactory.GetDay(DAY);
        if (day == null)
            return;

        day.Run();
        day.DisplayResult();
    }
}

public static class DayFactory
{
    public static IDay GetDay(int day)
    {
        switch (day)
        {
            case 1:
                return new Day1();
            default:
                return null;
        }
    }
}

public interface IDay
{
    public void Run();
    public void DisplayResult();
}