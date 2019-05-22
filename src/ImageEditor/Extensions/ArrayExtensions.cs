using System;
using System.Collections.Generic;
using System.Text;

namespace ImageEditor.Extensions
{
    public static class ArrayExtensions
    {

        public static T[,] To2DMatrix<T>(this T[] source, int rows, int colums)
        {
            if(source.Length != rows * colums)
            {
                throw new InvalidOperationException();
            }

            var matrix = new T[rows, colums];
            int k = 0, i, j;

            for (i = 0; i < rows; i++)
            {
                for (j = 0; j < colums; j++)
                {
                    matrix[i, j] = source[k++];
                }
            }

            return matrix;
        }

        public static T[] ToArray<T>(this T[,] source)
        {
            int height = source.GetLength(0),
                width = source.GetLength(1),
                k = 0, j, i;

            var array = new T[height * width];

            for (i = 0; i < height; i++)
            {
                for (j = 0; j < width; j++)
                {
                    array[k++] = source[i, j];
                }
            }

            return array;
        }

    }
}
