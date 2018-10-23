using System.Collections.Generic;

namespace CodeKatas.GameOfLife
{
    internal class AdjacentCells
    {
        internal List<Cell> Neighbours = new List<Cell>
        {
            new Cell(new Coordinate(1, 1), true),
            new Cell(new Coordinate(1, 0), true),
            new Cell(new Coordinate(1, -1), true),

            new Cell(new Coordinate(0, 1), true),
            new Cell(new Coordinate(0, -1), true),

            new Cell(new Coordinate(-1, -1), true),
            new Cell(new Coordinate(-1, 0), true),
            new Cell(new Coordinate(-1, 1), true)
        };
    }
}
