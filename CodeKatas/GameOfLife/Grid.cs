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
            var count = 0;
            var returnCells = new List<Cell>();
            foreach (var cell in _cells)
            {
                count = GetNumberOfAdjacentLiveCellsForCell(cell);

                // Underpopulation
                // Any live cell with fewer than two live neighbours dies, as if by loneliness.
                if (cell.state == CellState.Alive && count < 2)
                {
                    cell.state = CellState.Dead;
                }

                // Overpopulation
                // Any live cell with more than three live neighbours dies, as if by overcrowding.
                if (cell.state == CellState.Alive && count > 3)
                {
                    cell.state = CellState.Dead;
                }

                // Unchanged
                // Any live cell with two or three live neighbours lives, unchanged, to the next generation.
                if (cell.state == CellState.Alive && (count == 3 || count == 2))
                {
                    cell.state = CellState.Alive;
                }
                // Any dead cell with exactly three live neighbours comes to life.
                // Bring to life
                //if (cell.state == CellState.Dead && count == 3)
                //{
                //    cell.IsAlive = true;
                //}
            }

            return new Grid(_cells);
        }

        private int GetNumberOfAdjacentLiveCellsForCell(Cell cell)
        {
            var adjacentCell = new AdjacentCells();
            var liveNeighbours = 0;

            adjacentCell.Neighbours.ForEach(neighbour =>
            {
                var xAxis = cell.coordinate.XAxis + neighbour.XAxis;
                var yAxis = cell.coordinate.YAxis + neighbour.YAxis;

                var cellToMatch = new Cell(new Coordinate(xAxis, yAxis), CellState.Alive);

                liveNeighbours += _cells.Count(x => x.Equals(cellToMatch));

            });

            return liveNeighbours;
        }
    }
}