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
                new Cell(1, 1)
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
                new Cell(1, 1, CellState.Alive),
                new Cell(2, 2, CellState.Alive),
                new Cell(3, 3, CellState.Alive)
            };

            var expectedReturnCells = new List<Cell>
            {
                new Cell(2, 2, CellState.Alive)
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
                new Cell(1, 1, CellState.Alive),
                new Cell(1, 2, CellState.Alive),
                new Cell(1, 3, CellState.Alive)
            };

            var expectedReturnCells = new List<Cell>
            {
                new Cell(0, 2, CellState.Alive),
                new Cell(1, 2, CellState.Alive),
                new Cell(2, 2, CellState.Alive),
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
                new Cell(2, 2, CellState.Alive),
                new Cell(2, 3, CellState.Alive),
                new Cell(3, 2, CellState.Alive)
            };

            var expectedReturnCells = new List<Cell>
            {
                new Cell(2, 2, CellState.Alive),
                new Cell(2, 3, CellState.Alive),
                new Cell(3, 2, CellState.Alive),
                new Cell(3, 3, CellState.Alive)
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
                new Cell(1, 3, CellState.Alive),
                new Cell(2, 2, CellState.Alive),
                new Cell(3, 1, CellState.Alive),
                new Cell(3, 3, CellState.Alive)
            };

            var expectedReturnCells = new List<Cell>
            {
                new Cell(2, 2, CellState.Alive),
                new Cell(3, 2, CellState.Alive),
                new Cell(2, 3, CellState.Alive)
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
                new Cell(1, 1, CellState.Alive),
                new Cell(1, 2, CellState.Alive),
                new Cell(1, 3, CellState.Alive),
                new Cell(2, 2, CellState.Alive)
            };

            var expectedReturnCells = new List<Cell>
            {
                new Cell(0, 2, CellState.Alive),
                new Cell(1, 1, CellState.Alive),
                new Cell(1, 2, CellState.Alive),
                new Cell(1, 3, CellState.Alive),
                new Cell(2, 1, CellState.Alive),
                new Cell(2, 2, CellState.Alive),
                new Cell(2, 3, CellState.Alive)
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
                new Cell(1, 1, CellState.Alive),
                new Cell(1, 2, CellState.Alive),
                new Cell(1, 3, CellState.Alive),
                new Cell(2, 3, CellState.Alive),
                new Cell(3, 3, CellState.Alive)
            };

            var expectedReturnCells = new List<Cell>
            {
                new Cell(0, 2, CellState.Alive),
                new Cell(1, 2, CellState.Alive),
                new Cell(1, 3, CellState.Alive),
                new Cell(2, 3, CellState.Alive),
                new Cell(2, 4, CellState.Alive)
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
                new Cell(1, 1, CellState.Alive),
                new Cell(1, 2, CellState.Alive),
                new Cell(1, 4, CellState.Alive),
                new Cell(2, 2, CellState.Alive),
                new Cell(2, 3, CellState.Alive),
                new Cell(3, 1, CellState.Alive),
                new Cell(3, 2, CellState.Alive),
                new Cell(4, 1, CellState.Alive),
                new Cell(4, 2, CellState.Alive)
            };

            var expectedReturnCells = new List<Cell>
            {
                new Cell(1, 1, CellState.Alive),
                new Cell(1, 2, CellState.Alive),
                new Cell(4, 1, CellState.Alive),
                new Cell(4, 2, CellState.Alive)
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
                new Cell(1, 1, CellState.Alive),
                new Cell(1, 2, CellState.Alive),
                new Cell(1, 3, CellState.Alive),
                new Cell(2, 3, CellState.Alive),
                new Cell(3, 3, CellState.Alive)
            };

            var expectedReturnCellsForTickOne = new List<Cell>
            {
                new Cell(0, 2, CellState.Alive),
                new Cell(1, 2, CellState.Alive),
                new Cell(1, 3, CellState.Alive),
                new Cell(2, 3, CellState.Alive),
                new Cell(2, 4, CellState.Alive)
            };

            var expectedReturnCellsForTickTwo = new List<Cell>
            {
                new Cell(0, 2, CellState.Alive),
                new Cell(0, 3, CellState.Alive),
                new Cell(1, 2, CellState.Alive),
                new Cell(1, 4, CellState.Alive),
                new Cell(2, 2, CellState.Alive),
                new Cell(2, 3, CellState.Alive),
                new Cell(2, 4, CellState.Alive)
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
