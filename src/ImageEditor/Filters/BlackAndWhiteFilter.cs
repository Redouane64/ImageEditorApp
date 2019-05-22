using SkiaSharp;

namespace ImageEditor.Filters
{
    public class BlackAndWhiteFilter : IImageFilter
    {
        public string Name { get; } = "B & W";

        public SKColor[] Apply(SKColor[] pixels) => ImageEditor.Utilities.ImageFiltersHelpers.GreyScale(pixels);
    }
}
