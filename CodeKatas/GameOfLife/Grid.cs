using System.Collections.Generic;
using System.Linq;

namespace CodeKatas.GameOfLife
{
    public class Grid
    {
        private List<Cell> _cells;

        public Grid(List<Cell> inputCells)
        {
            _cells = inputCells;
        }

        protected bool Equals(Grid other)
        {
            return _cells == other._cells;
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
            var count = 0;

            foreach (var cell in _cells)
            {
                count = GetNumberOfAdjacentLiveCellsForCell(cell);

                // Underpopulation
                // Any live cell with fewer than two live neighbours dies, as if by loneliness.
                if (cell.IsAlive && count < 2)
                {
                    cell.IsAlive = false;
                }

                // Overpopulation
                // Any live cell with more than three live neighbours dies, as if by overcrowding.
                if (cell.IsAlive && count > 3)
                {
                    cell.IsAlive = false;
                }

                // Unchanged
                // Any live cell with two or three live neighbours lives, unchanged, to the next generation.
                if (cell.IsAlive && (count == 3 || count == 2))
                {
                    cell.IsAlive = true;
                }
                // Any dead cell with exactly three live neighbours comes to life.
                // Bring to life
                if (!cell.IsAlive && count == 3)
                {
                    cell.IsAlive = true;
                }
            }

            return new Grid(_cells);
        }

        private int GetNumberOfAdjacentLiveCellsForCell(Cell cell)
        {
            var adjacentCell = new AdjacentCells();
            var liveNeighbours = 0;

            adjacentCell.Neighbours.ForEach(neighbour =>
            {
                var xAxis = cell.coordinate.XAxis + neighbour.coordinate.XAxis;
                var yAxis = cell.coordinate.YAxis + neighbour.coordinate.YAxis;

                var cellToMatch = new Cell(new Coordinate(xAxis, yAxis), true);

                liveNeighbours += _cells.Count(x => x.Equals(cellToMatch));

            });

            return liveNeighbours;
        }
    }
}