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
        public void ReturnOneLiveCell_GivenGridWhichThreeVerticalLiveCells()
        {
            var listOfCells = new List<Cell>
            {
                new Cell(new Coordinate(1,1)),
                new Cell(new Coordinate(2,1)),
                new Cell(new Coordinate(3,1)),
            };

            var expectedReturnCells = new List<Cell>
            {
                new Cell(new Coordinate(2,1))
            };

            var grid = new Grid(listOfCells);
            var game = new Game(grid);
            var expectedGrid = new Grid(expectedReturnCells);

            var returnGrid = game.Tick();

            returnGrid.Should().Be(expectedGrid);
        }

        [Test]
        public void ReturnOneLiveCell_GivenGridWhichFourLiveCellsAndOnlyOneCellWithTwoAdjacentNeighbours()
        {
            var listOfCells = new List<Cell>
            {
                new Cell(new Coordinate(1,3)),
                new Cell(new Coordinate(2,2)),
                new Cell(new Coordinate(3,1)),
                new Cell(new Coordinate(3,3)),
            };

            var expectedReturnCells = new List<Cell>
            {
                new Cell(new Coordinate(2,2))
            };

            var grid = new Grid(listOfCells);
            var game = new Game(grid);
            var expectedGrid = new Grid(expectedReturnCells);

            var returnGrid = game.Tick();

            returnGrid.Should().Be(expectedGrid);
        }

        //[Test]
        //public void ReturnGridWithOneDeadCell_GivenOneLiveCell()
        //{
        //    var listOfCells = new List<Cell>
        //    {
        //        new Cell(new Coordinate(1,1), CellState.Alive),
        //    };

        //    var expectedReturnCells = new List<Cell>
        //    {
        //        new Cell(new Coordinate(1,1), CellState.Dead),
        //    };

        //    var grid = new Grid(listOfCells);
        //    var game = new Game(grid);
        //    var expectedGrid = new Grid(expectedReturnCells);

        //    var returnGrid = game.Tick();

        //    returnGrid.Should().Be(expectedGrid);
        //}

        [Test]
        public void ReturnGridWithFourLiveCells_GivenGridWithFourLiveCells_AllWithBetweenTwoAndThreeNeighbours()
        {
            var listOfCells = new List<Cell>
            {
                new Cell(new Coordinate(1,1)),
                new Cell(new Coordinate(1,2)),
                new Cell(new Coordinate(1,3)),
                new Cell(new Coordinate(2,2)),
            };

            var expectedReturnCells = new List<Cell>
            {
                new Cell(new Coordinate(1,1)),
                new Cell(new Coordinate(1,2)),
                new Cell(new Coordinate(1,3)),
                new Cell(new Coordinate(2,2)),
            };

            var grid = new Grid(listOfCells);
            var game = new Game(grid);
            var expectedGrid = new Grid(expectedReturnCells);

            var returnGrid = game.Tick();

            returnGrid.Should().Be(expectedGrid);
        }

        [Test]
        public void ReturnGridWithThreeLiveCells_GivenGridWithFourLiveCells_AndOneWithFewerThanTwoNeighbours()
        {
            var listOfCells = new List<Cell>
            {
                new Cell(new Coordinate(1,1)),
                new Cell(new Coordinate(1,2)),
                new Cell(new Coordinate(1,3)),
                new Cell(new Coordinate(2,3)),
                new Cell(new Coordinate(3,3)),
            };

            var expectedReturnCells = new List<Cell>
            {
                new Cell(new Coordinate(1,2)),
                new Cell(new Coordinate(1,3)),
                new Cell(new Coordinate(2,3)),
            };

            var grid = new Grid(listOfCells);
            var game = new Game(grid);
            var expectedGrid = new Grid(expectedReturnCells);

            var returnGrid = game.Tick();

            returnGrid.Should().Be(expectedGrid);
        }

        [Test]
        public void ReturnGridWithFourLiveCells_GivenGridWithNineLiveCells()
        {
            var listOfCells = new List<Cell>
            {
                new Cell(new Coordinate(1,1)),
                new Cell(new Coordinate(1,2)),
                new Cell(new Coordinate(1,4)),
                new Cell(new Coordinate(2,2)),
                new Cell(new Coordinate(2,3)),
                new Cell(new Coordinate(3,1)),
                new Cell(new Coordinate(3,2)),
                new Cell(new Coordinate(4,1)),
                new Cell(new Coordinate(4,2))

            };

            var expectedReturnCells = new List<Cell>
            {
                new Cell(new Coordinate(1,1)),
                new Cell(new Coordinate(1,2)),
                new Cell(new Coordinate(1,4)),
                new Cell(new Coordinate(4,1)),
                new Cell(new Coordinate(4,2))
            };

            var grid = new Grid(listOfCells);
            var game = new Game(grid);
            var expectedGrid = new Grid(expectedReturnCells);

            var returnGrid = game.Tick();

            returnGrid.Should().Be(expectedGrid);
        }

        //[Test]
        //public void ReturnThreeHorizontalLiveCells_GivenGridWhichHasThreeLiveVerticalCells()
        //{
        //    var listOfCells = new List<Cell>
        //    {
        //        new Cell(new Coordinate(2,1), true),
        //        new Cell(new Coordinate(2,2), true),
        //        new Cell(new Coordinate(2,3), true),
        //    };

        //    var expectedReturnCells = new List<Cell>
        //    {
        //        new Cell(new Coordinate(1,2), true),
        //        new Cell(new Coordinate(2,2), true),
        //        new Cell(new Coordinate(3,2), true),
        //    };

        //    var grid = new Grid(listOfCells);
        //    var game = new Game(grid);
        //    var expectedGrid = new Grid(expectedReturnCells);

        //    var returnGrid = game.Tick();

        //    returnGrid.Should().Be(expectedGrid);
        //}
    }
}
