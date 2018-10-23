namespace CodeKatas.GameOfLife
{
    public class Coordinate
    {
        public int XAxis { get; internal set;  }
        public int YAxis { get; internal set;  }

        public Coordinate(int xAxix, int yAxis)
        {
            this.XAxis = xAxix;
            this.YAxis = yAxis;
        }

        protected bool Equals(Coordinate other)
        {
            return XAxis == other.XAxis && YAxis == other.YAxis;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Coordinate)obj);
        }
    }
}