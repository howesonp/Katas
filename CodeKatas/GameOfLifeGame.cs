using CodeKatas.GameOfLife;

namespace CodeKatas
{
    public class GameOfLifeGame
    {
        private Grid grid;

        public GameOfLifeGame(Grid grid)
        {
            this.grid = grid;
        }

        public Grid Tick()
        {
            return grid.Equals(new EmptyGrid()) ? grid : grid.Regenerate();
        }    
    }
}
