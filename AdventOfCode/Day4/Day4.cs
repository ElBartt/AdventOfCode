using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day4 : Day
    {
        private int m_XmasCount = 0;

        private List<List<char>> m_XmasMatrix = new List<List<char>>();
        private List<char> m_Xmas = ['X', 'M', 'A', 'S'];

        public override void DisplayResult()
        {
            Console.WriteLine(m_XmasCount);
        }

        protected override void ReadInput()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(basePath, "..", "..", "..", "Day4", "input.txt");
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                m_XmasMatrix.Add(line.ToList());
            }
        }

        protected override void SolvePart1()
        {
            List<Tuple<int, int>> lookUpTable = [
                new (-1, 1),
                new (0, 1),
                new (1, 1),
                new (-1, -1),
                new (0, -1),
                new (1, -1),
                new (-1, 0),
                new (1, 0)
            ];

            m_XmasCount = 0;
            int columnLength = m_XmasMatrix.Count;
            int rowLength = m_XmasMatrix[0].Count;

            for (int y = 0; y < columnLength; y++)
            {
                for (int x = 0; x < rowLength; x++)
                {
                    if (m_XmasMatrix[y][x] != 'X')
                        continue;

                    foreach (Tuple<int, int> direction in lookUpTable)
                    {
                        Tuple<int, int> currentPosition = new(y, x);
                        Tuple<int, int> nextPosition = direction;

                        bool foundCompleteWord = true;
                        foreach (char xmasChar in m_Xmas.Skip(1))
                        {
                            if (TryFindLetterAtDirection(xmasChar, currentPosition, nextPosition) == false)
                            {
                                foundCompleteWord = false;
                                break;
                            }

                            currentPosition = new(currentPosition.Item1 + direction.Item1, currentPosition.Item2 + direction.Item2);
                        }

                        if (foundCompleteWord)
                            m_XmasCount++;
                    }
                }
            }
        }

        protected override void SolvePart2()
        {
            m_XmasCount = 0;
            int columnLength = m_XmasMatrix.Count;
            int rowLength = m_XmasMatrix[0].Count;

            for (int y = 1; y < columnLength - 1; y++)
            {
                for (int x = 1; x < rowLength - 1; x++)
                {
                    if (m_XmasMatrix[y][x] != 'A')
                        continue;

                    bool topLeftToBottomRight =
                        (IsLetter(y - 1, x - 1, 'M') && IsLetter(y + 1, x + 1, 'S')) ||
                        (IsLetter(y - 1, x - 1, 'S') && IsLetter(y + 1, x + 1, 'M'));

                    bool topRightToBottomLeft =
                        (IsLetter(y - 1, x + 1, 'M') && IsLetter(y + 1, x - 1, 'S')) ||
                        (IsLetter(y - 1, x + 1, 'S') && IsLetter(y + 1, x - 1, 'M'));

                    if (topLeftToBottomRight && topRightToBottomLeft)
                    {
                        m_XmasCount++;
                    }
                }
            }
        }

        private bool IsLetter(int y, int x, char letter)
        {
            if (IsValidPosition(x, y) == false)
                return false;

            return m_XmasMatrix[y][x] == letter;
        }

        private bool TryFindLetterAtDirection(char letter, Tuple<int, int> position, Tuple<int, int> direction)
        {
            int yPositionToSearch = direction.Item1 + position.Item1;
            int xPositionToSearch = direction.Item2 + position.Item2;

            if (IsValidPosition(xPositionToSearch, yPositionToSearch) == false)
                return false;

            return m_XmasMatrix[yPositionToSearch][xPositionToSearch] == letter;
        }

        private bool IsValidPosition(int x, int y)
        {
            if (y < 0 || y >= m_XmasMatrix.Count)
                return false;

            if (x < 0 || x >= m_XmasMatrix[0].Count)
                return false;

            return true;
        }
    }
}