namespace AdventOfCode
{
    public class Day1 : IDay
    {
        private const int ms_part = 2;

        private List<int> m_leftList = [];
        private List<int> m_rightList = [];

        private int m_result = -1;

        public void Run()
        {
            ReadLists();

            switch (ms_part)
            {
                case 1:
                    ComputeTotalDistance();
                    break;
                case 2:
                    ComputeSimilarityScore();
                    break;
                default:
                    Console.WriteLine("¯\\_(--')_/¯");
                    break;
            }
        }

        public void DisplayResult()
        {
            Console.WriteLine(m_result);
        }

        private void ReadLists()
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

        private void ComputeTotalDistance()
        {
            m_leftList.Sort();
            m_rightList.Sort();
            m_result = 0;

            for (int i = 0; i < m_leftList.Count; i++)
            {
                m_result += Math.Abs(m_leftList[i] - m_rightList[i]);
            }
        }

        private void ComputeSimilarityScore()
        {
            m_leftList.Sort();
            m_rightList.Sort();
            m_result = 0;

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