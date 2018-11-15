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
    }
}
