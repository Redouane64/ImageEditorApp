using ImageEditor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageEditor.Extensions
{
    internal static class SKBitmapExtensions
    {
        public static async Task<SkiaSharp.SKBitmap> ApplyOperationAsync(this SkiaSharp.SKBitmap bitmap, Func<BitmapData, BitmapData> operation, BitmapData data)
        {

            try
            {
                // This may be a long running process so we run it on a seperate thread
                // to avoid blocking the UI thread.
                BitmapData result = await Task.Run<BitmapData>(function: () => operation(data), cancellationToken: CancellationToken.None);
                return new SkiaSharp.SKBitmap(new SkiaSharp.SKImageInfo(result.Width, result.Height)) { Pixels = result.Pixels };
            }
            catch (TaskCanceledException) { throw; }
            finally { }

        }

        public static async Task<Stream> ToPngStreamAsync(this SkiaSharp.SKBitmap bitmap)
        {
            var pixelsMap = new SkiaSharp.SKPixmap(bitmap.Info, bitmap.GetAddr(0, 0));

            MemoryStream pngImageStream = new MemoryStream();
            using (var wStream = new SkiaSharp.SKManagedWStream(pngImageStream))
            {
                pixelsMap.Encode(wStream, SkiaSharp.SKPngEncoderOptions.Default);
            }

            return await Task.FromResult(pngImageStream);
        }

    }
}
