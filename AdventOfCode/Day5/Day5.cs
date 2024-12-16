using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day5 : Day
    {
        private int m_MiddlePageNumberTotal = 0;

        private Dictionary<int, List<int>> m_PageOrderingRules = [];
        private List<List<int>> m_Updates = [];

        private const char m_OrderSeparator = '|';
        private const char m_PrintSeparator = ',';

        public override void DisplayResult()
        {
            Console.WriteLine(m_MiddlePageNumberTotal);
        }

        protected override void ReadInput()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(basePath, "..", "..", "..", "Day5", "input.txt");
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                if (line.Contains(m_OrderSeparator))
                {
                    string[] pagesOrder = line.Split(m_OrderSeparator);
                    int key = int.Parse(pagesOrder[0]);
                    m_PageOrderingRules[key] = m_PageOrderingRules.GetValueOrDefault(key, []);
                    m_PageOrderingRules[key].Add(int.Parse(pagesOrder[1]));
                }

                if (line.Contains(m_PrintSeparator))
                {
                    m_Updates.Add(line.Split(m_PrintSeparator).Select(update => int.Parse(update)).ToList());
                }
            }
        }

        protected override void SolvePart1()
        {
            ProcessUpdate(false);
        }

        protected override void SolvePart2()
        {
            ProcessUpdate(true);
        }

        private void ProcessUpdate(bool reorder)
        {
            foreach (List<int> update in m_Updates)
            {
                bool legit = true;

                for (int i = 0; i < update.Count; i++)
                {
                    int pageNumber = update[i];
                    bool wrongOrdering = false;

                    for (int y = 0; y < i; y++)
                    {
                        if (m_PageOrderingRules.ContainsKey(pageNumber) && m_PageOrderingRules[pageNumber].Contains(update[y]))
                        {
                            wrongOrdering = true;
                            legit = false;

                            if (reorder)
                            {
                                int valueToMove = update[i];
                                update.RemoveAt(i);
                                update.Insert(i - 1, valueToMove);
                            }

                            break;
                        }
                    }

                    if (wrongOrdering)
                    {
                        if (reorder)
                        {
                            i = -1;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (reorder ^ legit)
                    m_MiddlePageNumberTotal += update[update.Count / 2];
            }
        }
    }
}