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

            //foreach (var cell in gridCellsToCheck)
            //{
            //    var newCell = CalculateCellStatusOnNewGrid(cell);
            //}

            var newCells = gridCellsToCheck
                                .Select(CalculateCellStatusOnNewGrid)
                                .Where(newCell => newCell != null)
                                .ToList();

            return new Grid(newCells);
        }

        private IEnumerable<Cell> GetGridCellsToCheck()
        {
            var xAxis = GetXAxisStartPoint();

            var yAxis = GetYAxisStartPoint();

            var returnCells = new List<Cell>();

            for (var x = xAxis; x <= yAxis; x++)
            {
                var counter = yAxis;

                while (counter >= xAxis)
                {
                    returnCells.Add(new Cell(new Coordinate(x, counter)));
                    counter--;
                }
            }

            return returnCells;
        }

        private int GetYAxisStartPoint()
        {
            var yAxis = _cells.Max(y => y.Coordinate.YAxis) > _cells.Max(x => x.Coordinate.XAxis)
                ? _cells.Max(y => y.Coordinate.YAxis)
                : _cells.Max(x => x.Coordinate.XAxis);

            return yAxis + 1;
        }

        private int GetXAxisStartPoint()
        {
            var xAxis = _cells.Min(e => e.Coordinate.XAxis) < _cells.Min(y => y.Coordinate.YAxis)
                ? _cells.Min(e => e.Coordinate.XAxis)
                : _cells.Min(y => y.Coordinate.YAxis);

            return xAxis - 1;
        }

        private Cell CalculateCellStatusOnNewGrid(Cell cell)
        {
            // Any live cell with fewer than two live neighbours dies, as if by loneliness.
            // Any live cell with more than three live neighbours dies, as if by overcrowding.
            // Any live cell with two or three live neighbours lives, unchanged, to the next generation.
            // Any dead cell with exactly three live neighbours comes to life.

            var neighbouringCells = cell.GetCellNeighours();

            var count = NumberOfLiveNeighbours(neighbouringCells);

            if (count < 2 || count > 3)
            {
                // don't add the cell
            }

            if (count == 2)
            {
                // Only add the cell if it exists in current set - is alive.
                if (_cells.Any(existing => existing.Equals(cell)))
                {
                    return cell;
                }
            }

            if (count == 3)
            {
                return cell;
            }

            return null;
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