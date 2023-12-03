
namespace AdventOfCode
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        private string[] input = File.ReadAllLines("./Data/Day1-input.txt");

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
            chars.First(item => Int32.TryParse(item.ToString(), out first));

            int last = 0;
            chars.Last(item => Int32.TryParse(item.ToString(), out last));

            var sum = first.ToString() + last.ToString();
            return Int32.Parse(sum);
        }

    }
}