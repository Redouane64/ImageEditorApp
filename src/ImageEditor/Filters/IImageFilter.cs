using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace ImageEditor.Filters
{
    public interface IImageFilter
    {
        string Name { get; }
        SKColor[] Apply(SKColor[] pixels);
    }
}
