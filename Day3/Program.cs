using System.Text.RegularExpressions;

namespace Day3;

class Program
{
    static void Main(string[] args)
    {
        Day3 test = new Day3();
        test.Process("test.txt");
        test.Answer();

        Day3 test2 = new Day3();
        test2.Process("test2.txt");
        test2.Answer2();

        Day3 actual = new Day3();
        actual.Process("input.txt");
        actual.Answer();

        Day3 actual2 = new Day3();
        actual2.Process("input.txt");
        actual2.Answer2();
    }
}

class Day3
{
    public Day3()
    {
    }

    List<string> input = new List<string>();
    public void Process(string file)
    {
        input = new List<string>();
        foreach (string line in File.ReadLines(file))
        {
            if (String.IsNullOrEmpty(line))
            {
                continue;
            }

            input.Add(line);
        }
    }

    public void Answer()
    {
        long multiplications = 0;
        Regex regex= new Regex(@"(mul\((\d+),(\d+)\))");
        foreach (string line in input)
        {
            foreach(Match match in regex.Matches(line))
            {
                multiplications += (Int32.Parse(match.Groups[2].Value) * Int32.Parse(match.Groups[3].Value));
            }
        }

        Console.WriteLine($"Multiplication: {multiplications}");
    }

    public void Answer2()
    {
        bool enabled = true;
        long multiplications = 0;
        Regex regex= new Regex(@"(do\(\))|(don't\(\))|(mul\((\d+),(\d+)\))");
        foreach (string line in input)
        {
            foreach(Match match in regex.Matches(line))
            {
                switch (match.Groups[0].Value)
                {
                    case "do()":
                        enabled = true;
                        break;
                    case "don't()":
                        enabled = false;
                        break;
                    default:
                        if (enabled)
                        {
                            multiplications += (Int32.Parse(match.Groups[4].Value) * Int32.Parse(match.Groups[5].Value));
                        }
                        break;
                }
            }
        }

        Console.WriteLine($"Multiplication: {multiplications}");
    }
}
