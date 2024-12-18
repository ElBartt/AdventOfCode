using System.Drawing;

public class Day6 : Day
{
    private enum Direction
    {
        NORTH,
        EAST,
        SOUTH,
        WEST
    }

    private const char m_Obstacle = '#';
    private const char m_FreeSpace = '.';
    private const char m_VisitedSpace = 'X';

    private List<List<char>> m_Map = [];
    private Point m_PlayerPosition = new(-1, -1);
    private Direction m_CurrentDirection = Direction.NORTH;
    private int m_VisitedPlace = 0;

    private record struct PositionDirection(Point Position, Direction Direction);

    public override void DisplayResult()
    {
        Console.WriteLine(m_VisitedPlace);
    }

    protected override void ReadInput()
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(basePath, "..", "..", "..", "Day6", "input.txt");
        string[] lines = File.ReadAllLines(filePath);
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            m_Map.Add(line.ToList());

            if (m_PlayerPosition.X == -1)
            {
                int playerIndex = line.IndexOf('^');
                if (playerIndex != -1)
                {
                    m_PlayerPosition = new(playerIndex, i);
                    m_Map[i][playerIndex] = '.';
                }
            }
        }
    }

    protected override void SolvePart1()
    {
        int x = 0;
        int y = -1;

        while (IsNextPositionValid(x, y))
        {
            int nextXposition = m_PlayerPosition.X + x;
            int nextYposition = m_PlayerPosition.Y + y;
            char nextPlace = m_Map[nextYposition][nextXposition];

            if (nextPlace.Equals(m_Obstacle))
            {
                ComputeNextDirection();
                ComputeNewOffset(ref x, ref y);
            }
            else
            {
                if (nextPlace.Equals(m_FreeSpace))
                {
                    m_Map[nextYposition][nextXposition] = m_VisitedSpace;
                    m_VisitedPlace += 1;
                }

                m_PlayerPosition.X = nextXposition;
                m_PlayerPosition.Y = nextYposition;
            }
        }
    }

    private void ComputeNextDirection()
    {
        switch (m_CurrentDirection)
        {
            case Direction.NORTH:
                m_CurrentDirection = Direction.EAST;
                break;
            case Direction.EAST:
                m_CurrentDirection = Direction.SOUTH;
                break;
            case Direction.SOUTH:
                m_CurrentDirection = Direction.WEST;
                break;
            case Direction.WEST:
                m_CurrentDirection = Direction.NORTH;
                break;
            default:
                Console.Write("Unknown direction");
                break;
        }
    }
    private void ComputeNewOffset(ref int x, ref int y)
    {
        switch (m_CurrentDirection)
        {
            case Direction.NORTH:
                x = 0;
                y = -1;
                break;
            case Direction.EAST:
                x = 1;
                y = 0;
                break;
            case Direction.SOUTH:
                x = 0;
                y = 1;
                break;
            case Direction.WEST:
                x = -1;
                y = 0;
                break;
            default:
                Console.Write("Unknown direction");
                break;
        }
    }

    private bool IsNextPositionValid(int x, int y)
    {
        if (m_PlayerPosition.X + x < 0 || m_PlayerPosition.X + x >= m_Map[0].Count ||
            m_PlayerPosition.Y + y < 0 || m_PlayerPosition.Y + y >= m_Map.Count)
        {
            return false;
        }

        return true;
    }

    protected override void SolvePart2()
    {
        int possiblePositions = 0;
        Point originalStart = m_PlayerPosition;

        for (int y = 0; y < m_Map.Count; y++)
        {
            for (int x = 0; x < m_Map[0].Count; x++)
            {
                if (x == originalStart.X && y == originalStart.Y)
                    continue;

                if (m_Map[y][x] != m_FreeSpace)
                    continue;

                // Add temporary obstacle
                m_Map[y][x] = m_Obstacle;
                m_PlayerPosition = originalStart;
                m_CurrentDirection = Direction.NORTH;

                if (CreatesLoop(m_Map))
                    possiblePositions++;

                m_Map[y][x] = m_FreeSpace;
            }
        }

        m_VisitedPlace = possiblePositions;
    }

    private bool CreatesLoop(List<List<char>> map)
    {
        HashSet<PositionDirection> visited = new HashSet<PositionDirection>();
        int x = 0, y = -1;

        while (IsNextPositionValid(x, y))
        {
            int nextX = m_PlayerPosition.X + x;
            int nextY = m_PlayerPosition.Y + y;
            PositionDirection currentState = new PositionDirection(m_PlayerPosition, m_CurrentDirection);

            if (visited.Contains(currentState))
                return true;

            visited.Add(currentState);

            if (map[nextY][nextX] == m_Obstacle)
            {
                ComputeNextDirection();
                ComputeNewOffset(ref x, ref y);
            }
            else
            {
                m_PlayerPosition = new Point(nextX, nextY);
            }
        }

        return false;
    }
}
