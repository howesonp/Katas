namespace CodeKatas.GameOfLife
{
    public class Cell
    {
        public Coordinate coordinate;
        public bool IsAlive { get; set; }

        public Cell(Coordinate coordinate, bool isAlive)
        {
            this.coordinate = coordinate;
            this.IsAlive = isAlive;
        }

        protected bool Equals(Cell other)
        {
            return coordinate.Equals(other.coordinate) && IsAlive == other.IsAlive;
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
