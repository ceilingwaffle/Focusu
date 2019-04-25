namespace GUI
{
    using System.Windows;

    using OsuStatePresenter;
    using DVPF.Core;
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;

    /// <inheritdoc />
    /// <summary>
    /// The controller.
    /// </summary>
    public class Controller : DependencyObject
    {
        protected readonly OsuPresenter osuStatePresenter;
        protected readonly Bindings dataBindings;
        protected readonly ScreenBlanker screenBlanker;

        public Controller(Bindings dataBindings)
        {
            this.dataBindings = dataBindings;
            this.screenBlanker = new ScreenBlanker(this.CollectScreens());

            osuStatePresenter = new OsuPresenter(HandleOsuGameStateCreated);
            osuStatePresenter.Start();
        }

        private FocusuScreenCollection CollectScreens()
        {
            var focusuScreenCollection = new FocusuScreenCollection();
            
            foreach (var screen in Screen.AllScreens)
            {
                var focusuScreen = new FocusuScreen(screen);

                if (screen.Primary)
                {
                    focusuScreen.IsEnabled = false;
                    focusuScreenCollection.PrimaryScreen = focusuScreen;
                }
                else
                {
                    focusuScreen.IsEnabled = true;
                    focusuScreenCollection.SecondaryScreens.Add(focusuScreen);
                }
            }

            return focusuScreenCollection;
        }

        private void ProcessOsuGameState(State newState)
        {
            // update the data bindings
            this.dataBindings.OsuStatus = this.ProcessOsuStatus(newState);

            // TODO: decide whether or not to blank the screen
            if (this.dataBindings.OsuStatus == OsuStatus.Playing)
            {
                if (this.dataBindings.IsBlanked)
                {
                    return;
                }

                bool blankingSucceeded = this.screenBlanker.BlankEnabled();
                this.dataBindings.IsBlanked = blankingSucceeded;
            }
            else
            {
                if (!this.dataBindings.IsBlanked)
                {
                    return;
                }

                bool unblankingSucceeded = this.screenBlanker.UnblankEnabled();
                this.dataBindings.IsBlanked = !unblankingSucceeded;
            }
        }

        private OsuStatus ProcessOsuStatus(State newState)
        {
            if (!TryCastOsuStateProperty(newState, "GameStatus", out string status))
            {
                status = "Unknown";
            }

            if (!TryCastOsuStateProperty(newState, "SongIsPaused", out bool isPaused))
            {
                isPaused = false;
            }

            if (!TryCastOsuStateProperty(newState, "IsMapBreak", out bool isMapBreak))
            {
                isMapBreak = false;
            }

            if (isPaused == false && status.Equals("Unknown"))
            {
                return OsuStatus.Unknown;
            }

            if (isPaused == false && status.Equals("Playing") && isMapBreak == true)
            {
                return OsuStatus.InMapBreak;
            }

            if (status.Equals("Playing") && isPaused == true)
            {
                return OsuStatus.SongPaused;
            }

            if (status.Length < 1)
            {
                return OsuStatus.Unknown;
            }

            // try to get the enum property name as a string
            if (!Enum.TryParse(status, true, out OsuStatus osuStatus))
            {
                return OsuStatus.Unknown;
            }

            return osuStatus;
        }

        private bool TryCastOsuStateProperty<T>(State state, string statePropName, out T result)
        {
            try
            {
                object oResult = state[statePropName];

                if (oResult != null)
                {
                    result = (T)oResult;
                    return true;
                }

            }
            catch
            {
                Debug.WriteLine($"ERROR: Unable to cast {statePropName} to type {typeof(T).ToString()}");
            }

            result = default;
            return false;
        }

        public void HandleFadeTimingChanged(double ms)
        {
            // TODO
            //Bindings.FadeTiming = TimeSpan.FromMilliseconds(ms);
        }

        public void HandleOsuGameStateCreated(State newState)
        {
            this.ProcessOsuGameState(newState);
        }
    }
}
