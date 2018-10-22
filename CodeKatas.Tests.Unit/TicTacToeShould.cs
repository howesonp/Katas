using System;
using System.Linq;
using NUnit.Framework;
using CodeKatas.TicTacToe;
using FluentAssertions;

namespace CodeKatas.Tests.Unit
{
    [TestFixture]
    public class TicTacToeShould
    {
        private TicTacToe.TicTacToeGame _ticTacToe;

        [SetUp]
        public void SetUp()
        {
            _ticTacToe = null;
            _ticTacToe = new TicTacToeGame();
        }

        [Test]
        public void PutAnXInPositionOne_WhenTakingFirstTurn_WithAnX()
        {
            _ticTacToe.TryToTakeTurn(BoardPosition.TopLeft, PlayerSign.Cross);
            
            _ticTacToe.Board.GetPlayerSignOnBoardPosition(BoardPosition.TopLeft).Should().Be(PlayerSign.Cross);
        }

        [Test]
        public void ThrowException_WhenTakingFirstTurn_WithAnO()
        {
            Action move = () => _ticTacToe.TryToTakeTurn(BoardPosition.TopLeft, PlayerSign.Nought);

            move.Should().Throw<Exception>().WithMessage("Nought cannot take the first move");
        }

        [Test]
        public void ThrowException_WhenTakingTurn_AndPositionHasAlreadyBeenTaken()
        {
            _ticTacToe.TryToTakeTurn(BoardPosition.TopLeft, PlayerSign.Cross);

            Action move = () => _ticTacToe.TryToTakeTurn(BoardPosition.TopLeft, PlayerSign.Nought);
            
            move.Should().Throw<Exception>().WithMessage("This position has already been taken");
        }

        [Test]
        public void ThrowException_WhenTakingTurnWithAnX_AndPreviousTurnWasAlsoX()
        {
            _ticTacToe.TryToTakeTurn(BoardPosition.TopLeft, PlayerSign.Cross);

            Action move = () => _ticTacToe.TryToTakeTurn(BoardPosition.BottomRight, PlayerSign.Cross);

            move.Should().Throw<Exception>().WithMessage($"{PlayerSign.Cross} took the previous turn");
        }

        [Test]
        public void ThrowException_WhenTakingTurn_AndAllPositionsAlreadyTaken()
        {
            var moves = new[]
            {
                BoardPosition.TopLeft, BoardPosition.TopRight, BoardPosition.Top,
                BoardPosition.Middle, BoardPosition.MiddleLeft, BoardPosition.MiddleRight,
                BoardPosition.BottomRight, BoardPosition.Bottom, BoardPosition.BottomLeft
            };

            TakeAlternateTurns(moves);

            Action move =  () => _ticTacToe.TryToTakeTurn(BoardPosition.BottomRight, PlayerSign.Nought);

            move.Should().Throw<Exception>().WithMessage("No more positions available on the board");
        }

        [Test]
        public void BeADraw_IfAllPositionsAreTaken_AndThereIsNoWinner()
        {
            var moves = new[]
            {
                BoardPosition.TopLeft,
                BoardPosition.Top,
                BoardPosition.TopRight,
                BoardPosition.Middle,
                BoardPosition.MiddleLeft,
                BoardPosition.BottomLeft,
                BoardPosition.MiddleRight,
                BoardPosition.BottomRight,
                BoardPosition.Bottom
            };

            TakeAlternateTurns(moves);

            var result = _ticTacToe.CheckGameForResult();

            result.Should().Be(GameState.IsDraw);
        }

        [Test]
        public void SetGameAsWon_WhenXGetThreeHorizontalInARow_WithValidMoves()
        {
            var moves = new[]
            {
                BoardPosition.TopLeft,
                BoardPosition.MiddleLeft,
                BoardPosition.Top,
                BoardPosition.Middle,
                BoardPosition.TopRight
            };

            TakeAlternateTurns(moves);

            var result = _ticTacToe.CheckGameForResult();

            result.Should().Be(GameState.PlayerXWin);
        }

