using System;
using System.Collections.Generic;
using System.Text;
using ImageEditor.Extensions;

namespace ImageEditor.Utilities
{
    public static class PixelsArrayHelpers
    {

        public static T[] RotateCW<T>(T[] input, int rows, int columns) where T : struct
        {
            T[] result = new T[rows * columns];

            int i = input.Length - 1,
                j = 0, p = 0;

            for (; p < columns; p++)
            {
                int offset = i - (columns - 1) + p;

                while (offset >= p)
                {
                    result[j++] = input[offset];

                    offset -= columns;
                }
            }

            return result;
        }

        public static T[] RotateCCW<T>(T[] input, int rows, int columns)
        {
            T[] result = new T[rows * columns];

            int offset, j = 0;

            for (offset = 1; offset <= columns; offset++)
            {
                int i = columns - offset;

                while (i < input.Length)
                {
                    result[j++] = input[i];
                    i += columns;
                }
            }

            return result;
        }

    }
}
