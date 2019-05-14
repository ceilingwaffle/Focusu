using System;
using System.Collections.Generic;
using System.Text;

using Config.Net;

namespace Focusu.GUI
{
    using Focusu.GUI.Options;

    public interface IAppSettings
    {
        [Option(DefaultValue = ControlMethod.Automatic)]
        ControlMethod ControlMethod { get; set; }

        [Option(DefaultValue = ManualControlType.AlwaysShow)]
        ManualControlType ManualControlType { get; set; }

        [Option(DefaultValue = true)]
        bool UnblankForMapBreak { get; set; }

        [Option(DefaultValue = true)]
        bool UnblankForSongPaused { get; set; }

        [Option(DefaultValue = true)]
        bool UnblankForMapStart { get; set; }

        [Option(DefaultValue = false)]
        bool StreamlabsEnabled { get; set; }

        [Option(DefaultValue = "")]
        string StreamlabsApiKey { get; set; }

    }
}
