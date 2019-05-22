using System;

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

                newPixels[i] = new SkiaSharp.SKColor(r, g, b);
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
    }
}
