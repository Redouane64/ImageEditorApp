using System;
using System.Collections.Generic;
using System.Text;

namespace ImageEditor.Models
{
    public class BitmapData
    {
        public BitmapData() { }

        public int Height { get; set; }

        public int Width { get; set; }

        public SkiaSharp.SKColor[] Pixels { get; set; }

        public object Extras { get; set; }
    }
}
