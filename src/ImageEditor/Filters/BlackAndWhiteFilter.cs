using SkiaSharp;

namespace ImageEditor.Filters
{
    public class BlackAndWhiteFilter : IImageFilter
    {
        public string Name { get; } = "B & W";

        public SKColor[] Apply(SKColor[] pixels, params object[] args) => Utilities.ImageFiltersHelpers.GreyScale(pixels);
    }
}
