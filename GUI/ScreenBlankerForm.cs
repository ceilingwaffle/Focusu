using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
    internal class ScreenBlankerForm : Form
    {
        // Shows the form as topmost without stealing focus: https://stackoverflow.com/a/25219414
        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }
        private const int WS_EX_TOPMOST = 0x00000008;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= WS_EX_TOPMOST;
                return createParams;
            }
        }

        public ScreenBlankerForm(Screen targetScreen)
        {
            InitializeComponent();

            BackColor = Color.Black;
            Opacity = 1;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            Location = targetScreen.WorkingArea.Location;
            Bounds = targetScreen.Bounds;
            //TopMost = true;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "BlankerForm";
            this.Load += new System.EventHandler(this.OnLoad);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseClick);
            this.ResumeLayout(false);
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            // TODO: Disable the monitor from blanking and display notification to the user that the screen is now disabled
        }

        private void OnLoad(object sender, EventArgs e)
        {
        }
    }
}
