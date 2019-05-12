﻿using System;
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
        public static async Task<Stream> ApplyOperationAsync(this SkiaSharp.SKBitmap bitmap, Func<object, SkiaSharp.SKColor[]> operation)
        {
            Stream result = null;

            try
            {
                // This may be a long running process so we run it on a seperate thread
                // to avoid blocking the UI thread.
                var exec = new Task<SkiaSharp.SKColor[]>(new Func<object, SkiaSharp.SKColor[]>(operation), bitmap.Pixels);
                exec.Start();

                result = await exec.ContinueWith<Stream>(ToPngStream, TaskContinuationOptions.OnlyOnRanToCompletion);
            }
            catch (TaskCanceledException)
            {
                // Task cancelled due to failure.
            }
            finally { }

            return result;
        }

        // Grab the new pixels and encode it to a PNG image.
        private static Stream ToPngStream(Task<SkiaSharp.SKColor[]> pixels)
        {
            // Swap height and width.
            var height = ViewModels.MasterViewModel.Current.Bitmap.Width;
            var width = ViewModels.MasterViewModel.Current.Bitmap.Height;

            var _bitmap = new SkiaSharp.SKBitmap(width, height);
            _bitmap.Pixels = pixels.Result;

            var pixelsMap = new SkiaSharp.SKPixmap(_bitmap.Info, _bitmap.GetAddr(0, 0));

            MemoryStream pngImageStream = new MemoryStream();
            using (var wStream = new SkiaSharp.SKManagedWStream(pngImageStream))
            {
                pixelsMap.Encode(wStream, SkiaSharp.SKPngEncoderOptions.Default);
            }

            return pngImageStream;
        }
    }
}