using System;
using System.Globalization;
using System.Windows.Data;

namespace SearchDocWindow.Converters
{
    /// <summary>
    /// Конвертер для индекса сортировки в GridControl.
    /// </summary>
    class SortIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int)value < 0) ? "" : ((int)value + 1).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
