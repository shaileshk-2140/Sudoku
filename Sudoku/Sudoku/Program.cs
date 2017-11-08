using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            int?[,] input = new int?[,]
            {
                {3, null, 6, 5, null, 8, 4, null, null},
                {5, 2, null, null, null, null, null, null, null},
                {null, 8, 7, null, null, null, null, 3, 1},
                {null, null, 3, null, 1, null, null, 8, null},
                {9, null, null, 8, 6, 3, null, null, 5},
                {null, 5, null, null, 9, null, 6, null, null},
                {1, 3, null, null, null, null, 2, 5, null},
                {null, null, null, null, null, null, null, 7, 4},
                {null, null, 5, 2, null, 6, 3, null, null}
            };

            Sudoku s = new Sudoku();

            var x = s.SolveSudoku(input);

            s.printGrid(s.SolveSudoku(input));
            
            Console.ReadLine();
        }
    }
}
