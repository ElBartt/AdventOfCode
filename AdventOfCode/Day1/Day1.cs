namespace AdventOfCode
{
    public class Day1 : Day
    {
        private List<int> m_leftList = [];
        private List<int> m_rightList = [];

        private int m_result = 0;

        public override void DisplayResult()
        {
            Console.WriteLine(m_result);
        }

        protected override void ReadInput()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(basePath, "..", "..", "..", "Day1", "input.txt");
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] values = line.Split([' '], StringSplitOptions.RemoveEmptyEntries);

                if (values.Length != 2)
                {
                    Console.WriteLine("Missing one location ID.");
                    return;
                }

                m_leftList.Add(int.Parse(values[0]));
                m_rightList.Add(int.Parse(values[1]));
            }

            if (m_leftList.Count != m_rightList.Count)
            {
                // Should not happened
                Console.WriteLine("List do not have the same length.");
                return;
            }
        }

        protected override void SolvePart1()
        {
            m_leftList.Sort();
            m_rightList.Sort();

            for (int i = 0; i < m_leftList.Count; i++)
            {
                m_result += Math.Abs(m_leftList[i] - m_rightList[i]);
            }
        }

        protected override void SolvePart2()
        {
            m_leftList.Sort();
            m_rightList.Sort();

            int previousLocationId = -1;
            int previousSimilarityScore = -1;
            foreach (int leftLocationId in m_leftList)
            {
                if (leftLocationId == previousLocationId)
                {
                    m_result += previousSimilarityScore;
                    continue;
                }

                int hit = m_rightList.Count(locationId => locationId == leftLocationId);
                int similarityScore = leftLocationId * hit;

                m_result += similarityScore;

                previousSimilarityScore = similarityScore;
                previousLocationId = leftLocationId;
            }
        }
    }
}