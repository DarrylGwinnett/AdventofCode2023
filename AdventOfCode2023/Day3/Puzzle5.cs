using System.Collections.Generic;

namespace AdventOfCode.Day3;

public class Puzzle5
{
    [SetUp]
    public void Setup()
    {
    }

    private string[] input = File.ReadAllLines("./Day3/Day3-input.txt");
    [Test]
    public void Test1()
    {
        var sum = 0;
        List<Number> numbers = new List<Number>();
        var lines = input.ToList();

        for (int lineNumber = 0; lineNumber < lines.Count; lineNumber++)
        {
            var chars = lines[lineNumber].ToCharArray().ToList();
            for (int i = 0; i < chars.Count; i++)
            {
                if (chars[i] == '.') continue;
                if (char.IsDigit(chars[i]))
                {
                    var numberValue = GetFullNumber(chars, i);
                    var number = new Number()
                    {
                        Value = numberValue,
                        X = i,
                        Y = lineNumber,
                        isPartNumber = isPartNumber(numberValue, i, lineNumber)

                    };
                    numbers.Add(number);
                    if(numberValue.ToString().Length > 1)
                    {
                        i += numberValue.ToString().Length - 1;
                    }
                }
            }
        }
        var partNumbers = numbers.Where(item => item.isPartNumber).ToList();
        partNumbers.ForEach(part => sum += part.Value);
        Console.WriteLine(sum);
    }

    private bool isPartNumber(int number, int X, int Y)
    {
        var firstLine = Y == 0 ? 0 : Y - 1;
        var linesToEnd = input.Length - Y;
        var lastLineOffset = linesToEnd < 3 ? linesToEnd : 3;
        List<string> adjacentLines = input.ToList().GetRange(firstLine, lastLineOffset);
        var lineLength = adjacentLines.First().Length;
        int outerLeft = X == 0 ? 0 : X - 1;
        int outerRight = X + number.ToString().Length == lineLength ? lineLength : X + number.ToString().Length + 1;
        List<char> allRelevantChars = new List<char>();
        adjacentLines.ForEach(line =>
        {
            var lengthIndex = outerRight-outerLeft;
            var chars = line.ToCharArray().ToList();
            allRelevantChars.AddRange(chars.GetRange(outerLeft, lengthIndex));

        });
        var symbols = allRelevantChars.Where(c =>
        {
            return (!char.IsDigit(c) && c != '.');
        });
        return symbols.Any();
    }

    private int GetFullNumber(List<char> chars, int startPosition)
    {
        string num = "";
        for (int i = startPosition; i < chars.Count; i++)
        {
            if (char.IsDigit(chars[i]))
            {
                num += chars[i];
            }
            else break;
        }
        return int.Parse(num);
    }

    class Number
    {
        public int Y { get; set; }
        public int X { get; set; }
        public int Value { get; set; }

        public bool isPartNumber { get; set; }
    }

}