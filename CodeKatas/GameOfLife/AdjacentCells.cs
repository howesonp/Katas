using System.Collections.Generic;

namespace CodeKatas.GameOfLife
{
    internal class AdjacentCells
    {
        internal List<Cell> Neighbours = new List<Cell>
        {
            new Cell(1, 1), 
            new Cell(1, 0), 
            new Cell(1, -1),

            new Cell(0, 1), 
            new Cell(0, -1),

            new Cell(-1, -1),
            new Cell(-1, 0),
            new Cell(-1, 1)
        };
    }
}
