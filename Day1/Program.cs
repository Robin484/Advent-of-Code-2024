using System.Diagnostics;

namespace Day1;

class Program
{
    static void Main(string[] args)
    {
        Day1 test = new Day1();
        Day1 actual = new Day1();

        test.Process("test.txt");
        actual.Process("input.txt");

        // Part 1
        test.Part1();
        actual.Part1();

        // Part 2
        test.Part2();
        actual.Part2();
    }
}

class Day1 {
    List<int> left = new List<int>();
    List<int> right = new List<int>();

    public void Process(string file)
    {
        left = new List<int>();
        right = new List<int>();

        foreach (string line in File.ReadLines(file))
        {
            if (String.IsNullOrEmpty(line))
            {
                continue;
            }

            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if(parts.Length != 2)
            {
                Console.Error.WriteLine($"Unexpected input '{line}', does not contain two numbers");
                continue;
            }

            int leftLocation, rightLocation;
            if(!Int32.TryParse(parts[0], out leftLocation) || !Int32.TryParse(parts[1], out rightLocation))
            {
                Console.Error.WriteLine($"Invalid location in '{line}'");
                continue;
            }

            left.Add(leftLocation);
            right.Add(rightLocation);
        }

        left.Sort();
        right.Sort();
    }

    public void Part1()
    {
        List<int> distances = new List<int>();

        for (int i = 0; i < left.Count; i++)
        {
            distances.Add(Math.Abs(left[i] - right[i]));
        }

        Console.WriteLine($"Total Distinace: {distances.Sum()}");
    }

    public void Part2()
    {
        List<int> similarities = new List<int>();

        foreach(var number in left)
        {
            similarities.Add(right.Count(n => n.Equals(number)) * number);
        }

        Console.WriteLine($"Total similarities: {similarities.Sum()}");
    }
}
