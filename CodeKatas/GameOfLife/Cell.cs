namespace CodeKatas.GameOfLife
{
    public struct Cell
    {
        public Position position;
        public State state;

        public Cell(Position position, State state)
        {
            this.position = position;
            this.state = state;
        }
    }
}
