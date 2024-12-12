using AdventOfCode;

internal class Program
{
    private const int DAY = 2;

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
            case 2:
                return new Day2();
            default:
                Console.WriteLine("Missing day code!");
                return null;
        }
    }
}

public interface IDay
{
    public void Run();
    public void DisplayResult();
}