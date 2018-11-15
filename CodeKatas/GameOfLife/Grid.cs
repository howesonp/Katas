using System.Collections.Generic;
using System.Linq;

namespace CodeKatas.GameOfLife
{
    public class Grid
    {
        private readonly List<Cell> _cells;
        private readonly int _outerGridRing = 1;

        public Grid(List<Cell> inputCells)
        {
            _cells = inputCells;
        }

        protected bool Equals(Grid other)
        {
            var isEqual = true;

            if (_cells.Count != other._cells.Count)
            {
                return false;
            }

            _cells.ForEach(cell =>
            {
                if (!other._cells.Any(cell.Equals))
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

        public Grid Regenerate()
        {
            var gridCellsToCheck = GetGridCellsToCheck();

            var newGridCells = gridCellsToCheck
                                .Select(CalculateCellStatusOnNewGrid)
                                .Where(newCell => newCell != null)
                                .ToList();

            return new Grid(newGridCells);
        }

        private IEnumerable<Cell> GetGridCellsToCheck()
        {
            var minGridPosition = GetMinGridPosition();
            var maxGridPosition = GetMaxGridPosition();

            var returnCells = new List<Cell>();

            for (var x = minGridPosition; x <= maxGridPosition; x++)
            {
                var y = maxGridPosition;

                while (y >= minGridPosition)
                {
                    returnCells.Add(new Cell(new Coordinate(x, y)));
                    y--;
                }
            }

            return returnCells;
        }
        private Cell CalculateCellStatusOnNewGrid(Cell cell)
        {
            // Any live cell with fewer than two live neighbours dies, as if by loneliness.
            // Any live cell with more than three live neighbours dies, as if by overcrowding.
            // Any live cell with two or three live neighbours lives, unchanged, to the next generation.
            // Any dead cell with exactly three live neighbours comes to life.

            var numberOfLiveCellNeighbours = cell.GetNumberOfLiveCellNeighbours(_cells);

            if (numberOfLiveCellNeighbours == 2 && CellIsAliveOnCurrentGrid(cell))
            {
                return cell;
            }

            if (numberOfLiveCellNeighbours == 3)
            {
                return cell;
            }

            return null;
        }

        private bool CellIsAliveOnCurrentGrid(Cell cell)
        {
            return _cells.Any(existing => existing.Equals(cell));
        }

        private int GetMaxGridPosition()
        {
            var maxGridPosition = YAxisMax() > XAxisMax() ? YAxisMax() : XAxisMax();

            return maxGridPosition + _outerGridRing;
        }

        private int GetMinGridPosition()
        {
            var minGridPosition = XAxisMin() < YAxisMin() ? XAxisMin() : YAxisMin();

            return minGridPosition - _outerGridRing;
        }

        private int XAxisMin()
        {
            return _cells.Min(e => e.Coordinate.XAxis);
        }

        private int XAxisMax()
        {
            return _cells.Max(e => e.Coordinate.XAxis);
        }

        private int YAxisMin()
        {
            return _cells.Min(e => e.Coordinate.YAxis);
        }

        private int YAxisMax()
        {
            return _cells.Max(e => e.Coordinate.YAxis);
        }
    }
}