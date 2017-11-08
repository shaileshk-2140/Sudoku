using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SudokuSolver;


namespace SudokuSolver.Test
{
    [TestClass]
    public class SudokuTest
    {
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        List<SudokuData> _sudokuDetails;

        [TestInitialize]
        public void TestInit()
        {
            _sudokuDetails = SudokuDetails.GetSudokuData();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _sudokuDetails = null;
        }

        /// <summary>
        /// This test method will test the sudoku solver. Input datasource is csv file.Array is delimited by '-'
        /// </summary>
        [TestMethod]
        [DeploymentItem("DataFile\\mycsv.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\DataFile\\mycsv.csv", "mycsv#csv", DataAccessMethod.Sequential)]
        public void Sudoku_Test()
        {
            var data = TestContext.DataRow["Array"].ToString() ;
            var expectedResult = TestContext.DataRow["ExpectedResult"].ToString() ;
            var splittedArray = data.Split('-');
            int?[,] grid = new int?[9, 9];
            int index = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    grid[i, j] = (!splittedArray[index].ToString().Trim().Equals("null"))? (int?)Convert.ToInt32(splittedArray[index]) : null;
                    index++;
                }
            }
            var sudoku = new Sudoku();

            var result = sudoku.SolveSudoku(grid);

            var isValidSudoku = Validate_Sudoku(result);

            Assert.AreEqual(isValidSudoku, Convert.ToBoolean(expectedResult));
        }
        /// <summary>
        /// This test method will check the null grid
        /// </summary>
        [TestMethod]
        public void Test_Sudoku_If_Grid_Is_Null()
        {
            var sudoku = new Sudoku();
            
            var result = sudoku.SolveSudoku(null);

            var isValidSudoku = Validate_Sudoku(null);

            Assert.AreEqual(isValidSudoku, false);

        }
        /// <summary>
        /// Test method to test sudoku over a given set of input. This is another way of testing with multiple values.
        /// </summary>
        [TestMethod]
        public void Test_Sudoku_For_Fixed_Set_Of_Input()
        {
            var sudoku = new Sudoku();
            foreach (var sudokuData in _sudokuDetails)
            {
                var result = sudoku.SolveSudoku(sudokuData.SudokuInitialGrid);

                var isValidSudoku = Validate_Sudoku(result);

                Assert.AreEqual(isValidSudoku, sudokuData.ExpectedResult);
            }
        }

        private bool Validate_Sudoku(int[,] grid)
        {
            if (grid == null) return false;
            //First check numbers are between 1-9
            for(int i = 0; i<9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if(grid[i,j] <= 0 || grid[i, j] > 9 || !IsValid(i,j,grid))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool IsValid(int rowIndex, int colIndex , int[,] grid)
        {
            for (int col = 0; col < 9; col++)
            {
                if (col != colIndex && grid[rowIndex, col] == grid[rowIndex, colIndex]) return false;
            }
            for (int row = 0; row < 9; row++)
            {
                if (row != rowIndex && grid[row, colIndex] == grid[rowIndex, colIndex]) return false;
            }
            for (int row = (rowIndex/3)*3; row < (rowIndex/3)*3 + 3; row++)
            {
                for (int col = (colIndex/3)*3; col <(colIndex/3)*3 + 3; col++)
                {
                    if (row != rowIndex && col != colIndex && grid[row, col] == grid[rowIndex, colIndex]) return false;
                }
            }
            return true;
        }
         
    }
}
