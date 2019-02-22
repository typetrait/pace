using Pace.Common.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Pace.Server.Converters
{
    [ValueConversion(typeof(FileType), typeof(string))]
    public class FileTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (FileType)value;
            return type == FileType.File ? Resources.Strings.FileType_File : Resources.Strings.FileType_Folder;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (string)value;
            return type == Resources.Strings.FileType_File ? FileType.File : FileType.Directory;
        }
    }
}