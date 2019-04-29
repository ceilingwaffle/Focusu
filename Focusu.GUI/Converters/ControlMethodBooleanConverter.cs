using Focusu.GUI.Options;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Focusu.GUI.Converters
{
    [ValueConversion(typeof(ControlMethod), typeof(bool))]
    public class ControlMethodBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // this is what is set in Bindings (e.g. ControlMethod.Manual)
            ControlMethod setInBindingsControlMethod = (ControlMethod)value;

            // this is the ConverterParameter attribute of the target (UI element) 
            ControlMethod elementParameterControlMethod = (ControlMethod)parameter;

            return setInBindingsControlMethod == elementParameterControlMethod; // to target (MainWindow)
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // if the target (UI element) was set to true, set Bindings.ControlMethod to whatever its parameter is (e.g. ControlMethod.Automatic)
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}
