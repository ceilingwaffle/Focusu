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
        protected OsuStatePresenter osuStatePresenter = new OsuStatePresenter();

        public Controller()
        {

        }

        public void HandleFadeTimingChanged(double ms)
        {
            Options.FadeTiming = TimeSpan.FromMilliseconds(ms);
        }
    }
}
