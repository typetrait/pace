using Avalonia.Data.Converters;
using Pace.Common.Model;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Pace.Server.Converters;

public class FileSizeConverter : IMultiValueConverter
{
    public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
    {
        long size = (long)values[0];
        var fileType = (FileType)values[1];

        if (fileType == FileType.Directory)
            return "-";

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

        return $"{minifiedSize} {unit}";
    }
}