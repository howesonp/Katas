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
            var grid = new Grid(new List<Cell>());

            var returnGrid = game.Tick(grid);

            returnGrid.Should().Be(grid);
        }

        [Test]
        public void ReturnOneLiveCell_GivenGridWhichHasOnlyOneCellWithTwoAdjacentNeighbours()
        {
            var listOfCells = new List<Cell>
            {
                new Cell(new Coordinate(1,3)),
                new Cell(new Coordinate(2,2)),
                new Cell(new Coordinate(3,1)),
                new Cell(new Coordinate(3,3)),
            };

            var grid = new Grid(listOfCells);
            var expectedGrid = new Grid(new List<Cell> { new Cell(new Coordinate(2, 2)) });

            var returnGrid = game.Tick(grid);

            returnGrid.Should().Be(expectedGrid);
        }
    }
}
