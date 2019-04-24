﻿namespace GUI
{
    using GUI.Options;
    using System;
    using System.ComponentModel;

    public class Bindings : INotifyPropertyChanged
    {
        private OsuStatus osuStatus = OsuStatus.NotRunning;

        private TimeSpan fadeTiming = TimeSpan.FromMilliseconds(0);
        private MonitorStatus monitorStatus = MonitorStatus.Unblanked;
        private ControlMethod controlMethod = ControlMethod.Automatic;
        private ManualControlType manualControlType = ManualControlType.AlwaysShow;
        private bool unblankForMapBreak = false;
        private bool unblankForSongPaused = true;

        public Bindings()
        {

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
                return controlMethod;
            }
            set
            {
                controlMethod = value;
                OnPropertyChanged("ControlMethod");
            }
        }

        public ManualControlType ManualControlType
        {
            get
            {
                return manualControlType;
            }
            set
            {
                manualControlType = value;
                OnPropertyChanged("ManualControlType");
            }
        }

        public bool UnblankForMapBreak
        {
            get
            {
                return unblankForMapBreak;
            }
            set
            {
                unblankForMapBreak = value;
                OnPropertyChanged("UnblankForMapBreak");
            }
        }

        public bool UnblankForSongPaused
        {
            get
            {
                return unblankForSongPaused;
            }
            set
            {
                unblankForSongPaused = value;
                OnPropertyChanged("UnblankForSongPaused");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}