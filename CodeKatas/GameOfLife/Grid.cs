using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeKatas.GameOfLife
{
    public class Grid
    {
        private List<Cell> _cells;
        private List<Cell> _liveAndDeadCells;
        private readonly int _outerGridRing = 1;

        public Grid(List<Cell> inputCells)
        {
            _cells = inputCells;
        }

        public Grid Tick()
        {
            if (!_cells.Any())
            {
                return new EmptyGrid();
            }

            _cells = GetNextGridLiveCells();

            return new Grid(_cells);
        }

        private List<Cell> GetNextGridLiveCells()
        {
            return GetNextGridUnfiltered().Where(cell => cell.IsAlive()).ToList();
        }

        public List<Cell> GetNextGridUnfiltered()
        {
            var gridCellsToCheck = GetGridCellsToCheck();

            return gridCellsToCheck.Select(CalculateCellStatusOnNewGrid)
                .ToList();
        }

        private IEnumerable<Cell> GetGridCellsToCheck()
        {
            var gridCoordinates = new Coordinate(GetMinGridPoint(), GetMaxGridPoint());

            return CreateNewGridCells(gridCoordinates);
        }

        private IEnumerable<Cell> CreateNewGridCells(Coordinate coordinates)
        {
            var returnCells = new List<Cell>();

            for (var xAxis = coordinates.XAxis; xAxis <= coordinates.YAxis; xAxis++)
            {
                var yAxis = coordinates.YAxis;

                while (yAxis >= coordinates.XAxis)
                {
                    returnCells.Add(new Cell(xAxis, yAxis, CellState.Dead));
                    yAxis--;
                }
            }

            return returnCells;
        }

        private Cell CalculateCellStatusOnNewGrid(Cell cell)
        {
            var cellNeighbours = cell.GetCellNeighours();

            var numberOfLiveCellNeighbours = GetNumberOfLiveCellNeighbours(cellNeighbours);

            if (cell.ShouldNotLive(numberOfLiveCellNeighbours))
            {
                return CreateDeadCell(cell);
            }

            if (cell.ShouldRemainAlive(numberOfLiveCellNeighbours) && CellIsAliveOnCurrentGrid(cell))
            {
                return CreateLiveCell(cell);
            }

            if (cell.ShouldSpawn(numberOfLiveCellNeighbours))
            {
                return CreateLiveCell(cell);
            }

            return CreateDeadCell(cell);
        }

        private static Cell CreateDeadCell(Cell cell)
        {
            return new Cell(cell.XAxis, cell.YAxis, CellState.Dead);
        }

        private static Cell CreateLiveCell(Cell cell)
        {
            return new Cell(cell.XAxis, cell.YAxis, CellState.Alive);
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

        private int GetMaxGridPoint()
        { 
            var maxGridPosition = YAxisMax() > XAxisMax() ? YAxisMax() : XAxisMax();

            return maxGridPosition + _outerGridRing;
        }

        private int GetMinGridPoint()
        {
            var minGridPosition = XAxisMin() < YAxisMin() ? XAxisMin() : YAxisMin();

            return minGridPosition - _outerGridRing;
        }

        private int XAxisMin()
        {
            return _cells.Min(c=> c.XAxis);
        }

        private int XAxisMax()
        {
            return _cells.Max(e => e.XAxis);
        }

        private int YAxisMin()
        {
            return _cells.Min(e => e.YAxis);
        }

        private int YAxisMax()
        {
            return _cells.Max(e => e.YAxis);
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

        public string GridAsHtmlTable()
        {
            var sb = new StringBuilder();
            sb.Append("<TABLE border=1>\n");
            var orderedCells = GetNextGridUnfiltered().OrderBy(e => e.XAxis).ThenBy(y => y.YAxis);
            bool firstIteration = true;
            int previousXAxis = 0;

            foreach (var cell in orderedCells)
            {
                if (firstIteration)
                {
                    //sb.Append("<TR>\n");

                    CreateHtmlCell(sb, cell, "firstIteration");
                    firstIteration = false;

                    //sb.Append("</TR>\n");
                }
                else if (cell.XAxis == previousXAxis)
                {
                    CreateHtmlCell(sb, cell, "cellSameAxis");
                }
                else if(cell.XAxis != previousXAxis)
                {
                    sb.Append("</TR>\n"); // close previous row
                    sb.Append("<TR>\n"); // start new row
                    CreateHtmlCell(sb, cell, "new row");   
                }

                previousXAxis = cell.XAxis;
            }
            sb.Append("</TABLE>");

            return sb.ToString();
        }

        private static void CreateHtmlCell(StringBuilder sb, Cell cell, string message)
        {
            sb.Append("<TD width=25px; height=25px>");
            sb.Append(cell.IsAlive()
                ? $"Alive"
                : $"     ");
            sb.Append("</TD>");
        }
    }
}