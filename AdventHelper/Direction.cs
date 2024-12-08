using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventHelper
{
    public enum Direction
    {
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest,
    }

    public static class DirectionExtensions
    {
        public static bool IsOrthoganal(this Direction direction)
        {
            return (direction == Direction.North || direction == Direction.South || direction == Direction.East || direction == Direction.West);
        }
    }
}
