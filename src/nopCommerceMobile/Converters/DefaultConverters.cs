using System;
using System.Globalization;
using nopCommerceMobile.Extensions;
using Xamarin.Forms;

namespace nopCommerceMobile.Converters
{
    public class NotEmptyStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;

            return !((string)value).IsNullOrEmpty();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class GreaterThanZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int) value) > 0;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
