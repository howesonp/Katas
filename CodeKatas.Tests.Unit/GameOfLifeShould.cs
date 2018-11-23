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
            var returnGrid = grid.Tick();

            returnGrid.Should().Be(new EmptyGrid());
        }

        [Test]
        public void ReturnEmptyGrid_GivenGridWithOneLiveCell()
        {
            // | x|  |  | =>  |  |  |  |  
            // |  |  |  |     |  |  |  |   
            // |  |  |  |     |  |  |  |  

            var listOfCells = new List<Cell>
            {
                new Cell(new Coordinate(1,1))
            };

            var grid = new Grid(listOfCells);
            var expectedGrid = new Grid(new List<Cell>());

            var returnGrid = grid.Tick();

            returnGrid.Should().Be(expectedGrid);
        }

        [Test]
        public void ReturnGridWithOneCell_GivenGridWithThreeDiagonalLiveCells()
        {
            // | x|  |  | =>  |  |  |  |  
            // |  | x|  |     |  | x|  |   
            // |  |  | x|     |  |  |  |  

            var listOfCells = new List<Cell>
            {
                new Cell(new Coordinate(1,1)),
                new Cell(new Coordinate(2,2)),
                new Cell(new Coordinate(3,3))
            };

            var expectedReturnCells = new List<Cell>
            {
                new Cell(new Coordinate(2,2))
            }; 

            var grid = new Grid(listOfCells);
            var expectedGrid = new Grid(expectedReturnCells);

            var returnGrid = grid.Tick();

            returnGrid.Should().Be(expectedGrid);
        }

        [Test]
        public void ReturnThreeHorizontalLiveCells_GivenGridWhichThreeVerticalLiveCells_TwoLiveCellsDieTwoCellsSpawn()
        {
            // | x|  |  | =>  |  |  |  |  |
            // | x|  |  |     | x| x| x|  | 
            // | x|  |  |     |  |  |  |  |

            var listOfCells = new List<Cell>
            {
                new Cell(new Coordinate(1,1)),
                new Cell(new Coordinate(2,1)),
                new Cell(new Coordinate(3,1))
            };

            var expectedReturnCells = new List<Cell>
            {
                new Cell(new Coordinate(2,0)),
                new Cell(new Coordinate(2,1)),
                new Cell(new Coordinate(2,2))
            };

            var grid = new Grid(listOfCells);
            var expectedGrid = new Grid(expectedReturnCells);

            var returnGrid = grid.Tick();

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
                new Cell(new Coordinate(2,2)),
                new Cell(new Coordinate(2,3)),
                new Cell(new Coordinate(3,2))
            };

            var expectedReturnCells = new List<Cell>
            {
                new Cell(new Coordinate(2,2)),
                new Cell(new Coordinate(2,3)),
                new Cell(new Coordinate(3,2)),
                new Cell(new Coordinate(3,3))
            };

            var grid = new Grid(listOfCells);
            var expectedGrid = new Grid(expectedReturnCells);

            var returnGrid = grid.Tick();

            returnGrid.Should().Be(expectedGrid);
        }

        [Test]
        public void ReturnThreeLiveCells_GivenGridWhichFourLiveCells_ThreeLiveCellsDieAndTwoAreSpawned()
        {
            // |  |  | x| =>  |  |  |  |
            // |  | x|  |     |  | x| x| x1 - y3
            // | x|  | x|     |  | x|  |

            var listOfCells = new List<Cell>
            {
                new Cell(new Coordinate(1,3)),
                new Cell(new Coordinate(2,2)),
                new Cell(new Coordinate(3,1)),
                new Cell(new Coordinate(3,3)),
            };

            var expectedReturnCells = new List<Cell>
            {
                new Cell(new Coordinate(2,2)),
                new Cell(new Coordinate(2,3)),
                new Cell(new Coordinate(3,2))
            };

            var grid = new Grid(listOfCells);
            var expectedGrid = new Grid(expectedReturnCells);

            var returnGrid = grid.Tick();

            returnGrid.Should().Be(expectedGrid);
        }

        [Test]
        public void ReturnGridWithSevenLiveCells_GivenGridWithFourLiveCells_FourStayAliveAndThreeSpawn()
        {
            //                |  | x|  |  
            // | x| x| x| =>  | x| x| x|
            // |  | x|  |     | x| x| x| 
            // |  |  |  |     |  |  |  |

            var listOfCells = new List<Cell>
            {
                new Cell(new Coordinate(1,1)),
                new Cell(new Coordinate(1,2)),
                new Cell(new Coordinate(1,3)),
                new Cell(new Coordinate(2,2))
            };

            var expectedReturnCells = new List<Cell>
            {
                new Cell(new Coordinate(0,2)),
                new Cell(new Coordinate(1,1)),
                new Cell(new Coordinate(1,2)),
                new Cell(new Coordinate(1,3)),
                new Cell(new Coordinate(2,1)),
                new Cell(new Coordinate(2,2)),
                new Cell(new Coordinate(2,3))
            };

            var grid = new Grid(listOfCells);
            var expectedGrid = new Grid(expectedReturnCells);

            var returnGrid = grid.Tick();

            returnGrid.Should().Be(expectedGrid);
        }

        [Test]
        public void ReturnGridWithFiveLiveCells_GivenGridWithFiveLiveCells_WithTwoDyingAndTwoSpawning()
        {  
            //                |  | x|  |  |
            // | x| x| x| =>  |  | x| x|  |
            // |  |  | x|     |  |  | x| x|
            // |  |  | x|     |  |  |  |  |

            var listOfCells = new List<Cell>
            {
                new Cell(new Coordinate(1,1)),
                new Cell(new Coordinate(1,2)),
                new Cell(new Coordinate(1,3)),
                new Cell(new Coordinate(2,3)),
                new Cell(new Coordinate(3,3))
            };

            var expectedReturnCells = new List<Cell>
            {
                new Cell(new Coordinate(0,2)),
                new Cell(new Coordinate(1,2)),
                new Cell(new Coordinate(1,3)),
                new Cell(new Coordinate(2,3)),
                new Cell(new Coordinate(2,4)),
            };

            var grid = new Grid(listOfCells);
            var expectedGrid = new Grid(expectedReturnCells);

            var returnGrid = grid.Tick();

            returnGrid.Should().Be(expectedGrid);
        }

        [Test]
        public void ReturnGridWithFourLiveCells_GivenGridWithNineLiveCells()
        { 
            // | x | x |   | x |  =>  | x | x |   |   |
            // |   | x | x |   |      |   |   |   |   |
            // | x | x |   |   |      |   |   |   |   |
            // | x | x |   |   |      | x | x |   |   |

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
                new Cell(new Coordinate(4,1)),
                new Cell(new Coordinate(4,2))
            };

            var grid = new Grid(listOfCells);
            var expectedGrid = new Grid(expectedReturnCells);

            var returnGrid = grid.Tick();

            returnGrid.Should().Be(expectedGrid);
        }

        [Test]
        public void ReturnCorrectGrids_GivenGridWithFiveLiveCells_AndWhenTickingTwice()
        {
            //                                   
            //                |  | x|  |  |      |  | x| x|  |
            // | x| x| x| =>  |  | x| x|  | =>   |  | x|  | x|
            // |  |  | x|     |  |  | x| x|      |  | x| x| x|
            // |  |  | x|     |  |  |  |  |      |  |  |  |  |

            var listOfCells = new List<Cell>
            {
                new Cell(new Coordinate(1,1)),
                new Cell(new Coordinate(1,2)),
                new Cell(new Coordinate(1,3)),
                new Cell(new Coordinate(2,3)),
                new Cell(new Coordinate(3,3))
            };

            var expectedReturnCellsForTickOne = new List<Cell>
            {
                new Cell(new Coordinate(0,2)),
                new Cell(new Coordinate(1,2)),
                new Cell(new Coordinate(1,3)),
                new Cell(new Coordinate(2,3)),
                new Cell(new Coordinate(2,4))
            };

            var expectedReturnCellsForTickTwo = new List<Cell>
            {
                new Cell(new Coordinate(0,2)),
                new Cell(new Coordinate(0,3)),
                new Cell(new Coordinate(1,2)),
                new Cell(new Coordinate(1,4)),
                new Cell(new Coordinate(2,2)),
                new Cell(new Coordinate(2,3)),
                new Cell(new Coordinate(2,4))
            };

            var grid = new Grid(listOfCells);
            var expectedGridOne = new Grid(expectedReturnCellsForTickOne);
            var expectedGridTwo = new Grid(expectedReturnCellsForTickTwo);

            var returnGrid = grid.Tick();

            returnGrid.Should().Be(expectedGridOne);

            var returnGridTwo = grid.Tick();

            returnGridTwo.Should().Be(expectedGridTwo);
        }
    }
}
