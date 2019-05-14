namespace Focusu.GUI
{
    using GUI.Options;
    using System;
    using System.ComponentModel;

    using Config.Net;

    public class Bindings : INotifyPropertyChanged
    {
        private IAppSettings settings;

        // state (UI cannot modify these)
        private OsuStatus osuStatus = OsuStatus.NotRunning;
        private bool isBlanked = false;

        // controls/settings
        private TimeSpan fadeTiming = TimeSpan.FromMilliseconds(0);
        private MonitorStatus monitorStatus = MonitorStatus.Unblanked;
        private ControlMethod controlMethod;
        private ManualControlType manualControlType;
        private bool unblankForMapBreak;
        private bool unblankForSongPaused;
        private bool unblankForMapStart;
        private bool streamlabsEnabled;
        private string streamlabsApiKey;

        public Bindings()
        {
            this.settings = new ConfigurationBuilder<IAppSettings>()
                .UseIniFile("Focusu.config.ini")
                .Build();

            this.controlMethod = this.settings.ControlMethod;
            this.manualControlType = this.settings.ManualControlType;
            this.unblankForMapBreak = this.settings.UnblankForMapBreak;
            this.unblankForSongPaused = this.settings.UnblankForSongPaused;
            this.unblankForMapStart = this.settings.UnblankForMapStart;
            this.streamlabsEnabled = this.settings.StreamlabsEnabled;
            this.streamlabsApiKey = this.settings.StreamlabsApiKey;
        }

        public TimeSpan FadeTiming
        {
            get
            {
                return fadeTiming;
            }
            set
            {
                fadeTiming = value;
                OnPropertyChanged("FadeTiming");
            }
        }

        public OsuStatus OsuStatus
        {
            get
            {
                return osuStatus;
            }
            set
            {
                osuStatus = value;
                OnPropertyChanged("OsuStatus");
            }
        }

        public MonitorStatus MonitorStatus
        {
            get
            {
                return monitorStatus;
            }
            set
            {
                monitorStatus = value;
                OnPropertyChanged("MonitorStatus");
            }
        }

        public ControlMethod ControlMethod
        {
            get
            {
                return this.controlMethod;
            }
            set
            {
                this.controlMethod = value;
                this.settings.ControlMethod = value;
                this.OnPropertyChanged("ControlMethod");
            }
        }

        public ManualControlType ManualControlType
        {
            get
            {
                return this.manualControlType;
            }
            set
            {
                this.manualControlType = value;
                this.settings.ManualControlType = value;
                this.OnPropertyChanged("ManualControlType");
            }
        }

        public bool UnblankForMapBreak
        {
            get
            {
                return this.unblankForMapBreak;
            }
            set
            {
                this.unblankForMapBreak = value;
                this.settings.UnblankForMapBreak = value;
                this.OnPropertyChanged("UnblankForMapBreak");
            }
        }

        public bool UnblankForSongPaused
        {
            get
            {
                return this.unblankForSongPaused;
            }
            set
            {
                this.unblankForSongPaused = value;
                this.settings.UnblankForSongPaused = value;
                this.OnPropertyChanged("UnblankForSongPaused");
            }
        }

        public bool IsBlanked
        {
            get
            {
                return this.isBlanked;
            }
            set
            {
                this.isBlanked = value;
                this.OnPropertyChanged("IsBlanked");
            }
        }

        public bool UnblankForMapStart
        {
            get
            {
                return this.unblankForMapStart;
            }
            set
            {
                this.unblankForMapStart = value;
                this.settings.UnblankForMapStart = value;
                this.OnPropertyChanged("UnblankForMapStart");
            }
        }

        public bool StreamlabsEnabled
        {
            get
            {
                return this.streamlabsEnabled;
            }
            set
            {
                this.streamlabsEnabled = value;
                this.settings.StreamlabsEnabled = value;
                this.OnPropertyChanged("StreamlabsEnabled");
            }
        }

        public string StreamlabsApiKey
        {
            get
            {
                return this.streamlabsApiKey;
            }
            set
            {
                this.streamlabsApiKey = value;
                this.settings.StreamlabsApiKey = value;
                this.OnPropertyChanged("StreamlabsApiKey");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
