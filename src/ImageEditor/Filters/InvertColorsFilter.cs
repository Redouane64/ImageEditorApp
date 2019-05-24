using SkiaSharp;

namespace ImageEditor.Filters
{
    public class InvertColorsFilter : IImageFilter
    {
        public string Name { get; } = "Invert Colors";

        public SKColor[] Apply(SKColor[] pixels, params object[] args) => Utilities.ImageFiltersHelpers.InvertColors(pixels);
    }
}
