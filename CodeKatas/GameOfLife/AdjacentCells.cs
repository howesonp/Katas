using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKatas.GameOfLife
{
    public class AdjacentCells
    {
        public Position TopLeft = new Position(-1, -1);
        public Position Top = new Position(-1, -1);
        public Position TopRight = new Position(-1, -1);
        public Position MiddleRight = new Position(-1, -1);
        public Position MiddleLeft = new Position(-1, -1);
        public Position BottomLeft = new Position(-1, -1);
        public Position Bottom = new Position(-1, -1);
        public Position BottomRight = new Position(-1, -1);
    }
}
