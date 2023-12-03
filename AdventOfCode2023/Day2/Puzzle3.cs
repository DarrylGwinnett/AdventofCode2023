namespace AdventOfCode.Day2
{
    public class Puzzle3
    {
        [SetUp]
        public void Setup()
        {
        }

        private string[] input = File.ReadAllLines("./Day2-input.txt");
        private const int redMax = 12;
        private const int blueMax = 14;
        private const int greenMax = 13;

        [Test]
        public void Test1()
        {
            var sum = 0;
            var lines = input.ToList();
            lines.ForEach(line =>
            {
                sum += GetValidIds(line);
            });
            Console.WriteLine(sum);
        }

        private static int GetValidIds(string line)
        {
            var splitOnName = line.Split(':');
            var name = splitOnName[0];
            var id = name.Split(' ')[1];
            var roundStrings = splitOnName[1].Split(';').ToList();
            List<Round> rounds = new List<Round>();
            roundStrings.ForEach(roundString =>
            {
                var components = roundString.Split(',');
                var round = new Round();
                round.Green = FindColourCount(components, "green");
                round.Blue = FindColourCount(components, "blue");
                round.Red = FindColourCount(components, "red");
                rounds.Add(round);
            });
            var game = new Game()
            {
                Rounds = rounds,
                Name = name,
                Id = int.Parse(id)

            };
            if (!game.IsValid()) return 0;
            return game.Id;

        }

        private static int FindColourCount(string[] components, string colour)
        {
            var matchingComponents = components.Where(component => component.Contains(colour));
            if (matchingComponents.Count() == 0) { return 0; };
            var component = matchingComponents.Single();
            var count = component.Split(' ')[1];
            return int.Parse(count);

        }


        public class Game
        {
            public string Name { get; set; }

            public List<Round> Rounds { get; set; }

            public int Id { get; set; }

            public bool IsValid()
            {
                return !HasExceededMaxColorCount(Rounds, round => round.Blue, blueMax) &&
                       !HasExceededMaxColorCount(Rounds, round => round.Green, greenMax) &&
                       !HasExceededMaxColorCount(Rounds, round => round.Red, redMax);
            }
            private bool HasExceededMaxColorCount(IEnumerable<Round> rounds, Func<Round, int> colorSelector, int max)
            {
                return rounds.Any(round => colorSelector(round) > max);
            }

        }

        public class Round
        {
            public int Red { get; set; }
            public int Green { get; set; }

            public int Blue { get; set; }
        }
    }

}