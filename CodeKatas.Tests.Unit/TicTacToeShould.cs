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
        private TicTacToe.TicTacToe _ticTacToe;

        [SetUp]
        public void SetUp()
        {
            _ticTacToe = null;
            _ticTacToe = new TicTacToe.TicTacToe();
        }

        //[Test]
        //public void ReturnEmptyBoard_WhenCreatingNewGame()
        //{
        //    var board = _ticTacToe.Game.Board;

        //    board.Squares.Should()(9);
        //}

        [Test]
        public void PutAnXInPositionOne_WhenTakingFirstTurn_WithAnX()
        {
            _ticTacToe.PlayerX.TryToTakeTurn(BoardPosition.TopLeft);
            
            _ticTacToe.Game.Board.Squares.GetPlayerSignOnBoardPosition(BoardPosition.TopLeft).Should().Be(PlayerSign.Cross);
        }

        [Test]
        public void ThrowException_WhenTakingFirstTurn_WithAnO()
        {
            Action move = () => _ticTacToe.PlayerO.TryToTakeTurn(BoardPosition.TopLeft);

            move.Should().Throw<Exception>();
        }

        [Test]
        public void ThrowException_WhenTakingTurn_AndPositionHasAlreadyBeenTaken()
        {
            _ticTacToe.PlayerX.TryToTakeTurn(BoardPosition.TopLeft);

            Action move = () => _ticTacToe.PlayerO.TryToTakeTurn(BoardPosition.TopLeft);
            
            move.Should().Throw<Exception>();
        }

        [Test]
        public void ThrowException_WhenTakingTurnWithAnX_AndPreviousTurnWasAlsoX()
        {
            _ticTacToe.PlayerX.TryToTakeTurn(BoardPosition.TopLeft);

            Action move = () => _ticTacToe.PlayerX.TryToTakeTurn(BoardPosition.BottomRight);

            move.Should().Throw<Exception>();
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
            Action move =  () => _ticTacToe.PlayerO.TryToTakeTurn(BoardPosition.BottomRight);

            move.Should().Throw<Exception>();
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

            _ticTacToe.Game.CurrentState.Should().Be(GameState.IsDraw);
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

            _ticTacToe.Game.CurrentState.Should().Be(GameState.PlayerXWin);
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

            _ticTacToe.Game.CurrentState.Should().Be(GameState.PlayerXWin);
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

            _ticTacToe.Game.CurrentState.Should().Be(GameState.PlayerOWin);
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

            _ticTacToe.Game.CurrentState.Should().Be(GameState.PlayerXWin);
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
            
            _ticTacToe.Game.CurrentState.Should().Be(GameState.PlayerXWin);
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

            _ticTacToe.Game.CurrentState.Should().Be(GameState.PlayerXWin);
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

            _ticTacToe.Game.CurrentState.Should().Be(GameState.PlayerOWin);
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

            _ticTacToe.Game.CurrentState.Should().Be(GameState.PlayerOWin);
    
            Action move = () => _ticTacToe.PlayerX.TryToTakeTurn(BoardPosition.Top);

            move.Should().Throw<Exception>();
        }

        private void TakeAlternateTurns(BoardPosition[] moves)
        {
            var isPlayerX = true;

            foreach (var move in moves)
            {
                if (isPlayerX)
                {
                    _ticTacToe.PlayerX.TryToTakeTurn(move);
                    isPlayerX = false;
                }
                else
                {
                    _ticTacToe.PlayerO.TryToTakeTurn(move);
                    isPlayerX = true;
                }
            }
        }
    }
}
