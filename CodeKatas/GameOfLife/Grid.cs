using System.Collections.Generic;

namespace CodeKatas.GameOfLife
{
    public class Grid
    {
        private List<Cell> _cells;

        public Grid(List<Cell> inputCells)
        {
            _cells = inputCells;
        }

        //public void SeedCells(List<Cell> inputCells)
        //{
        //    inputCells.ForEach(cell =>
        //    {
        //        _cells.Add(cell); 
        //    });
        //}

        public bool GridIsEmpty()
        {
            return _cells.Count == 0;
        }
    }
}