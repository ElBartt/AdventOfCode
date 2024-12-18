using AdventOfCode;

internal class Program
{
    private const int DAY = 6;
    private const int PART = 2;

    private static void Main(string[] args)
    {
        IDay day = DayFactory.GetDay(DAY);
        if (day == null)
            return;

        day.Run(PART);
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
            case 3:
                return new Day3();
            case 4:
                return new Day4();
            case 5:
                return new Day5();
            case 6:
                return new Day6();
            default:
                Console.WriteLine("Missing day code!");
                return null;
        }
    }
}

public interface IDay
{
    public void Run(int part);
    public void DisplayResult();
}

public abstract class Day : IDay
{
    public abstract void DisplayResult();

    protected abstract void ReadInput();
    protected abstract void SolvePart1();
    protected abstract void SolvePart2();

    public void Run(int part)
    {
        ReadInput();

        switch (part)
        {
            case 1:
                SolvePart1();
                break;
            case 2:
                SolvePart2();
                break;
            default:
                Console.WriteLine("¯\\_(--')_/¯");
                break;
        }
    }
}
