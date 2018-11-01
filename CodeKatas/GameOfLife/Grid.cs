using System.Collections.Generic;
using System.Linq;

namespace CodeKatas.GameOfLife
{
    public class Grid
    {
        private readonly List<Cell> _cells;

        public Grid(List<Cell> inputCells)
        {
            _cells = inputCells;
        }

        protected bool Equals(Grid other)
        {
            var isEqual = true;

            _cells.ForEach(cell =>
            {
                if (!other._cells.Any(otherCell => cell.Equals(otherCell)))
                {
                    isEqual = false;
                }
            });

            return isEqual;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Grid)obj);
        }

        public override int GetHashCode()
        {
            return 908360967 + EqualityComparer<List<Cell>>.Default.GetHashCode(_cells);
        }

        public Grid Regenerate()
        {
            var newCells = new List<Cell>();
            foreach (var cell in _cells)
            {
                AddCellToRegeneratedGrid(cell, newCells);
            }

            return new Grid(newCells);
        }

        private void AddCellToRegeneratedGrid(Cell cell, List<Cell> returnCells)
        {
            // Underpopulation
            // Any live cell with fewer than two live neighbours dies, as if by loneliness.

            // Overpopulation
            // Any live cell with more than three live neighbours dies, as if by overcrowding.

            //// Unchanged
            //// Any live cell with two or three live neighbours lives, unchanged, to the next generation.

            // Any dead cell with exactly three live neighbours comes to life.

            var neighbouringCells = cell.GetCellNeighours();

            var count = NumberOfLiveNeighbours(neighbouringCells);

            // Bring to life
            if (count < 2 || count > 3)
            {
                // don't add the cell
            }

            if (count == 2 || count == 3)
            {
                returnCells.Add(cell);
            }
        }

        private int NumberOfLiveNeighbours(List<Cell> neighbouringCells)
        {
            var liveNeighbours = 0;

            neighbouringCells.ForEach(neighbour =>
            {
                liveNeighbours += _cells.Count(cell => cell.Equals(neighbour));
            });

            return liveNeighbours;
        }
    }
}