using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageEditor.Utilities
{
    public static class StreamHelpers
    {

        public static Stream Copy(Stream source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            try
            {
                // Source stream reading position is garenteed to be zero
                // at this line of code.
                var buffer = new byte[source.Length];
                source.Read(buffer, 0, buffer.Length);

                // Reset read position of source stream to begining.
                source.Seek(0, SeekOrigin.Begin);

                return new MemoryStream(buffer);
            }
            finally
            {
            }
        }

        public static void ShouldSeekToZero(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (stream.CanSeek && stream.Position != 0)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }
        }

    }
}
