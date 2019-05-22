using SkiaSharp;

namespace ImageEditor.Filters
{
    public class InvertColorsFilter : IImageFilter
    {
        public string Name { get; } = "Invert Colors";

        public SKColor[] Apply(SKColor[] pixels) => ImageEditor.Utilities.ImageFiltersHelpers.InvertColors(pixels);
    }
}
