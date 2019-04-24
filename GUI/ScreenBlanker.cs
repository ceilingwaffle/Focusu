using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
    public class ScreenBlanker
    {
        public ScreenBlanker()
        {
        }

        public void Blank(params Screen[] targetScreens)
        {
            foreach (var target in targetScreens)
            {
                Form blankerForm = new ScreenBlankerForm(target);
            }
        }
    }
}
