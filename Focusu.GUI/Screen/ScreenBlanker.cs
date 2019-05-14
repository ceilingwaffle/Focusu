using System;
using System.Threading;

namespace Focusu.GUI.Screen
{
    public class ScreenBlanker : IFocusBehaviour
    {
        private readonly FocusuScreenCollection screens;

        public ScreenBlanker(FocusuScreenCollection screens)
        {
            this.screens = screens;
        }

        /// <summary>
        /// returns true if all enabled screens were successfully blanked
        /// </summary>
        /// <returns></returns>
        public bool BlankEnabledSecondaryScreens()
        {
            bool successfullyBlankedAll = true;

            foreach (var screen in this.screens.SecondaryScreens)
            {
                if (screen.IsEnabled && !screen.IsBlanked)
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

        /// <summary>
        /// returns true if all screens were successfully unblanked
        /// </summary>
        /// <returns></returns>
        public bool UnblankAllSecondaryScreens()
        {
            bool successfullyUnblankedAll = true;

            foreach (var screen in this.screens.SecondaryScreens)
            {
                if (screen.IsBlanked)
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

        public bool Focus(EventArgs focusEventArgs = null)
        {
            return this.BlankEnabledSecondaryScreens();
        }

        public bool Unfocus(EventArgs unfocusEventArgs = null)
        {
            return this.UnblankAllSecondaryScreens();
        }
    }
}
