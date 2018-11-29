using System;
using System.Collections.Generic;

namespace CodeKatas.GameOfLife
{
    public class Cell
    {
        private readonly CellState _cellState;
        public int XAxis { get; }
        public int YAxis { get; }
 
        public Cell(int xAxis, int yAxis, CellState cellState = CellState.Dead)
        {
            _cellState = cellState;
            XAxis = xAxis;
            YAxis = yAxis;
        }

        protected bool Equals(Cell other)
        {
            return XAxis == other.XAxis && YAxis == other.YAxis;
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

        private static Cell GetCellNeighbour(Cell cell, Cell neighbour)
        {
            var xAxis = cell.XAxis + neighbour.XAxis;
            var yAxis = cell.YAxis + neighbour.YAxis;

            var cellToMatch = new Cell(xAxis, yAxis);
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

        public bool IsAlive()
        {
            return _cellState == CellState.Alive;
        }
    }
}
