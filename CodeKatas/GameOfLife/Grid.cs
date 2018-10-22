using System.Collections.Generic;

namespace CodeKatas.GameOfLife
{
    public class Grid
    {
        private List<Cell> _cells;

        public Grid()
        {
            _cells = new List<Cell>();
        }

        public void SeedCells(List<Cell> inputCells)
        {
            inputCells.ForEach(cell =>
            {
                _cells.Add(cell); 
            });
        }

        public bool GridIsEmpty()
        {
            return _cells.Count == 0;
        }
    }
}