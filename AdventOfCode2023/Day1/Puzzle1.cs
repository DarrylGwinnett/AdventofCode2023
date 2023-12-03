namespace AdventOfCode.Day1
{
    public class Puzzle1
    {
        [SetUp]
        public void Setup()
        {
        }

        private string[] input = File.ReadAllLines("./Day1-input.txt");

        [Test]
        public void Test1()
        {
            var sum = 0;
            var lines = input.ToList();
            lines.ForEach(line =>
            {
                sum += SumOfFirstAndLastDigit(line);
            });
            Console.WriteLine(sum);
        }

        private static int SumOfFirstAndLastDigit(string line)
        {
            var chars = line.ToCharArray().ToList();
            int first = 0;
            chars.First(item => int.TryParse(item.ToString(), out first));

            int last = 0;
            chars.Last(item => int.TryParse(item.ToString(), out last));

            var sum = first.ToString() + last.ToString();
            return int.Parse(sum);
        }

    }
}