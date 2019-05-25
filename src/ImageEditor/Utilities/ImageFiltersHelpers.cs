using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageEditor.Utilities
{
    public static class ImageFiltersHelpers
    {

        public static SkiaSharp.SKColor[] InvertColors(SkiaSharp.SKColor[] pixels)
        {
            SkiaSharp.SKColor[] newPixels = new SkiaSharp.SKColor[pixels.Length];

            for (int i = 0; i < pixels.Length; i++)
            {
                Byte r = (byte)(255 - pixels[i].Red);
                Byte g = (byte)(255 - pixels[i].Green);
                Byte b = (byte)(255 - pixels[i].Blue);

                newPixels[i] = new SkiaSharp.SKColor(r, g, b); // 0 255 255
            }

            return newPixels;
        }

        public static SkiaSharp.SKColor[] GreyScale(SkiaSharp.SKColor[] pixels)
        {
            SkiaSharp.SKColor[] newPixels = new SkiaSharp.SKColor[pixels.Length];

            for (int i = 0; i < pixels.Length; i++)
            {
                var value = (byte)((pixels[i].Red + pixels[i].Green + pixels[i].Blue) / 3);
                newPixels[i] = new SkiaSharp.SKColor(value, value, value);
            }

            return newPixels;
        }

        public static SkiaSharp.SKColor[] Scale(SkiaSharp.SKColor[] pixels, int height, int width, double scale)
        {
            double _width = width * scale;
            double _height = (_width * ((double)height / (double)width));

            var result = new SkiaSharp.SKColor[(int)(_width * _height)];

            double dx = width / _width;
            double dy = height / _height;

            var scaleX = new int[(int)_width];
            for (int i = 0; i < scaleX.Length; i++)
            {
                scaleX[i] = (int)(i * dx);
            }

            for (int j = 0; j < (int)_height; j++)
            {
                int y = (int)(j * dy);
                for (int k = 0; k < (int)_width; k++)
                {
                    result[(int)(j * _width + k)] = pixels[y * width + scaleX[k]];
                }
            }

            return result;
        }
    }
}
