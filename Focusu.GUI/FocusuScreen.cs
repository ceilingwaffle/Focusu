using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Focusu.GUI
{
    public class FocusuScreen
    {
        public FocusuScreen(Screen screen)
        {
            Screen = screen;
            BlankerForm = new ScreenBlankerForm(screen);
        }

        public Screen Screen { get; }

        public ScreenBlankerForm BlankerForm { get; }

        public bool IsBlanked { get; set; } = false;

        public bool IsEnabled { get; set; } = false;
    }
}
