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

            board.Should().HaveCount(9);
        }

        [Test]
        public void PutAnXInPositionOne_WhenTakingFirstTurn_WithAnX()
        {
            var actualValidationResult = _ticTacToe.TryTakeTurn(1, "X");
            var expectedValidation = new ValidationResult {IsValid = true};

            actualValidationResult.Should().AllBeEquivalentTo(expectedValidation);
            _ticTacToe.Game.Board[1].Should().Be("X");
        }

        [Test]
        public void FailValidation_WhenTakingFirstTurn_WithAnO()
        {
            var actualValidationResult = _ticTacToe.TryTakeTurn(1, "O");

            actualValidationResult.Select(result => result.ValidationMessage).Should().Contain("O is not permitted to take the first turn");
        }

        [Test]
        public void FailValidation_WhenTakingTurn_AndPositionHasAlreadyBeenTaken()
        {
            _ticTacToe.TryTakeTurn(1, "X");
            var actualValidationResult = _ticTacToe.TryTakeTurn(1, "O");

            actualValidationResult.Select(result => result.ValidationMessage).Should().Contain("Board position already filled");
        }

        [Test]
        public void FailValidation_WhenTakingTurnWithAnX_AndPreviousTurnWasAlsoX()
        {
             _ticTacToe.TryTakeTurn(1, "X");
            var actualValidationResult = _ticTacToe.TryTakeTurn(2, "X");
            actualValidationResult.Select(result => result.ValidationMessage).Should().Contain("X took the previous turn");
        }

        [Test]
        public void FailValidation_WhenTakingTurn_AndAllPositionsAlreadyTaken()
        {
            _ticTacToe.TryTakeTurn(1, "X");
            _ticTacToe.TryTakeTurn(2, "O");
            _ticTacToe.TryTakeTurn(3, "X");
            _ticTacToe.TryTakeTurn(4, "O");
            _ticTacToe.TryTakeTurn(5, "X");
            _ticTacToe.TryTakeTurn(6, "O");
            _ticTacToe.TryTakeTurn(7, "X");
            _ticTacToe.TryTakeTurn(8, "O");
            _ticTacToe.TryTakeTurn(9, "X");
            var actualValidationResult = _ticTacToe.TryTakeTurn(9, "X");

            actualValidationResult.Select(result => result.ValidationMessage).Should().Contain("Game is over");
        }
    }
}
