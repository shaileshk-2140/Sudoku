using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    /// <summary>
    /// This class solves a sudoku of 3X3 block in size
    /// </summary>
    public class Sudoku
    {
        /// <summary>
        /// This method reursively solve the sudoku using backtracking 
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        //public bool SolveSudoku(int?[,] grid)
        //{
        //    if (grid == null) return false;
        //    int row = 0, col = 0;
        //    if (!FindUnassignedLocation(grid, ref row, ref col)) return true; 

        //    for (int num = 1; num <= 9 ; num++)
        //    {
        //        if (isSafe(grid, row, col, num))
        //        {
        //            grid[row, col] = num;

        //            if (SolveSudoku(grid))
        //                return true;

        //            grid[row,col] = null;
        //        }
        //    }

        //    return false;
        //}
        public int[,] SolveSudoku(int?[,] grid)
        {
            if (grid == null) return null;
            int row = 0, col = 0;
            if (!FindUnassignedLocation(grid, ref row, ref col)) return grid.To2DArray(9,9);

            for (int num = 1; num <= 9; num++)
            {
                if (isSafe(grid, row, col, num))
                {
                    grid[row, col] = num;

                    var nonNullableGrid = SolveSudoku(grid);

                    int startRowIndex=0, startColIndex=0;

                    if (!FindUnassignedLocation(grid, ref startRowIndex, ref startColIndex))
                    {
                        return nonNullableGrid;
                    }

                    grid[row, col] = null;
                }
            }
            return grid.To2DArray(9,9);
        }
        /// <summary>
        /// This method would check for any empty block and returns the indices by ref
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private bool FindUnassignedLocation(int?[,] grid, ref int  row, ref int col)
        {
            for (row = 0; row < 9; row++)
                for (col = 0; col < 9; col++)
                    if (grid[row,col] == null)
                        return true;
            return false;
        }
        /// <summary>
        /// This method checks if the given number exist in selected row.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="row"></param>
        /// <param name="num"></param>
        /// <returns>true or false</returns>
        private bool UsedInRow(int? [,] grid, int row, int num)
        {
            for (int col = 0; col < 9; col++)
                if (grid[row, col] == num)
                    return true;
            return false;
        }
        /// <summary>
        /// This method check if the given number exist in selected col.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="col"></param>
        /// <param name="num"></param>
        /// <returns>true / false</returns>
        private bool UsedInCol(int? [,] grid, int col, int num)
        {
            for (int row = 0; row < 9; row++)
                if (grid[row,col] == num)
                    return true;
            return false;
        }
        /// <summary>
        /// This method check if the given number is present in the selected box
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="boxStartRow"></param>
        /// <param name="boxStartCol"></param>
        /// <param name="num"></param>
        /// <returns>true / false</returns>
        private bool UsedInBox(int? [,] grid, int boxStartRow, int boxStartCol, int num)
        {
            for (int row = 0; row < 3; row++)
                for (int col = 0; col < 3; col++)
                    if (grid[row + boxStartRow,col + boxStartCol] == num)
                        return true;
            return false;
        }

        /// <summary>
        /// This mehod checks if the number is be safe to insert.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        private bool isSafe(int? [,] grid, int row, int col, int num)
        {
             
            return !UsedInRow(grid, row, num) &&
                   !UsedInCol(grid, col, num) &&
                   !UsedInBox(grid, row - row % 3, col - col % 3, num);
        }
        /// <summary>
        /// Print the solved sudoku
        /// </summary>
        /// <param name="grid"></param>
        public void printGrid(int [,] grid)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                    Console.Write(grid[row,col] +"\t");
                Console.WriteLine("\n");
            }
        }
    }
}
