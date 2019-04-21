namespace GUI
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Controller controller;

        public MainWindow()
        {
            InitializeComponent();

            AppState_RadioButton_Automatic.IsChecked = true;
            this.AppState_RadioButton_Disable.IsChecked = true;

            this.controller = new Controller();
        }

        private void AppState_Slider_FadeTiming_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double ms = e.NewValue;
            double seconds = e.NewValue / 1000;
            this.Label_FadeTiming.Content = $"{seconds:F2} seconds";

            this.controller.HandleFadeTimingChanged(ms);
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
            e.Handled = !Helpers.IsValidNumberInput(e.Text);
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

            if (!(border.Child is StackPanel panel))
            {
                return;
            }

            foreach (object panelElement in panel.Children)
            {
                switch (panelElement)
                {
                    // TODO: Get/Set opacity/enabled from a variable from the xaml, or somewhere else other than this code
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
