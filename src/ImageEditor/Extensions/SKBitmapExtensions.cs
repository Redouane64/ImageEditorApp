using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditor.Extensions
{
    internal static class SKBitmapExtensions
    {
        /// <summary>
        /// Apply an operation on pixels of the image.
        /// </summary>
        /// <param name="bitmap">Bitmap instance.</param>
        /// <param name="operation">Function that accepts and returns an array of pixels</param>
        /// <returns>Stream of PNG image.</returns>
        public static async Task<Stream> ApplyOperationAsync(this SkiaSharp.SKBitmap bitmap, Func<SkiaSharp.SKColor[], SkiaSharp.SKColor[]> operation)
        {
            Stream result = null;

            try
            {
                // This may be a long running process so we run it on a seperate thread
                // to avoid blocking the UI thread.
                result = await Task.Run(() => operation(bitmap.Pixels))
                        .ContinueWith<Stream>((t) => { 

                        // Grab the new pixels and encode it to a PNG image.
                        //bitmap.Pixels = t.Result;

                        var _bitmap = new SkiaSharp.SKBitmap(bitmap.Height, bitmap.Width);
                        _bitmap.Pixels = t.Result;

                        var pixelsMap = new SkiaSharp.SKPixmap(_bitmap.Info, _bitmap.GetAddr(0, 0));

                        MemoryStream output = new MemoryStream();
                        using (var wStream = new SkiaSharp.SKManagedWStream(output))
                        {
                            pixelsMap.Encode(wStream, SkiaSharp.SKPngEncoderOptions.Default);
                        }
                
                        return output;

                }, TaskContinuationOptions.OnlyOnRanToCompletion);

            }
            catch (TaskCanceledException tce)
            {
                // Task cancelled due to failure.
            }
            finally { }

            return result;
        }
    }
}
