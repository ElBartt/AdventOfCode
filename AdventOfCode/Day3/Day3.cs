using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Day3 : Day
    {
        private int m_Result = 0;

        private Regex m_MulRegex = new(@"mul\((\d{1,3}),(\d{1,3})\)");
        private Regex m_DoRegex = new(@"do\(\)");
        private Regex m_DontRegex = new(@"don't\(\)");

        private MatchCollection m_MulMatches = Regex.Matches("", "");
        private MatchCollection m_DoMatches = Regex.Matches("", "");
        private MatchCollection m_DontMatches = Regex.Matches("", "");

        public override void DisplayResult()
        {
            Console.WriteLine(m_Result);
        }

        protected override void ReadInput()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(basePath, "..", "..", "..", "Day3", "input.txt");
            string[] lines = File.ReadAllLines(filePath);
            string fullInput = string.Join("\n", lines);

            m_MulMatches = m_MulRegex.Matches(fullInput);
            m_DoMatches = m_DoRegex.Matches(fullInput);
            m_DontMatches = m_DontRegex.Matches(fullInput);
        }

        protected override void SolvePart1()
        {
            m_Result = 0;

            foreach (Match match in m_MulMatches)
            {
                GroupCollection groups = match.Groups;
                m_Result += int.Parse(groups[1].Value) * int.Parse(groups[2].Value);
            }
        }

        protected override void SolvePart2()
        {
            m_Result = 0;

            SortedDictionary<int, bool> instructions = new SortedDictionary<int, bool>();
            foreach (Match match in m_DoMatches)
            {
                instructions[match.Index] = true;
            }

            foreach (Match match in m_DontMatches)
            {
                instructions[match.Index] = false;
            }
            
            foreach (Match match in m_MulMatches)
            {
                GroupCollection groups = match.Groups;
                int groupsIndex = groups[0].Index;

                KeyValuePair<int, bool> instruction = instructions.Where(x => x.Key < groupsIndex).LastOrDefault();

                bool enabled = instruction.Key == 0 ? true : instruction.Value;
                if (enabled)
                {
                    m_Result += int.Parse(groups[1].Value) * int.Parse(groups[2].Value);
                }
            }
        }
    }
}