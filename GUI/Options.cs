namespace GUI
{
    using System;
    using System.ComponentModel;

    public class Options : INotifyPropertyChanged
    {
        private TimeSpan fadeTiming = TimeSpan.FromMilliseconds(0);
        private OsuStatus osuStatus = OsuStatus.NotRunning;
        private MonitorStatus monitorStatus = MonitorStatus.Unblanked;
        private bool manualControl = true;

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

        public bool ManualControl
        {
            get
            {
                return manualControl;
            }
            set
            {
                manualControl = value;
                OnPropertyChanged("ManualControl");
            }
        }


        public Options()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
