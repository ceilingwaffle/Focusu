using System;
using System.Collections.Generic;
using System.Text;

namespace Focusu.GUI
{
    public interface IFocusBehaviour
    {
        bool Focus(EventArgs focusEventArgs);

        bool Unfocus(EventArgs unfocusEventArgs);
    }
}
