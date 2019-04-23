namespace GUI
{
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
            //Bindings.FadeTiming = TimeSpan.FromMilliseconds(ms);
        }
    }
}