        [Test]
        public void SetGameAsWon_WhenXGetThreeHorizontalInARow_WithValidMovesInNonSequential()
        {
            var moves = new[]
            {
                BoardPosition.TopRight,
                BoardPosition.MiddleLeft,
                BoardPosition.TopLeft,
                BoardPosition.Middle,
                BoardPosition.Top
            };

            TakeAlternateTurns(moves);

            var result = _ticTacToe.CheckGameForResult();

            result.Should().Be(GameState.PlayerXWin);
        }

        [Test]
        public void SetGameAsWon_WhenOGetThreeHorizontalInARow_WithValidMoves()
        {
            var moves = new[]
            {
                BoardPosition.TopLeft,
                BoardPosition.MiddleLeft,
                BoardPosition.Top,
                BoardPosition.Middle,
                BoardPosition.BottomLeft,
                BoardPosition.MiddleRight
            };

            TakeAlternateTurns(moves);

            var result = _ticTacToe.CheckGameForResult();

            result.Should().Be(GameState.PlayerOWin);
        }

        [Test]
        public void SetGameAsWon_WhenXGetThreeVerticalInARow_WithValidMoves()
        {
            var moves = new[]
            {
                BoardPosition.TopLeft,
                BoardPosition.Top,
                BoardPosition.MiddleLeft,
                BoardPosition.TopRight,
                BoardPosition.BottomLeft
            };

            TakeAlternateTurns(moves);

            var result = _ticTacToe.CheckGameForResult();

            result.Should().Be(GameState.PlayerXWin);
        }

        [Test]
        public void SetGameAsWon_WhenXGetThreeDiagonalInARow_WithValidMoves()
        {
            var moves = new[]
            {
                BoardPosition.TopLeft,
                BoardPosition.Top,
                BoardPosition.Middle,
                BoardPosition.TopRight,
                BoardPosition.BottomRight
            };

            TakeAlternateTurns(moves);

            var result = _ticTacToe.CheckGameForResult();

            result.Should().Be(GameState.PlayerXWin);
        }

        [Test]
        public void SetGameAsWon_WhenXGetThreeDiagonalInARow_WithValidMovesNonOrdered()
        {
            var moves = new[]
            {
                BoardPosition.Middle,
                BoardPosition.Top,
                BoardPosition.TopRight,
                BoardPosition.MiddleLeft,
                BoardPosition.BottomLeft
            };

            TakeAlternateTurns(moves);

            var result = _ticTacToe.CheckGameForResult();

            result.Should().Be(GameState.PlayerXWin);
        }

        [Test]
        public void SetGameAsWon_WhenOGetThreeDiagonalInARow_WithValidMovesNonOrdered()
        {
            var moves = new[]
            {
                BoardPosition.MiddleRight,
                BoardPosition.TopLeft,
                BoardPosition.Bottom,
                BoardPosition.BottomRight,
                BoardPosition.BottomLeft,
                BoardPosition.Middle
            };

            TakeAlternateTurns(moves);

            var result = _ticTacToe.CheckGameForResult();

            result.Should().Be(GameState.PlayerOWin);
        }

        [Test]
        public void ReturnValidationError_WhenGameIsWon_AndPlayerTriesToTakeAnotherTurn()
        {
            var moves = new[]
            {
                BoardPosition.MiddleRight,
                BoardPosition.TopLeft,
                BoardPosition.Bottom,
                BoardPosition.BottomRight,
                BoardPosition.BottomLeft,
                BoardPosition.Middle
            };

            TakeAlternateTurns(moves);

            var result = _ticTacToe.CheckGameForResult();

            result.Should().Be(GameState.PlayerOWin);
    
            Action move = () => _ticTacToe.TryToTakeTurn(BoardPosition.Top, PlayerSign.Cross);

            move.Should().Throw<Exception>();
        }

        private void TakeAlternateTurns(BoardPosition[] moves)
        {
            var isPlayerX = true;

            foreach (var move in moves)
            {
                if (isPlayerX)
                {
                    _ticTacToe.TryToTakeTurn(move, PlayerSign.Cross);
                    isPlayerX = false;
                }
                else
                {
                    _ticTacToe.TryToTakeTurn(move, PlayerSign.Nought);
                    isPlayerX = true;
                }
            }
        }
    }
}
