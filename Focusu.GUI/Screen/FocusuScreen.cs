namespace Focusu.GUI.Screen
{
    public class FocusuScreen
    {
        public FocusuScreen(System.Windows.Forms.Screen screen)
        {
            Screen = screen;
            BlankerForm = new ScreenBlankerForm(screen);
        }

        public System.Windows.Forms.Screen Screen { get; }

        public ScreenBlankerForm BlankerForm { get; }

        public bool IsBlanked { get; set; } = false;

        public bool IsEnabled { get; set; } = false;
    }
}
