namespace AdventOfCode
{
    public class Day2 : IDay
    {
        private const int ms_part = 2;

        private List<Report> m_Reports = new List<Report>();
        private int m_SafeReportNumber = 0;

        public void DisplayResult()
        {
            Console.WriteLine(m_SafeReportNumber);
        }

        public void Run()
        {
            ReadInput();

            switch (ms_part)
            {
                case 1:
                    ComputeSafeReports();
                    break;
                case 2:
                    ComputeSafeReportsWithDampener();
                    break;
                default:
                    Console.WriteLine("¯\\_(--')_/¯");
                    break;
            }
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
                m_SafeReportNumber += report.IsSafe().safe ? 1 : 0;
            }
        }

        private void ComputeSafeReportsWithDampener()
        {
            foreach (Report report in m_Reports)
            {
                m_SafeReportNumber += report.IsSafeWithDampener() ? 1 : 0;
            }
        }

        private class Report
        {
            List<int> m_Levels = new List<int>();

            public struct SafetyReport
            {
                public int faultyIndex;
                public bool safe;
            }

            public Report(string reportLine)
            {
                foreach (string level in reportLine.Split(" "))
                {
                    m_Levels.Add(int.Parse(level));
                }
            }

            public Report(List<int> levels)
            {
                m_Levels = levels;
            }

            public SafetyReport IsSafe()
            {
                bool isIncreasing = m_Levels[1] > m_Levels[0];

                for (int i = 0; i < m_Levels.Count - 1; i++)
                {
                    int levelDifference = m_Levels[i] - m_Levels[i + 1];

                    if (Math.Abs(levelDifference) is < 1 or > 3 || levelDifference == 0)
                    {
                        return new SafetyReport { faultyIndex = i, safe = false };
                    }

                    if ((isIncreasing && levelDifference > 0) || (isIncreasing == false && levelDifference < 0))
                    {
                        return new SafetyReport { faultyIndex = i, safe = false };
                    }
                }

                return new SafetyReport { faultyIndex = -1, safe = true };
            }

            public bool IsSafeWithDampener()
            {
                var safetyCheck = this.IsSafe();
                if (safetyCheck.safe)
                    return true;

                int faultyIndex = safetyCheck.faultyIndex;
                List<int> modifiedLevels;

                for (int i = Math.Max(0, faultyIndex - 1); i <= Math.Min(faultyIndex + 1, m_Levels.Count - 1); i++)
                {
                    modifiedLevels = new List<int>(m_Levels);
                    modifiedLevels.RemoveAt(i);
                    Report modifiedReport = new Report(modifiedLevels);
                    if (modifiedReport.IsSafe().safe)
                        return true;
                }

                return false;
            }
        }
    }
}