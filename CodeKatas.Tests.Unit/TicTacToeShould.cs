using System.Collections.Generic;
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
            var actualValidationResult = _ticTacToe.PlayerX.TryToTakeTurn(1);
            var expectedValidation = new ValidationResult {IsValid = true};

            actualValidationResult.Should().AllBeEquivalentTo(expectedValidation);
            _ticTacToe.Game.Board.Squares[1].Should().Be("X");
        }

        [Test]
        public void FailValidation_WhenTakingFirstTurn_WithAnO()
        {
            var actualValidationResult = _ticTacToe.PlayerO.TryToTakeTurn(1);

            actualValidationResult.Select(result => result.ValidationMessage).Should().Contain("O is not permitted to take the first turn");
            actualValidationResult.Count(result => !result.IsValid).Should().Be(1);
        }

        [Test]
        public void FailValidation_WhenTakingTurn_AndPositionHasAlreadyBeenTaken()
        {
            _ticTacToe.PlayerX.TryToTakeTurn(1);
            var actualValidationResult = _ticTacToe.PlayerO.TryToTakeTurn(1);

            actualValidationResult.Select(result => result.ValidationMessage).Should().Contain("Board position already filled");
            actualValidationResult.Count(result => !result.IsValid).Should().Be(1);
        }

        [Test]
        public void FailValidation_WhenTakingTurnWithAnX_AndPreviousTurnWasAlsoX()
        {
             _ticTacToe.PlayerX.TryToTakeTurn(1);
            var actualValidationResult = _ticTacToe.PlayerX.TryToTakeTurn(2);
            actualValidationResult.Select(result => result.ValidationMessage).Should().Contain("X took the previous turn");
            actualValidationResult.Count(result => !result.IsValid).Should().Be(1);
        }

        [Test]
        public void FailValidation_WhenTakingTurn_AndAllPositionsAlreadyTaken()
        {
            _ticTacToe.PlayerX.TryToTakeTurn(1);
            _ticTacToe.PlayerO.TryToTakeTurn(2);
            _ticTacToe.PlayerX.TryToTakeTurn(3);
            _ticTacToe.PlayerO.TryToTakeTurn(4);
            _ticTacToe.PlayerX.TryToTakeTurn(5);
            _ticTacToe.PlayerO.TryToTakeTurn(6);
            _ticTacToe.PlayerX.TryToTakeTurn(7);
            _ticTacToe.PlayerO.TryToTakeTurn(8);
            _ticTacToe.PlayerX.TryToTakeTurn(9);
            var actualValidationResult = _ticTacToe.PlayerO.TryToTakeTurn(9);

            actualValidationResult.Select(result => result.ValidationMessage).Should().Contain("Game is over", "Board position is already filled");
            actualValidationResult.Count(result => !result.IsValid).Should().Be(2);
        }

        //public void ReturnWin_WhenPlaying_AndPositionOneTwoAndThreeAreFilledByX()
        //{
        //    var moves = new Dictionary<int, string>
        //    {
        //        {1, "X"},
        //        {4, "O"},
        //        {2, "X"},
        //        {5, "O"},
        //        {3, "X"},

        //    };
        //}

        //private Game PlayWholeGame(Dictionary<int, string> moves, Game game)
        //{
        //    foreach (var move in moves)
        //    {
        //        TryTakeTurn(move.Key, move.Value);
        //    }
        //}
    }
}
