using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace AdventHelper
{
    public class Grid<T>
    {
        public Grid()
        {
            data = new List<List<T>> ();
        }

        public Grid(List<List<T>> data)
        {
            this.data = data;
        }
        private List<List<T>> data = new List<List<T>>();

        public T DefaultValue { get; set; }

        public int Rows { get { return data.Count; } }
        public int Columns { get { return data.Count == 0 ? 0 : data[0].Count; } }

        public bool HasElement(int row, int column)
        {
            if (row < 0 || column < 0)
                return false;
            if (row >= data.Count)
                return false;
            if (column >= data[row].Count)
                return false;
            return true;
        }

        public T? GetElement(int row, int column)
        {
            if (!HasElement(row, column))
                return default(T);

            return data[row][column];
        }

        public T[] GetElements(int row, int column, Direction direction, int length)
        {
            List<T> elements = new List<T>(length);


            (int row, int column) coordinates = (row, column);
            do
            {
                if (!HasElement(coordinates.row, coordinates.column))
                    return Array.Empty<T>();

                var element = GetElement(coordinates.row, coordinates.column);
                if (element == null)
                    return Array.Empty<T>();

                elements.Add(element);

                coordinates = GetNextCordinate(coordinates.row, coordinates.column, direction);
            }
            while (elements.Count < length);

            return elements.ToArray();
        }

        public bool MatchingGrid(int row, int column, List<List<T>> matchingGrid)
        {
            if (matchingGrid == null || matchingGrid.Count == 0)
                throw new ArgumentException("matchingGrid cannot be empty");

            if (!HasElement(row + (matchingGrid.Count - 1), column + (matchingGrid[0].Count - 1)))
                return false;

            for (int matchRow = 0; matchRow < matchingGrid.Count; matchRow++)
            {
                for (int matchColumn = 0; matchColumn < matchingGrid[matchRow].Count; matchColumn++)
                {
                    var match = matchingGrid[matchRow][matchColumn];
                    if (match == null || match.Equals(DefaultValue))
                        continue;

                    var element = GetElement(row + matchRow, column + matchColumn);
                    
                    if(!match.Equals(element))
                        return false;
                }
            }

            return true;
        }

        public int Matches(int row, int column, T[] match, bool orthognalMatch = false)
        {
            return Matches(row, column, match, orthognalMatch, false);
        }

        public bool HasMatches(int row, int column, T[] match, bool orthognalMatch = false)
        {
            return Matches(row, column, match, orthognalMatch, true) > 0;
        }

        private int Matches(int row, int column, T[] match, bool orthognalMatch, bool returnOnFirstMatch)
        {
            int count = 0;
            foreach (Direction d in Enum.GetValues(typeof(Direction)))
            {
                if (orthognalMatch && !d.IsOrthoganal())
                    continue;

                var elements = GetElements(row, column, d, match.Length);
                if (match.SequenceEqual(elements))
                {
                    count++;

                    if (returnOnFirstMatch)
                        return count;
                }
            }

            return count;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var row in data)
            {
                foreach (var col in row)
                {
                    sb.Append(col);
                }
            }

            sb.AppendLine();

            return sb.ToString();
        }

        public (int, int) GetNextCordinate(int row, int column, Direction direction)
        {
            int nextRow = row;
            int nextColumn = column;
            switch (direction)
            {
                case Direction.North:
                case Direction.NorthEast:
                case Direction.NorthWest:
                    nextRow--;
                    break;
                case Direction.South:
                case Direction.SouthEast:
                case Direction.SouthWest:
                    nextRow++;
                    break;
                default:
                    break;
            }

            switch (direction)
            {
                case Direction.East:
                case Direction.NorthEast:
                case Direction.SouthEast:
                    nextColumn++;
                    break;
                case Direction.West:
                case Direction.NorthWest:
                case Direction.SouthWest:
                    nextColumn--;
                    break;
                default:
                    break;
            }
            return (nextRow, nextColumn);
        }
    }
}
