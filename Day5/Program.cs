using System.Formats.Asn1;

namespace Day5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Day5 test = new Day5();
            Day5 actual = new Day5();

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

    public class Day5
    {
        public Day5()
        {

        }

        public int MaxThreads { get; set; } = 5;

        Dictionary<int, List<int>> rules = new Dictionary<int, List<int>>();
        List<int[]> pages = new List<int[]>();

        public void Process(string file)
        {
            int lineNo = 0;
            rules = new Dictionary<int, List<int>>();
            pages = new List<int[]>();

            foreach (string line in File.ReadLines(file))
            {
                lineNo++;
                if (String.IsNullOrEmpty(line))
                {
                    continue;
                }

                if (!line.Contains('|'))
                {
                    string[] parts = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    pages.Add(Array.ConvertAll(parts, int.Parse));
                }
                else
                {
                    string[] parts = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length != 2)
                    {
                        throw new Exception($"Unexpected rule on line: {line}");
                    }

                    int x = int.Parse(parts[0]);
                    int y = int.Parse(parts[1]);

                    if(!rules.ContainsKey(x))
                    {
                        rules[x] = new List<int>() { y };
                    }
                    else
                    {
                        rules[x].Add(y);
                    }
                }
            }
        }

        public bool ValidRule(int[] page, int index)
        {
            int pageNumber = page[index];
            if (!rules.ContainsKey(pageNumber))
                return true;

            var check = rules[pageNumber];

            for (int i = 0; i < index && i < page.Length; i++)
            {
                var pageToCheck = page[i];
                if (check.Contains(pageToCheck))
                    return false;
            }

            return true;
        }

        public bool ValidatePages(int[] pages)
        {
            for (int i = 0; i < pages.Length; i++)
            {
                if (!ValidRule(pages, i))
                    return false;
            }
            return true;
        }

        public int[] Reorder(int[] page)
        {
            List<int> result = page.ToList();
            result.Sort(delegate(int x, int y) { return CompareRule(x, y); });

            return result.ToArray();
        }

        public void Part1()
        {
            List<int> middlePages = new List<int>();

            for (int i = 0; i < pages.Count; i++)
            {
                bool isValid = ValidatePages(pages[i]);

                if (isValid)
                {
                    middlePages.Add(pages[i][pages[i].Length / 2]);
                }
            }
            Console.WriteLine($"Part 1 page sum: {middlePages.Sum()}");
        }

        public void Part2()
        {
            List<int> middlePages = new List<int>();

            for (int i = 0; i < pages.Count; i++)
            {
                bool isValid = ValidatePages(pages[i]);

                if (!isValid)
                {
                    var page = pages[i];
                    int[] reOrdered = Reorder(page);
                    middlePages.Add(reOrdered[reOrdered.Length / 2]);
                }
            }
            Console.WriteLine($"Part 2 page sum: {middlePages.Sum()}");
        }

        private int CompareRule(int x, int y)
        {
            if (rules.ContainsKey(x) && rules[x].Contains(y))
                return -1;
            return 1;
        }
    }
}
