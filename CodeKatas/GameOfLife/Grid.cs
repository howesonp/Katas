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
            return cell != null;
        }

        private IEnumerable<Cell> GetGridCellsToCheck()
        {
            var minGridCoordinate = GetMinGridCoordinate();
            var maxGridCoordinate = GetMaxGridCoordinate();

            return CreateNewGridCells(minGridCoordinate, maxGridCoordinate);
        }

        private IEnumerable<Cell> CreateNewGridCells(Coordinate minCoordinate, Coordinate maxCoordinate)
        {
            var returnCells = new List<Cell>();

            for (var xAxis = minCoordinate.XAxis; xAxis <= maxCoordinate.YAxis; xAxis++)
            {
                var yAxis = maxCoordinate.YAxis;

                while (yAxis >= minCoordinate.XAxis)
                {
                    returnCells.Add(new Cell(new Coordinate(xAxis, yAxis)));
                    yAxis--;
                }
            }

            return returnCells;
        }

        private Cell CalculateCellStatusOnNewGrid(Cell cell)
        {
            var cellNeighbours = cell.GetCellNeighours();

            var numberOfLiveCellNeighbours = GetNumberOfLiveCellNeighbours(cellNeighbours);

            if (CellShouldNotLive(numberOfLiveCellNeighbours))
            {
                return null;
            }

            if (CellShouldRemainAlive(cell, numberOfLiveCellNeighbours))
            {
                return cell;
            }

            if (CellShouldSpawn(numberOfLiveCellNeighbours))
            {
                return cell;
            }

            return null;
        }

        private static bool CellShouldSpawn(int numberOfLiveCellNeighbours)
        {
            return numberOfLiveCellNeighbours == 3;
        }

        private bool CellShouldRemainAlive(Cell cell, int numberOfLiveCellNeighbours)
        {
            return numberOfLiveCellNeighbours == 2 && CellIsAliveOnCurrentGrid(cell);
        }

        private static bool CellShouldNotLive(int numberOfLiveCellNeighbours)
        {
            return numberOfLiveCellNeighbours < 2 || numberOfLiveCellNeighbours > 3;
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

        private Coordinate GetMaxGridCoordinate()
        {
            var maxGridPosition = YAxisMax() > XAxisMax() ? YAxisMax() : XAxisMax();

            return new Coordinate(maxGridPosition + _outerGridRing, maxGridPosition + _outerGridRing);
        }

        private Coordinate GetMinGridCoordinate()
        {
            var minGridPosition = XAxisMin() < YAxisMin() ? XAxisMin() : YAxisMin();

            return new Coordinate(minGridPosition - _outerGridRing, minGridPosition - _outerGridRing);
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