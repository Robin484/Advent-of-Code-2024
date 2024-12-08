using AdventHelper;

namespace Day4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Day4 test = new Day4("XMAS");
            Day4 actual = new Day4("XMAS");

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

    public class Day4
    {
        public Day4 (string match)
        {
            Match = match.ToCharArray();
            grid = new Grid<char>();
        }

        private char[] Match { get; set; }
        private Grid<char> grid;

        public void Process(string file)
        {
            List<List<char>> data = new List<List<char>>();
            foreach (string line in File.ReadLines(file))
            {
                if (String.IsNullOrEmpty(line))
                {
                    continue;
                }

                data.Add(line.Trim().ToCharArray().ToList());
            }

            grid = new Grid<char>(data) { DefaultValue = '*' };
        }

        public void Part1()
        {
            long matches = 0;
            for (int row = 0; row < grid.Rows; row++)
            {
                for(int col = 0; col < grid.Columns; col++)
                {
                    matches += grid.Matches(row, col, Match);
                }
            }

            Console.WriteLine($"Part1: Xmas occurs {matches} times");
        }

        public void Part2()
        {
            List<List<List<char>>> matches = new List<List<List<char>>>()
            {
                new List<List<char>>() {
                    new List<char>() { 'M', '*', 'M' },
                    new List<char>() { '*', 'A', '*' },
                    new List<char>() { 'S', '*', 'S' }
                },

                new List<List<char>>() {
                    new List<char>() { 'M', '*', 'S' },
                    new List<char>() { '*', 'A', '*' },
                    new List<char>() { 'M', '*', 'S' }
                },

                new List<List<char>>() {
                    new List<char>() { 'S', '*', 'S' },
                    new List<char>() { '*', 'A', '*' },
                    new List<char>() { 'M', '*', 'M' }
                },

                new List<List<char>>() {
                    new List<char>() { 'S', '*', 'M' },
                    new List<char>() { '*', 'A', '*' },
                    new List<char>() { 'S', '*', 'M' }
                }
            };


            long matched = 0;
            for (int row = 0; row < grid.Rows; row++)
            {
                for (int col = 0; col < grid.Columns; col++)
                {
                    foreach (var match in matches)
                    {
                        if (grid.MatchingGrid(row, col, match))
                            matched++;
                    }
                }
            }

            Console.WriteLine($"Part1: Xmas occurs {matched} times");
        }
    }
}
