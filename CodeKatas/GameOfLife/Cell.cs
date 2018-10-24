namespace CodeKatas.GameOfLife
{
    public class Cell
    {
        public Coordinate coordinate;
        public CellState state;

        public Cell(Coordinate coordinate, CellState cellState)
        {
            this.coordinate = coordinate;
            this.state = cellState;
        }

        protected bool Equals(Cell other)
        {
            return coordinate.Equals(other.coordinate) && state == other.state;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Cell)obj);
        }        
    }
}
