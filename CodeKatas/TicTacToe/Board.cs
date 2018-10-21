namespace CodeKatas.TicTacToe
{
    public class Board
    {
        public SquareCollection Squares;

        public BoardPosition[] WinningHorizontalLines =
        {
            BoardPosition.TopLeft, BoardPosition.MiddleLeft, BoardPosition.BottomLeft
        };

        public BoardPosition[] WinningVerticalLines =
        {
            BoardPosition.TopLeft, BoardPosition.Top, BoardPosition.TopRight
        };

        public BoardPosition[] WinningDiagonalLineOne = { BoardPosition.TopLeft };
        public BoardPosition[] WinningDiagonalLineTwo = { BoardPosition.TopRight };

        public Board()
        {
            Squares = new SquareCollection();
        }

        public void UpdateSquare(PlayerSign playerSign, BoardPosition boardPosition)
        {
            Squares.UpdateSquare(boardPosition, playerSign);
        }
    }
}
