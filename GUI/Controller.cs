namespace GUI
{
    using System;
    using System.Windows;

    using OsuStatePresenter;

    /// <inheritdoc />
    /// <summary>
    /// The controller.
    /// </summary>
    public class Controller : DependencyObject
    {
        protected OsuPresenter osuStatePresenter;

        public Controller()
        {
            osuStatePresenter = new OsuPresenter();
            osuStatePresenter.Start();
        }

        public void HandleFadeTimingChanged(double ms)
        {
            //Options.FadeTiming = TimeSpan.FromMilliseconds(ms);
        }
    }
}
