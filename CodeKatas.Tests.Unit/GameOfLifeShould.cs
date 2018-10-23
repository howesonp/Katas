using CodeKatas.GameOfLife;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace CodeKatas.Tests.Unit
{
    [TestFixture]
    public class GameOfLifeShould
    {
        [Test]
        public void ReturnEmptyGrid_GivenInitialEmptyGrid()
        {
            var grid = new Grid(new List<Cell>());
            var game = new Game(grid);
            var returnGrid = game.Tick();

            returnGrid.Should().Be(grid);
        }

        [Test]
        public void ReturnOneLiveCell_GivenGridWhichHasOnlyOneCellWithTwoAdjacentNeighbours()
        {
            var listOfCells = new List<Cell>
            {
                new Cell(new Coordinate(1,3), true),
                new Cell(new Coordinate(2,2), true),
                new Cell(new Coordinate(3,1), true),
                new Cell(new Coordinate(3,3), true),
            };

            var expectedReturnCells = new List<Cell>
            {
                new Cell(new Coordinate(1,3), false),
                new Cell(new Coordinate(2,2), true),
                new Cell(new Coordinate(3,1), false),
                new Cell(new Coordinate(3,3), false),
            };

            var grid = new Grid(listOfCells);
            var game = new Game(grid);
            var expectedGrid = new Grid(expectedReturnCells);

            var returnGrid = game.Tick();

            returnGrid.Should().Be(expectedGrid);
        }
    }
}
