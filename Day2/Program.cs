namespace Day2;

class Program
{
    static void Main(string[] args)
    {
        Day2 test = new Day2(1, 3);
        Day2 actual = new Day2(1, 3);

        test.Process("test.txt");
        actual.Process("input.txt");

        // Part 1
        test.Part1();
        actual.Part1();

        // Part 2
        test.Part2();
        //actual.Part2();
    }
}

enum Movement
{
    Unknown,
    Increasing,
    Decreasing
}

class Report {
    public Report(string line, int min, int max)
    {
        string[] levels = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        int? previousValue = null;

        foreach(var level in levels)
        {
            int value;
            if(!Int32.TryParse(level, out value))
            {
                throw new Exception($"Invlaid level {level} found in '{line}'");
            }

            if(previousValue.HasValue)
            {
                // Difference
                int difference = previousValue.Value - value;
                
                if (previousValue.Value == value)
                {
                    Safe = false;
                }
                Movement currentMovement = GetMovement(difference);

                if(Movement == Movement.Unknown)
                {
                    Movement = currentMovement;
                }

                if (Movement != currentMovement)
                {
                    Safe = false;
                }
                
                if (Math.Abs(difference) < min || Math.Abs(difference) > max)
                {
                    Safe = false;
                }

            }

            Levels.Add(value);
            previousValue = value;
        }
    }

    public bool Safe { get; set; } = true;
    public List<int> Levels {get; set; } = new List<int>();

    public Movement Movement { get; private set;} = Movement.Unknown;


    private static Movement GetMovement(int difference)
    {
        if (difference > 0)
        {
            return Movement.Increasing;
        }

        if (difference < 0)
        {
            return Movement.Decreasing;
        }

        return Movement.Unknown;
    }
}

class Day2
{
    public Day2(int min, int max)
    {
        Min = min;
        Max = max;
    }

    public int Min { get; init; }
    public int Max { get; init; }

    List<Report> reports = new List<Report>();
    public void Process(string file)
    {

        foreach (string line in File.ReadLines(file))
        {
            if (String.IsNullOrEmpty(line))
            {
                continue;
            }

            reports.Add(new Report(line, Min, Max));
        }
    }

    public void Part1()
    {
        Console.WriteLine($"Safe reports: {reports.Count(r => r.Safe)}");
    }

    public void Part2()
    {

    }
}
