using Focusu.GUI.Options;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Focusu.GUI.Converters
{
    [ValueConversion(typeof(bool), typeof(string))]
    public class BooleanVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // value = e.g. checkbox checked/unchecked
            return (bool)value ? "Visible" : "Collapsed";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals("Visible") ? parameter : Binding.DoNothing;
        }
    }
}
