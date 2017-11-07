using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public static class Convert2DNullbaleArrayTo2DArray
    {
        public static int[,] To2DArray(this int?[,] source, int row, int col)
        {
            if (source == null)
            {
                return null;
            }
            var result = new int[row, col];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    result[i, j] = source[i, j] ?? default(int); ;
                }
            }
            return result;
        }
    }
}
