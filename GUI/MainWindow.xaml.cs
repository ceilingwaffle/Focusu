using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Focusu
{
    using System.Text.RegularExpressions;

    using Button = System.Windows.Controls.Button;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TimeSpan fadeTime = TimeSpan.FromMilliseconds(0);

        public MainWindow()
        {
            InitializeComponent();

            AppState_RadioButton_Automatic.IsChecked = true;
        }

        /// <summary>
        /// Returns true if the given string contains only numbers.
        /// </summary>
        /// <param name="text">
        /// The text to check
        /// </param>
        /// <returns>
        /// true if valid number
        /// </returns>
        private static bool IsValidNumberInput(string text)
        {
            var regex = new Regex("[0-9]+");
            return regex.IsMatch(text);
        }

        private void AppState_Slider_FadeTiming_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double ms = e.NewValue;
            this.fadeTime = TimeSpan.FromMilliseconds(ms);
            double seconds = e.NewValue / 1000;
            this.Label_FadeTiming.Content = $"{seconds:F2} seconds";
        }

        private void AppState_RadioButton_Manual_Checked(object sender, RoutedEventArgs e)
        {
            this.GroupBox_ManualControls.Visibility = Visibility.Visible;
            this.GroupBox_AutomaticOptions.Visibility = Visibility.Collapsed;
        }

        private void AppState_RadioButton_Automatic_Checked(object sender, RoutedEventArgs e)
        {
            this.GroupBox_AutomaticOptions.Visibility = Visibility.Visible;
            this.GroupBox_ManualControls.Visibility = Visibility.Collapsed;
        }

        private void AppState_CheckBox_MaxPP_Checked(object sender, RoutedEventArgs e)
        {
            AppState_TextBox_MaxPP.Visibility = Visibility.Visible;
        }

        private void AppState_CheckBox_MaxPP_Unchecked(object sender, RoutedEventArgs e)
        {
            AppState_TextBox_MaxPP.Visibility = Visibility.Collapsed;
        }

        private void AppState_TextBox_MaxPP_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValidNumberInput(e.Text);
        }

        private void AppState_TextBox_MaxPP_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            // ignore anything attempted to be pasted into the text box
            e.CancelCommand();
        }

        private void AppState_TextBox_MaxPP_OnKeyUp(object sender, KeyEventArgs e)
        {
            // if they press enter after entering the value, do something so it doesn't look its ignoring their attempt at saving the value.
            if (e.Key is Key.Enter)
            {
                this.AppState_TextBox_MaxPP.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void AppState_RadioButton_Enable_Checked(object sender, RoutedEventArgs e)
        {
            // TODO: Enable the screen blanker
        }

        private void AppState_RadioButton_Disable_Checked(object sender, RoutedEventArgs e)
        {
            // TODO: Disable the screen blanker
        }

        private void Monitor_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is Border border))
            {
                return;
            }

            if (!(border.Child is Grid grid))
            {
                return;
            }

            foreach (object gridElement in grid.Children)
            {
                switch (gridElement)
                {
                    case Image image:
                        image.Opacity = image.Opacity <= 0.5 ? 1.0 : 0.5;
                        break;
                    case Label label:
                        label.Content = label.Content.ToString().Contains("Enabled") ? "Disabled" : "Enabled";
                        break;
                }
            }


            // swap DynamicResource 
            //// image.Source = (ImageSource)this.FindResource("BlankedMonitor");
        }
    }
}
