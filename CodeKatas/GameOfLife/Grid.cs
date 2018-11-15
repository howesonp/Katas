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


        public Grid Regenerate()
        {
            var gridCellsToCheck = GetGridCellsToCheck();

            var newGridCells = gridCellsToCheck
                               .Select(CalculateCellStatusOnNewGrid)
                               .Where(NewCellIsAlive)
                               .ToList();

            return new Grid(newGridCells);
        }

        private static bool NewCellIsAlive(Cell cell)
        {
            return cell.GetType() != typeof(DeadCell);
        }

        private IEnumerable<Cell> GetGridCellsToCheck()
        {
            var minGridPosition = GetMinGridPosition();
            var maxGridPosition = GetMaxGridPosition();

            return CreateNewGridCells(minGridPosition, maxGridPosition);
        }

        private IEnumerable<Cell> CreateNewGridCells(int minGridPosition, int maxGridPosition)
        {
            var returnCells = new List<Cell>();

            for (var xAxis = minGridPosition; xAxis <= maxGridPosition; xAxis++)
            {
                var yAxis = maxGridPosition;

                while (yAxis >= minGridPosition)
                {
                    returnCells.Add(new Cell(new Coordinate(xAxis, yAxis)));
                    yAxis--;
                }
            }

            return returnCells;
        }

        private Cell CalculateCellStatusOnNewGrid(Cell cell)
        {
            var deadCell = new DeadCell();

            var cellNeighbours = cell.GetCellNeighours();

            var numberOfLiveCellNeighbours = GetNumberOfLiveCellNeighbours(cellNeighbours);

            if (numberOfLiveCellNeighbours < 2 || numberOfLiveCellNeighbours > 3)
            {
                return deadCell;
            }

            if (numberOfLiveCellNeighbours == 2 && CellIsAliveOnCurrentGrid(cell))
            {
                return cell;
            }

            if (numberOfLiveCellNeighbours == 3)
            {
                return cell;
            }

            return deadCell;
        }

        public int GetNumberOfLiveCellNeighbours(List<Cell> cellNeighbours)
        {
            var liveNeighbours = 0;

            cellNeighbours.ForEach(neighbour =>
            {
                liveNeighbours += _cells.Count(cell => cell.Equals(neighbour));
            });

            return liveNeighbours;
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
    }
}