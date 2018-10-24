using System.Collections.Generic;

namespace CodeKatas.GameOfLife
{
    internal class AdjacentCells
    {
        internal List<Coordinate> Neighbours = new List<Coordinate>
        {
            new Coordinate(1, 1), 
            new Coordinate(1, 0), 
            new Coordinate(1, -1),

            new Coordinate(0, 1), 
            new Coordinate(0, -1),

            new Coordinate(-1, -1),
            new Coordinate(-1, 0),
            new Coordinate(-1, 1),
        };
    }
}
