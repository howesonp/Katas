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
            var grid = new EmptyGrid();
            var game = new Game(grid);
            var returnGrid = game.Tick();

            returnGrid.Should().Be(new EmptyGrid());
        }

        [Test]
        public void ReturnOneLiveCell_GivenGridWhichThreeVerticalLiveCells()
        {
            // | x|  |  | =>  |  |  |  |  |
            // | x|  |  |     | x| x| x|  | x1 - y3
            // | x|  |  |     |  |  |  |  |

            var listOfCells = new List<Cell>
            {
                new Cell(new Coordinate(1,1), CellState.Alive),
                new Cell(new Coordinate(2,1), CellState.Alive),
                new Cell(new Coordinate(3,1), CellState.Alive)
            };

            var expectedReturnCells = new List<Cell>
            {
                //new Cell(new Coordinate(2,0), CellState.Alive),
                new Cell(new Coordinate(2,1), CellState.Alive),
                new Cell(new Coordinate(2,2), CellState.Alive)
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
            // |  |  | x| =>  |  |  |  |
            // |  | x|  |     |  | x| x| x1 - y3
            // | x|  | x|     |  | x|  |

            var listOfCells = new List<Cell>
            {
                new Cell(new Coordinate(1,3), CellState.Alive),
                new Cell(new Coordinate(2,2), CellState.Alive),
                new Cell(new Coordinate(3,1), CellState.Alive),
                new Cell(new Coordinate(3,3), CellState.Alive),
            };

            var expectedReturnCells = new List<Cell>
            {
                new Cell(new Coordinate(2,2), CellState.Alive),
                new Cell(new Coordinate(2,3), CellState.Alive),
                new Cell(new Coordinate(3,2), CellState.Alive)
            };

            var grid = new Grid(listOfCells);
            var game = new Game(grid);
            var expectedGrid = new Grid(expectedReturnCells);

            var returnGrid = game.Tick();

            returnGrid.Should().Be(expectedGrid);
        }

        [Test]
        public void ReturnGridWithFourLiveCells_GivenGridWithFourLiveCells_AllWithBetweenTwoAndThreeNeighbours()
        {
            //                |  | x|  |  
            // | x| x| x| =>  | x| x| x|
            // |  | x|  |     | x| x| x| x1 - y3
            // |  |  |  |     |  |  |  |

            var listOfCells = new List<Cell>
            {
                new Cell(new Coordinate(1,1), CellState.Alive),
                new Cell(new Coordinate(1,2), CellState.Alive),
                new Cell(new Coordinate(1,3), CellState.Alive),
                new Cell(new Coordinate(2,2), CellState.Alive)
            };

            var expectedReturnCells = new List<Cell>
            {
                //new Cell(new Coordinate(0,2), CellState.Alive),
                new Cell(new Coordinate(1,1), CellState.Alive),
                new Cell(new Coordinate(1,2), CellState.Alive),
                new Cell(new Coordinate(1,3), CellState.Alive),
                new Cell(new Coordinate(2,1), CellState.Alive),
                new Cell(new Coordinate(2,2), CellState.Alive),
                new Cell(new Coordinate(2,3), CellState.Alive)
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
            //                |  | x|  |
            // | x| x| x| =>  |  | x| x|
            // |  |  | x|     |  | x| x| x1 - y3
            // |  |  | x|     |  |  |  |

            var listOfCells = new List<Cell>
            {
                new Cell(new Coordinate(1,1), CellState.Alive),
                new Cell(new Coordinate(1,2), CellState.Alive),
                new Cell(new Coordinate(1,3), CellState.Alive),
                new Cell(new Coordinate(2,3), CellState.Alive),
                new Cell(new Coordinate(3,3), CellState.Alive)
            };

            var expectedReturnCells = new List<Cell>
            {
                //new Cell(new Coordinate(0,2), CellState.Alive),
                new Cell(new Coordinate(1,2), CellState.Alive),
                new Cell(new Coordinate(1,3), CellState.Alive),
                new Cell(new Coordinate(2,2), CellState.Alive),
                new Cell(new Coordinate(2,3), CellState.Alive),
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
            // | x | x |   | x |  =>  | X | X |   |   |
            // |   | x | x |   |      |   |   |   |   |
            // | x | x |   |   |      |   |   |   |   |
            // | x | x |   |   |      | X | X |   |   |

            var listOfCells = new List<Cell>
            {
                new Cell(new Coordinate(1,1), CellState.Alive),
                new Cell(new Coordinate(1,2), CellState.Alive),
                new Cell(new Coordinate(1,4), CellState.Alive),
                new Cell(new Coordinate(2,2), CellState.Alive),
                new Cell(new Coordinate(2,3), CellState.Alive),
                new Cell(new Coordinate(3,1), CellState.Alive),
                new Cell(new Coordinate(3,2), CellState.Alive),
                new Cell(new Coordinate(4,1), CellState.Alive),
                new Cell(new Coordinate(4,2), CellState.Alive)
            };

            var expectedReturnCells = new List<Cell>
            {
                new Cell(new Coordinate(1,1), CellState.Alive),
                new Cell(new Coordinate(1,2), CellState.Alive),
                new Cell(new Coordinate(4,1), CellState.Alive),
                new Cell(new Coordinate(4,2), CellState.Alive)
            };

            var grid = new Grid(listOfCells);
            var game = new Game(grid);
            var expectedGrid = new Grid(expectedReturnCells);

            var returnGrid = game.Tick();

            returnGrid.Should().Be(expectedGrid);
        }


        [Test]
        public void ReturnFourLiveCells_GivenGridWhichHasThreeLiveCellsWhichShouldSurviveAndSpawnANewOne()
        {
            // |  |  |  | =>  |  |  |  |
            // |  | x|x |     |  |x |x | x2 - y3
            // |  | x|  |     |  |x |x |
            
            var listOfCells = new List<Cell>
            {
                new Cell(new Coordinate(2,2), CellState.Alive),
                new Cell(new Coordinate(2,3), CellState.Alive),
                new Cell(new Coordinate(3,2), CellState.Alive)
            };

            var expectedReturnCells = new List<Cell>
            {
                new Cell(new Coordinate(2,2), CellState.Alive),
                new Cell(new Coordinate(2,3), CellState.Alive),
                new Cell(new Coordinate(3,2), CellState.Alive),
                new Cell(new Coordinate(3,3), CellState.Alive)
            };

            var grid = new Grid(listOfCells);
            var game = new Game(grid);
            var expectedGrid = new Grid(expectedReturnCells);

            var returnGrid = game.Tick();

            returnGrid.Should().Be(expectedGrid);
        }
    }
}
