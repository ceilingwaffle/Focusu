using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focusu.GUI
{
    using System.ComponentModel;

    /// <summary>
    /// Associates the "monitor blanked" status with a reason for being blanked/unblanked. See: <see cref="Helpers.GetDescription"/>
    /// </summary>
    public enum FocusStatusReason
    {
        // controls
        [Description("Manual controls enabled.")]
        ControlsManual = 1,

        // osu memory status
        [Description("osu! is not running.")]
        OsuNotRunning = 100,

        [Description("The map is paused.")]
        OsuPaused = 101,

        [Description("Playing a map.")]
        OsuPlaying = 102,

        [Description("The map is in a break.")]
        OsuMapBreak = 103,

        // options
        [Description("pp value reached.")]
        PpReached = 1001
    }
}
