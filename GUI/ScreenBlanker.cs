using System.Threading;
using System.Windows.Forms;

namespace GUI
{
    public class ScreenBlanker
    {
        private readonly FocusuScreenCollection screens;

        public ScreenBlanker(FocusuScreenCollection screens)
        {
            this.screens = screens;
        }

        public bool BlankEnabled()
        {
            bool successfullyBlankedAll = true;

            foreach (var screen in this.screens.SecondaryScreens)
            {
                if (screen.IsEnabled)
                {
                    if (!this.BlankOne(screen))
                    {
                        successfullyBlankedAll = false;
                    }
                }
            }

            return successfullyBlankedAll;
        }

        /// <summary>
        /// returns true if the screen was blanked (or is already blanked)
        /// </summary>
        /// <param name="screen"></param>
        /// <returns></returns>
        public bool BlankOne(FocusuScreen screen)
        {
            if (screen.IsBlanked)
            {
                return true;
            }

            var blankerThread = new Thread(new ThreadStart(screen.BlankerForm.Blank));
            blankerThread.Start();

            //screen.BlankerForm.Blank();
            bool wasBlanked = true;
            screen.IsBlanked = wasBlanked;
            return wasBlanked;
        }

        public bool UnblankEnabled()
        {
            bool successfullyUnblankedAll = true;

            foreach (var screen in this.screens.SecondaryScreens)
            {
                if (screen.IsEnabled)
                {
                    if (!this.UnblankOne(screen))
                    {
                        successfullyUnblankedAll = false;
                    }
                }
            }

            return successfullyUnblankedAll;
        }

        /// <summary>
        /// returns true if the screen was unblanked (or is already unblanked)
        /// </summary>
        /// <param name="screen"></param>
        /// <returns></returns>
        public bool UnblankOne(FocusuScreen screen)
        {
            if (!screen.IsBlanked)
            {
                return true;
            }

            var blankerThread = new Thread(new ThreadStart(screen.BlankerForm.Unblank));
            blankerThread.Start();

            //screen.BlankerForm.Unblank();
            bool wasUnblanked = true;
            screen.IsBlanked = !wasUnblanked;
            return wasUnblanked;
        }
    }
}
