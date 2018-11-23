using System.Collections.Generic;
using System.Linq;

namespace CodeKatas.GameOfLife
{
    public class Cell
    {
        public Coordinate Coordinate;



        public Cell(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }

        protected bool Equals(Cell other)
        {
            return Coordinate.Equals(other.Coordinate);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Cell) obj);
        }

        public List<Cell> GetCellNeighours()
        {
            var adjacentCell = new AdjacentCells();
            var cellNeighbours = new List<Cell>();

            adjacentCell.Neighbours.ForEach(neighbour =>
            {
                cellNeighbours.Add(GetCellNeighbour(this, neighbour));
            });

            return cellNeighbours;
        }

        private static Cell GetCellNeighbour(Cell cell, Coordinate neighbour)
        {
            var xAxis = cell.Coordinate.XAxis + neighbour.XAxis;
            var yAxis = cell.Coordinate.YAxis + neighbour.YAxis;

            var cellToMatch = new Cell(new Coordinate(xAxis, yAxis));
            return cellToMatch;
        }


        public bool ShouldSpawn(int numberOfLiveCellNeighbours)
        {
            return numberOfLiveCellNeighbours == 3;
        }

        public bool ShouldRemainAlive(int numberOfLiveCellNeighbours)
        {
            return numberOfLiveCellNeighbours == 2;
        }

        public bool ShouldNotLive(int numberOfLiveCellNeighbours)
        {
            return numberOfLiveCellNeighbours < 2 || numberOfLiveCellNeighbours > 3;
        }

        //private Coordinate GetMaxGridCoordinate(List<Cell> cells)
        //{
        //    var maxGridPosition = YAxisMax(cells) > XAxisMax(cells) ? YAxisMax(cells) : XAxisMax(cells);

        //    return new Coordinate(maxGridPosition + 1, maxGridPosition + 1);
        //}

        //private Coordinate GetMinGridCoordinate(List<Cell> cells)
        //{
        //    var minGridPosition = XAxisMin(cells) < YAxisMin(cells) ? XAxisMin(cells) : YAxisMin(cells);

        //    return new Coordinate(minGridPosition - 1, minGridPosition - 1);
        //}

        //private int XAxisMin(List<Cell> cells)
        //{
        //    return cells.Min(e => e.Coordinate.XAxis);
        //}

        //private int XAxisMax(List<Cell> cells)
        //{
        //    return cells.Max(e => e.Coordinate.XAxis);
        //}

        //private int YAxisMin(List<Cell> cells)
        //{
        //    return cells.Min(e => e.Coordinate.YAxis);
        //}

        //private int YAxisMax(List<Cell> cells)
        //{
        //    return cells.Max(e => e.Coordinate.YAxis);
        //}
    }
}
