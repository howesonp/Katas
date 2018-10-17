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

        [Test]
        public void ReturnEmptyBoard_WhenCreatingNewGame()
        {
            var board = _ticTacToe.Game.Board;

            board.Squares.Should().HaveCount(9);
        }

        [Test]
        public void PutAnXInPositionOne_WhenTakingFirstTurn_WithAnX()
        {
            _ticTacToe.PlayerX.TryToTakeTurn(1);
            var expectedValidation = new ValidationResult {IsValid = true};

            _ticTacToe.Game.MoveValidationResults.Should().AllBeEquivalentTo(expectedValidation);
            _ticTacToe.Game.Board.Squares[1].Should().Be("X");
        }

        [Test]
        public void FailValidation_WhenTakingFirstTurn_WithAnO()
        {
            _ticTacToe.PlayerO.TryToTakeTurn(1);

            _ticTacToe.Game.MoveValidationResults.Select(result => result.ValidationMessage).Should().Contain("O is not permitted to take the first turn");
            _ticTacToe.Game.MoveValidationResults.Count(result => !result.IsValid).Should().Be(1);
        }

        [Test]
        public void FailValidation_WhenTakingTurn_AndPositionHasAlreadyBeenTaken()
        {
            _ticTacToe.PlayerX.TryToTakeTurn(1);
            _ticTacToe.PlayerO.TryToTakeTurn(1);

            _ticTacToe.Game.MoveValidationResults.Select(result => result.ValidationMessage).Should().Contain("Board position already filled");
            _ticTacToe.Game.MoveValidationResults.Count(result => !result.IsValid).Should().Be(1);
        }

        [Test]
        public void FailValidation_WhenTakingTurnWithAnX_AndPreviousTurnWasAlsoX()
        {
            _ticTacToe.PlayerX.TryToTakeTurn(1);
            _ticTacToe.PlayerX.TryToTakeTurn(2);
            _ticTacToe.Game.MoveValidationResults.Select(result => result.ValidationMessage).Should().Contain("X took the previous turn");
            _ticTacToe.Game.MoveValidationResults.Count(result => !result.IsValid).Should().Be(1);
        }

        [Test]
        public void FailValidation_WhenTakingTurn_AndAllPositionsAlreadyTaken()
        {
            var moves = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            TakeAlternateTurns(moves);
             _ticTacToe.PlayerO.TryToTakeTurn(9);

            _ticTacToe.Game.MoveValidationResults.Select(result => result.ValidationMessage).Should().Contain("Game is over");
            _ticTacToe.Game.MoveValidationResults.Count(result => !result.IsValid).Should().Be(1);
        }

        [Test]
        public void BeADraw_IfAllPositionsAreTaken_AndThereIsNoWinner()
        {
            var moves = new[] { 1, 2, 3, 5, 4, 7, 6, 9, 8 };
            TakeAlternateTurns(moves);
            _ticTacToe.Game.IsDraw.Should().Be(true);
        }

        [Test]
        public void SetGameAsWon_WhenXGetThreeHorizontalInARow_WithValidMoves()
        {
            var moves = new[] {1, 4, 2, 5, 3};

            TakeAlternateTurns(moves);

            _ticTacToe.Game.HasWinner.Should().Be(true);
            _ticTacToe.Game.WinningPlayer.Should().Be("X");
        }

        [Test]
        public void SetGameAsWon_WhenXGetThreeHorizontalInARow_WithValidMovesInNonSequential()
        {
            var moves = new[] { 3, 4, 1, 5, 2 };

            TakeAlternateTurns(moves);

            _ticTacToe.Game.HasWinner.Should().Be(true);
            _ticTacToe.Game.WinningPlayer.Should().Be("X");
        }

        [Test]
        public void SetGameAsWon_WhenOGetThreeHorizontalInARow_WithValidMoves()
        {
            var moves = new[] { 1, 4, 2, 5, 7, 6 };

            TakeAlternateTurns(moves);

            _ticTacToe.Game.HasWinner.Should().Be(true);
            _ticTacToe.Game.WinningPlayer.Should().Be("O");
        }

        [Test]
        public void SetGameAsWon_WhenXGetThreeVerticalInARow_WithValidMoves()
        {
            var moves = new[] { 1, 2, 4, 3, 7 };

            TakeAlternateTurns(moves);

            _ticTacToe.Game.HasWinner.Should().Be(true);
            _ticTacToe.Game.WinningPlayer.Should().Be("X");
        }

        [Test]
        public void SetGameAsWon_WhenXGetThreeDiagonalInARow_WithValidMoves()
        {
            var moves = new[] { 1, 2, 5, 3, 9 };

            TakeAlternateTurns(moves);

            _ticTacToe.Game.HasWinner.Should().Be(true);
            _ticTacToe.Game.WinningPlayer.Should().Be("X");
        }

        [Test]
        public void SetGameAsWon_WhenXGetThreeDiagonalInARow_WithValidMovesNonOrdered()
        {
            var moves = new[] { 5, 2, 3, 4, 7 };

            TakeAlternateTurns(moves);

            _ticTacToe.Game.HasWinner.Should().Be(true);
            _ticTacToe.Game.WinningPlayer.Should().Be("X");
        }

        private void TakeAlternateTurns(int[] moves)
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
