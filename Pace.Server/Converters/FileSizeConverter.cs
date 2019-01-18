using System;
using System.Globalization;
using System.Windows.Data;

namespace Pace.Server.Converters
{
    [ValueConversion(typeof(long), typeof(string))]
    public class FileSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long size = (long)value;

            string unit = "bytes";
            long minifiedSize = size;

            if (size >= 1000)
            {
                unit = "KB";
                minifiedSize = size / 1024;
            }
            if (size >= 1000000)
            {
                unit = "MB";
                minifiedSize = size / 1048576;
            }
            if (size >= 1000000000)
            {
                unit = "GB";
                minifiedSize = size / 1073741824;
            }

            return $"{minifiedSize.ToString()} {unit}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as string).Split(' ')[0];
        }
    }
}