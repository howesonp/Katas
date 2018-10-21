﻿using System.Linq;

namespace CodeKatas.TicTacToe
{
    public class SquareCollection 
    {
        private Square[] _squares;

        public SquareCollection()
        {
            CreateSquares();
        }

        private void CreateSquares()
        {
            _squares = new[]
            {
                new Square{Position = BoardPosition.TopLeft, PlayerSign = PlayerSign.Empty},
                new Square{Position = BoardPosition.Top, PlayerSign = PlayerSign.Empty},
                new Square{Position = BoardPosition.TopRight, PlayerSign = PlayerSign.Empty},
                new Square{Position = BoardPosition.MiddleLeft, PlayerSign = PlayerSign.Empty},
                new Square{Position = BoardPosition.Middle, PlayerSign = PlayerSign.Empty},
                new Square{Position = BoardPosition.MiddleRight, PlayerSign = PlayerSign.Empty},
                new Square{Position = BoardPosition.BottomLeft, PlayerSign = PlayerSign.Empty},
                new Square{Position = BoardPosition.Bottom, PlayerSign = PlayerSign.Empty},
                new Square{Position = BoardPosition.BottomRight, PlayerSign = PlayerSign.Empty}
            };
        }

        public void UpdateSquare(BoardPosition move, PlayerSign playerSign)
        {
            var currentSquare = _squares.First(square => square.Position == move);

            currentSquare.PlayerSign = playerSign;
        }

        public bool AreAllSquaresEmpty()
        {
            return _squares.All(square => square.PlayerSign == PlayerSign.Empty);
        }

        public bool AreAllSquaresFilled()
        {
            return _squares.All(square => square.PlayerSign != PlayerSign.Empty);
        }

        public bool IsSquareFilled(BoardPosition position)
        {
            return _squares.Any(e => e.Position == position && e.PlayerSign != PlayerSign.Empty);
        }

        public PlayerSign GetPlayerSignOnBoardPosition(BoardPosition position)
        {
            var square = _squares.First(e => e.Position == position);

            return square.PlayerSign;
        }
    }
}
