using Focusu.GUI.Options;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Focusu.GUI.Converters
{
    [ValueConversion(typeof(ControlMethod), typeof(string))]
    public class ControlMethodVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ControlMethod setInBindingsControlMethod = (ControlMethod)value;
            ControlMethod elementParameterControlMethod = (ControlMethod)parameter;
            return setInBindingsControlMethod == elementParameterControlMethod ? "Visible" : "Collapsed";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals("Visible") ? parameter : Binding.DoNothing;
        }
    }
}
