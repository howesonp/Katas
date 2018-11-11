using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKatas.GameOfLife
{
    public class EmptyGrid : Grid
    {
        public EmptyGrid() : base(new List<Cell>())
        {

        }
    }
}
