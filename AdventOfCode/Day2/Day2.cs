namespace AdventOfCode
{
    public class Day2 : IDay
    {
        List<Report> m_Reports = new List<Report>();
        int m_SafeReportNumber = 0;

        public void DisplayResult()
        {
            Console.WriteLine(m_SafeReportNumber);
        }

        public void Run()
        {
            ReadInput();
            ComputeSafeReports();
        }

        private void ReadInput()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(basePath, "..", "..", "..", "Day2", "input.txt");
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                m_Reports.Add(new Report(line));
            }
        }

        private void ComputeSafeReports()
        {
            foreach (Report report in m_Reports)
            {
                m_SafeReportNumber += report.IsSafe() ? 1 : 0;
            }
        }

        private class Report
        {
            List<int> m_Levels = new List<int>();

            public Report(string reportLine)
            {
                foreach (string level in reportLine.Split(" "))
                {
                    m_Levels.Add(int.Parse(level));
                }
            }

            public bool IsSafe()
            {
                bool isIncreasing = m_Levels[1] > m_Levels[0];

                for (int i = 0; i < m_Levels.Count - 1; i++)
                {
                    int levelDifference = m_Levels[i] - m_Levels[i + 1];
                    
                    if (Math.Abs(levelDifference) is < 1 or > 3 || levelDifference == 0)
                    {
                        return false;
                    }

                    if ((isIncreasing && levelDifference > 0) || (isIncreasing == false && levelDifference < 0))
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}