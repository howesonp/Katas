using System;
using System.Linq;
using NUnit.Framework;
using CodeKatas.TicTacToe;
using FluentAssertions;
using System.Collections.Generic;

namespace CodeKatas.Tests.Unit
{
    [TestFixture]
    public class TicTacToeShould
    {
        private TicTacToeGame _ticTacToe;

        [SetUp]
        public void SetUp()
        {
            _ticTacToe = null;
            _ticTacToe = new TicTacToeGame();
        }

        [Test]
        public void ReturnGameInProgress_WhenTakingFirstTurn_WithAnX()
        {
            var crossMove = new Move(BoardPosition.TopLeft, PlayerSign.Cross);

            var gameState = _ticTacToe.TryToTakeTurn(crossMove);

            gameState.Should().Be(GameState.InProgress);
        }

        [Test]
        public void ThrowException_WhenTakingFirstTurn_WithAnO()
        {
            var noughtMove = new Move(BoardPosition.TopLeft, PlayerSign.Nought);

            Action moveAction = () => _ticTacToe.TryToTakeTurn(noughtMove);

            moveAction.Should().Throw<Exception>().WithMessage("Nought cannot take the first move");
        }

        [Test]
        public void ThrowException_WhenTakingTurn_AndPositionHasAlreadyBeenTaken()
        {
            var crossMove = new Move(BoardPosition.TopLeft, PlayerSign.Cross);
            _ticTacToe.TryToTakeTurn(crossMove);
            var noughtMove = new Move(BoardPosition.TopLeft, PlayerSign.Nought);

            Action moveAction = () => _ticTacToe.TryToTakeTurn(noughtMove);

            moveAction.Should().Throw<Exception>().WithMessage("This position has already been taken");
        }

        [Test]
        public void ThrowException_WhenTakingTurnWithAnX_AndPreviousTurnWasAlsoX()
        {
            var crossMove = new Move(BoardPosition.TopLeft, PlayerSign.Cross);
            _ticTacToe.TryToTakeTurn(crossMove);
            var bottomRightXMove = new Move(BoardPosition.BottomRight, PlayerSign.Cross);

            Action moveAction = () => _ticTacToe.TryToTakeTurn(bottomRightXMove);

            moveAction.Should().Throw<Exception>().WithMessage($"{PlayerSign.Cross} took the previous turn");
        }

        [Test]
        public void ThrowException_WhenTakingTurn_AndAllPositionsAlreadyTaken()
        {
            var moves = new List<Move>
            {
                new Move(BoardPosition.TopLeft, PlayerSign.Cross),
                new Move(BoardPosition.TopRight, PlayerSign.Nought),
                new Move(BoardPosition.Top, PlayerSign.Cross),
                new Move(BoardPosition.Middle, PlayerSign.Nought),
                new Move(BoardPosition.MiddleLeft, PlayerSign.Cross),
                new Move(BoardPosition.MiddleRight, PlayerSign.Nought),
                new Move(BoardPosition.BottomRight, PlayerSign.Cross),
                new Move(BoardPosition.Bottom, PlayerSign.Nought),
                new Move(BoardPosition.BottomLeft, PlayerSign.Cross)
            };

            moves.ForEach(move => _ticTacToe.TryToTakeTurn(move));

            var bottomRightOMove = new Move(BoardPosition.BottomRight, PlayerSign.Cross);

            Action moveAction =  () => _ticTacToe.TryToTakeTurn(bottomRightOMove);

            moveAction.Should().Throw<Exception>().WithMessage("No more positions available on the board");
        }

        [Test]
        public void BeADraw_IfAllPositionsAreTaken_AndThereIsNoWinner()
        {
            var moves = new List<Move>
            {
                new Move(BoardPosition.TopLeft, PlayerSign.Cross),
                new Move(BoardPosition.Top, PlayerSign.Nought),
                new Move(BoardPosition.TopRight, PlayerSign.Cross),
                new Move(BoardPosition.Middle, PlayerSign.Nought),
                new Move(BoardPosition.BottomRight, PlayerSign.Cross),
                new Move(BoardPosition.MiddleRight, PlayerSign.Nought),
                new Move(BoardPosition.MiddleLeft, PlayerSign.Cross),
                new Move(BoardPosition.BottomLeft, PlayerSign.Nought),
            };

            var finalMove = new Move(BoardPosition.Bottom, PlayerSign.Cross);
            var gameState = GetFinalGameState(moves, finalMove);

            gameState.Should().Be(GameState.IsDraw);
        }

        [Test]
        public void SetGameAsWon_WhenXGetThreeHorizontalInARow_WithValidMoves()
        {
            var moves = new List<Move>
            {
                new Move(BoardPosition.TopLeft, PlayerSign.Cross),
                new Move(BoardPosition.MiddleLeft, PlayerSign.Nought),
                new Move(BoardPosition.Top, PlayerSign.Cross),
                new Move(BoardPosition.Middle, PlayerSign.Nought)
            };

            var finalMove = new Move(BoardPosition.TopRight, PlayerSign.Cross);
            var gameState = GetFinalGameState(moves, finalMove);

            gameState.Should().Be(GameState.PlayerXWin);
        }

