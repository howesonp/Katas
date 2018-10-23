using CodeKatas.GameOfLife;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace CodeKatas.Tests.Unit
{
    [TestFixture]
    public class GameOfLifeShould
    {
        private Game game;

        [SetUp]
        public void SetUp()
        {
            game = new Game();
        }

        [Test]
        public void ReturnEmptyGrid_GivenInitialEmptyGrid()
        {
            var grid = new Grid();

            var returnGrid = game.Tick(grid);

            returnGrid.Should().Be(grid);
        }

        [Test]
        public void ReturnOneLiveCell_GivenGridWithOneLiveCell()
        {
            var coordinate = new Coordinate(1, 1);
            var cell = new Cell(coordinate);
            var listOfCells = new List<Cell> { cell };
            var grid = new Grid();

            grid.SeedCells(listOfCells);

            var returnGrid = game.Tick(grid);

            returnGrid.Should().Be(grid);
        }
    }
}
