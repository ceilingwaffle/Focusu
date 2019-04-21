namespace GUI
{
    using System;

    /// <summary>
    /// The options for Focuso! screen-blanking behaviour
    /// </summary>
    internal static class Options
    {
        /// <summary>
        /// Gets or sets the fade timing.
        /// </summary>
        public static TimeSpan FadeTiming { get; set; }

        /// <summary>
        /// Gets or sets the osu! status.
        /// </summary>
        public static OsuStatus OsuStatus { get; set; }

        /// <summary>
        /// Gets or sets the monitor "blanked/unblanked" status.
        /// </summary>
        public static MonitorStatus MonitorStatus { get; set; }
    }
}
