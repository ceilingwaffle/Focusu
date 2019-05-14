using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Focusu.GUI.Screen
{
    public class FocusuScreenCollection : ObservableCollection<FocusuScreen>
    {
        public FocusuScreen PrimaryScreen { get; set; }
        public List<FocusuScreen> SecondaryScreens { get; set; } = new List<FocusuScreen>();
    }
}
