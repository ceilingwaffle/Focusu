using Focusu.GUI.Options;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Focusu.GUI.Converters
{
    [ValueConversion(typeof(ControlMethod), typeof(bool))]
    public class ManualControlTypeBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ManualControlType setInBindingsControlMethod = (ManualControlType)value;
            ManualControlType elementParameterControlMethod = (ManualControlType)parameter;
            return setInBindingsControlMethod == elementParameterControlMethod;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}
