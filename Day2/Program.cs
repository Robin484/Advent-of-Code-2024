namespace Day2;

class Program
{
    static void Main(string[] args)
    {
        Day2 test = new Day2(1, 3);
        Day2 actual = new Day2(1, 3);


        // Part 1
        Console.WriteLine("Part 1");
        
        test.ProcessPart1("test.txt");
        actual.ProcessPart1("input.txt");
        test.Answer();
        actual.Answer();

        Console.WriteLine("");

        // Part 2
        Console.WriteLine("Part 2");
        
        test.ProcessPart2("test.txt");
        actual.ProcessPart2("input.txt");
        test.Answer();
        actual.Answer();
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
        Min = min;
        Max = max;
        string[] levels = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach(var level in levels)
        {
            int value;
            if(!Int32.TryParse(level, out value))
            {
                throw new Exception($"Invlaid level {level} found in '{line}'");
            }

            Levels.Add(value);
        }

        Safe = IsSafe();
    }

    public bool Safe { get; set; } = true;
    public List<int> Levels {get; set; } = new List<int>();
    private int Min { get; set; }
    private int Max { get; set; }

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

    public bool IsSafe(int? ignoreIndex = null)
    {
        Movement movement = Movement.Unknown;
        int? previousValue = null;

        for(int i = 0; i < Levels.Count; i++)
        {
            if(ignoreIndex.HasValue && i == ignoreIndex.Value)
            {
                continue;
            }

            if(previousValue.HasValue)
            {
                int difference = previousValue.Value - Levels[i];
                
                if (previousValue.Value == Levels[i])
                {
                    return false;
                }
                Movement currentMovement = GetMovement(difference);

                if(movement == Movement.Unknown)
                {
                    movement = currentMovement;
                }

                if (movement != currentMovement)
                {
                    return false;
                }
                
                if (Math.Abs(difference) < Min || Math.Abs(difference) > Max)
                {
                    return false;
                }
            }

            previousValue = Levels[i];
        }

        return true;
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
    public void ProcessPart1(string file)
    {
        reports = new List<Report>();
        foreach (string line in File.ReadLines(file))
        {
            if (String.IsNullOrEmpty(line))
            {
                continue;
            }

            reports.Add(new Report(line, Min, Max));
        }
    }

    public void ProcessPart2(string file)
    {
        reports = new List<Report>();
        foreach (string line in File.ReadLines(file))
        {
            if (String.IsNullOrEmpty(line))
            {
                continue;
            }

            Report report = new Report(line, Min, Max);

            if (!report.Safe)
            {
                List<bool> test = new List<bool>();
                for(int i = 0; i < report.Levels.Count; i++)
                {
                    test.Add(report.IsSafe(i));
                }

                if (test.Any(t => t == true))
                {
                    report.Safe = true;
                }
            }

            reports.Add(report);
        }
    }

    public void Answer()
    {
        Console.WriteLine($"Safe reports: {reports.Count(r => r.Safe)}");
    }
}
