
using System.Globalization;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode.Day1
{
    public class Puzzle2
    {

        private string[] input = File.ReadAllLines("./Day1-input.txt");
        private List<string> wordList = new List<string>()
        {
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine"

        };

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


        private int SumOfFirstAndLastDigit(string line)
        {
            int firstValue = FindFirstNumber(line);
            int lastValue = FindLastNumber(line);
            var sum = firstValue.ToString() + lastValue.ToString();
            return int.Parse(sum);
        }

        private int FindLastNumber(string line)
        {
            string reversedLine = Reverse(line);
            var lastNumResult = FindFirstInteger(reversedLine);
            List<string> reversedWordList = [];
            wordList.ForEach(word =>
            {
                reversedWordList.Add(Reverse(word));
            });
            var lastWordResult = FindFirstWord(reversedLine!, reversedWordList);

            var lastIndex = lastWordResult.Success && lastWordResult.Index < lastNumResult.Index ? lastWordResult.Number : lastNumResult.Number;
            return lastIndex;
        }

        private int FindFirstNumber(string line)
        {
            var firstWord = FindFirstWord(line, wordList);
            var firstNumber = FindFirstInteger(line);
            var firstValue = firstWord.Success && firstWord.Index < firstNumber.Index ? firstWord.Number : firstNumber.Number;
            return firstValue;
        }

        private static Result FindFirstInteger(string line)
        {
            var chars = line.ToCharArray().ToList();
            for (var i = 0; i < chars.Count; i++)
            {
                if (char.IsDigit(chars[i]))
                {
                    return new Result()
                    {
                        Success = true,
                        Number = int.Parse(char.ToString(chars[i])),
                        Index = i
                    };
                }
            }
            throw new InvalidDataException("Contained not numbers");
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        static Result FindFirstWord(string inputStr, List<string> testList)
        {
            string pattern = string.Join("|", testList);
            Regex regex = new Regex(pattern);
            Match match = regex.Match(inputStr);
            int number = -1;
            if (match.Success)
            {
                number = ConvertTextToNumber(match.Value);
            }
            return new Result()
            {
                Success = match.Success,
                Number = number,
                Index = match.Index
            };
        }

        static int ConvertTextToNumber(string text)
        {
            string[] numberStrings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            string formattedText = text.ToLower();

            int index = Array.IndexOf(numberStrings, formattedText);

            if (index != -1)
            {
                return index;
            }
            else
            {
                var reversed = Reverse(text);
                index = Array.IndexOf(numberStrings, reversed);
                if (index == -1) throw new InvalidDataException("Couldn't parse word " + text);
                return index;
            }
        }

    }



    internal class Result
    {
        public bool Success { get; set; }

        public int Number { get; set; } = default!;

        public int Index { get; set; } = default!;
    }
}