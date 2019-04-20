﻿using System;
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
    }
}
