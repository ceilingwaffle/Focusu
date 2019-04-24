namespace GUI
{
    using System.Windows;

    using OsuStatePresenter;
    using DVPF.Core;
    using System;
    using System.Diagnostics;

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
            this.screenBlanker = new ScreenBlanker();
            osuStatePresenter = new OsuPresenter(HandleOsuGameStateCreated);
            osuStatePresenter.Start();
        }

        public void HandleFadeTimingChanged(double ms)
        {
            //Bindings.FadeTiming = TimeSpan.FromMilliseconds(ms);
        }

        public void HandleOsuGameStateCreated(State newState)
        {
            this.ProcessOsuGameState(newState);
        }

        private void ProcessOsuGameState(State newState)
        {
            // update the data bindings
            this.dataBindings.OsuStatus = this.ProcessOsuStatus(newState);

            // decide whether or not to blank the screen
        }

        private OsuStatus ProcessOsuStatus(State newState)
        {
            // TODO: catch potential cast exceptions
            string status = "Unknown";
            bool isPaused = false;

            try
            {
                object oStatus = newState["GameStatus"];

                if (oStatus != null)
                {
                    status = (string)oStatus;
                }

            }
            catch
            {
                Debug.WriteLine("ERROR: Unable to cast GameStatus");
            }

            try
            {
                object oIsPaused = newState["SongIsPaused"];

                if (oIsPaused != null)
                {
                    isPaused = (bool)oIsPaused;
                }
            }
            catch
            {
                Debug.WriteLine("ERROR: Unable to cast SongIsPaused");
            }

            if (isPaused == false && status.Equals("Unknown"))
            {
                return OsuStatus.Unknown;
            }

            // TODO: Only return paused if the map time is greater than some minimum time, to prevent paused/playing flickering. Need to find the time of the first beatmap object, then add on some minimum time (e.g. 2 seconds)
            if (status.Equals("Playing") && isPaused == true)
            {
                return OsuStatus.SongPaused;
            }

            if (status.Length < 1)
            {
                return OsuStatus.Unknown;
            }

            if (!Enum.TryParse(status, true, out OsuStatus osuStatus))
            {
                return OsuStatus.Unknown;
            }

            return osuStatus;
        }
    }
}
