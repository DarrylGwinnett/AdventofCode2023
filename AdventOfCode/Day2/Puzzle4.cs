namespace AdventOfCode.Day2
{
    public class Puzzle4
    {
        [SetUp]
        public void Setup()
        {
        }

        private string[] input = File.ReadAllLines("./Data/Day2-input.txt");
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
                sum += GetCubePower(line);
            });
            Console.WriteLine(sum);
        }

        private static int GetCubePower(string line)
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
                Rounds = rounds

            };
            var power = game.FewestRequiredGreens() * game.FewestRequiredBlues() * game.FewestRequiredReds();
            return power;

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
            public List<Round> Rounds { get; set; }

            public int FewestRequiredGreens()
            {
                return GetFewestRequiredColor(Rounds, round => round.Green);
            }

            public int FewestRequiredBlues()
            {
                return GetFewestRequiredColor(Rounds, round => round.Blue);
            }

            public int FewestRequiredReds()
            {
                return GetFewestRequiredColor(Rounds, round => round.Red);
            }

            private int GetFewestRequiredColor(IEnumerable<Round> rounds, Func<Round, int> colorSelector)
            {
                int fewestRequired = 0;
                foreach (var round in rounds)
                {
                    int currentColorCount = colorSelector(round);
                    if (currentColorCount > fewestRequired)
                    {
                        fewestRequired = currentColorCount;
                    }
                }
                return fewestRequired;
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