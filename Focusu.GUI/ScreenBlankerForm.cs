using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace GUI
{
    public class ScreenBlankerForm : Form
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

        public Screen TargetScreen { get; }

        public ScreenBlankerForm(Screen targetScreen)
        {
            InitializeComponent();

            BackColor = Color.Black;
            Opacity = 1;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            Location = targetScreen.WorkingArea.Location;
            Bounds = targetScreen.Bounds;
            TargetScreen = targetScreen;
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
            // TODO: temporarily set TargetScreen.IsEnabled = false (but reset to true when the user re-enters the osu! window)
            this.Unblank();
        }

        private void OnLoad(object sender, EventArgs e)
        {
        }

        delegate void BlankCallback();

        public void Blank()
        {
            if (this.InvokeRequired)
            {
                BlankCallback d = new BlankCallback(Blank);
                this.Invoke(d, new object[] { });
            }
            else
            {
                this.Visible = false;
                this.ShowDialog();
            }
        }

        delegate void UnblankCallback();

        public void Unblank()
        {
            if (this.InvokeRequired)
            {
                UnblankCallback d = new UnblankCallback(Unblank);
                this.Invoke(d, new object[] { });
            }
            else
            {
                this.Visible = false;
                this.Hide();
            }
        }
    }
}
