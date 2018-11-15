using System.Collections.Generic;

namespace CodeKatas.GameOfLife
{
    public class EmptyGrid : Grid
    {
        public EmptyGrid() : base(new List<Cell>()) { }
    }
}
