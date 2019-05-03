using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace ImageEditor.Converters
{
    internal class StreamToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;

            var stream = value as Stream;

            if (stream == null) throw new ArgumentException();

            var copyStream = Utilities.StreamHelpers.Copy(stream);

            // we need to give ImageSource.FromStream a copy because this API
            // disposes the stream after consumption.
            return ImageSource.FromStream(() => copyStream);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
