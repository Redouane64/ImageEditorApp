using System;
using System.Collections.Generic;
using System.Text;
using ImageEditor.Utilities;

namespace ImageEditor.Services
{
    public class ImageFiltersService
    {
        public ImageFiltersService() { }

        public SkiaSharp.SKColor[] InvertColors(SkiaSharp.SKColor[] pixels)
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

    }
}
