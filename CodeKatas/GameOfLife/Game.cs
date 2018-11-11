using System;
using System.Collections.Generic;

namespace CodeKatas.GameOfLife
{
    public class Game
    {
        private Grid grid;

        public Game(Grid grid)
        {
            this.grid = grid;
        }

        public Grid Tick()
        {
            if (grid.Equals(new EmptyGrid()))
            {
                return grid;
            }

            var regeneratedGrid = grid.Regenerate();

            return regeneratedGrid;
        }    
    }
}