        [Test]
        public void SetGameAsWon_WhenXGetThreeHorizontalInARow_WithValidMovesInNonSequential()
        {
            var moves = new List<Move>
            {
                new Move(BoardPosition.TopRight, PlayerSign.Cross),
                new Move(BoardPosition.MiddleLeft, PlayerSign.Nought),
                new Move(BoardPosition.TopLeft, PlayerSign.Cross),
                new Move(BoardPosition.Middle, PlayerSign.Nought)
            };

            var finalMove = new Move(BoardPosition.Top, PlayerSign.Cross);
            var gameState = GetFinalGameState(moves, finalMove);

            gameState.Should().Be(GameState.PlayerXWin);
        }

        [Test]
        public void SetGameAsWon_WhenOGetThreeHorizontalInARow_WithValidMoves()
        {
            var moves = new List<Move>
            {
                new Move(BoardPosition.TopLeft, PlayerSign.Cross),
                new Move(BoardPosition.MiddleLeft, PlayerSign.Nought),
                new Move(BoardPosition.Top, PlayerSign.Cross),
                new Move(BoardPosition.Middle, PlayerSign.Nought),
                new Move(BoardPosition.BottomLeft, PlayerSign.Cross)
            };

            var finalMove = new Move(BoardPosition.MiddleRight, PlayerSign.Nought);
            var gameState = GetFinalGameState(moves, finalMove);

            gameState.Should().Be(GameState.PlayerOWin);
        }

        [Test]
        public void SetGameAsWon_WhenXGetThreeVerticalInARow_WithValidMoves()
        {
            var moves = new List<Move>
            {
                new Move(BoardPosition.TopLeft, PlayerSign.Cross),
                new Move(BoardPosition.Top, PlayerSign.Nought),
                new Move(BoardPosition.MiddleLeft, PlayerSign.Cross),
                new Move(BoardPosition.TopRight, PlayerSign.Nought)
            };

            var finalMove = new Move(BoardPosition.BottomLeft, PlayerSign.Cross);
            var gameState = GetFinalGameState(moves, finalMove);

            gameState.Should().Be(GameState.PlayerXWin);
        }

        [Test]
        public void SetGameAsWon_WhenXGetThreeDiagonalInARow_WithValidMoves()
        {
            var moves = new List<Move>
            {
                new Move(BoardPosition.TopLeft, PlayerSign.Cross),
                new Move(BoardPosition.Top, PlayerSign.Nought),
                new Move(BoardPosition.Middle, PlayerSign.Cross),
                new Move(BoardPosition.TopRight, PlayerSign.Nought)
            };

            var finalMove = new Move(BoardPosition.BottomRight, PlayerSign.Cross);
            var gameState = GetFinalGameState(moves, finalMove);

            gameState.Should().Be(GameState.PlayerXWin);
        }

        [Test]
        public void SetGameAsWon_WhenXGetThreeDiagonalInARow_WithValidMovesNonOrdered()
        {
            var moves = new List<Move>
            {
                new Move(BoardPosition.Middle, PlayerSign.Cross),
                new Move(BoardPosition.Top, PlayerSign.Nought),
                new Move(BoardPosition.TopRight, PlayerSign.Cross),
                new Move(BoardPosition.MiddleLeft, PlayerSign.Nought)
            };

            var finalMove = new Move(BoardPosition.BottomLeft, PlayerSign.Cross);
            var gameState = GetFinalGameState(moves, finalMove);

            gameState.Should().Be(GameState.PlayerXWin);
        }

        [Test]
        public void SetGameAsWon_WhenOGetThreeDiagonalInARow_WithValidMovesNonOrdered()
        {
            var moves = new List<Move>
            {
                new Move(BoardPosition.MiddleRight, PlayerSign.Cross),
                new Move(BoardPosition.TopLeft, PlayerSign.Nought),
                new Move(BoardPosition.Bottom, PlayerSign.Cross),
                new Move(BoardPosition.BottomRight, PlayerSign.Nought),
                new Move(BoardPosition.BottomLeft, PlayerSign.Cross)
            };

            var finalMove = new Move(BoardPosition.Middle, PlayerSign.Nought);
            var gameState = GetFinalGameState(moves, finalMove);

            gameState.Should().Be(GameState.PlayerOWin);
        }

        private GameState GetFinalGameState(List<Move> moves, Move finalMove)
        {
            moves.ForEach(move => _ticTacToe.TryToTakeTurn(move));
            return _ticTacToe.TryToTakeTurn(finalMove);
        }
    }
}
