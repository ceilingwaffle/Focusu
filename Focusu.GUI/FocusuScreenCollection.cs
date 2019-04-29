using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Forms;

namespace Focusu.GUI
{
    public class FocusuScreenCollection : ObservableCollection<FocusuScreen>
    {
        public FocusuScreen PrimaryScreen { get; set; }
        public List<FocusuScreen> SecondaryScreens { get; set; } = new List<FocusuScreen>();
    }
}
