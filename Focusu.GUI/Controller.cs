﻿using Focusu.GUI.Screen;
using Focusu.GUI.StreamAlerts;

namespace Focusu.GUI
{
    using System.Windows;

    using OsuStatePresenter;
    using DVPF.Core;
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;

    using Focusu.GUI.Screen;

    /// <inheritdoc />
    /// <summary>
    /// Processes the osu! game state, updates the data bindings, and blanks/unblanks the screens.
    /// </summary>
    public class Controller : DependencyObject
    {
        protected readonly OsuPresenter osuStatePresenter;
        protected readonly Bindings dataBindings;
        protected readonly IFocusBehaviour screenBlanker;
        // protected readonly IFocusBehaviour streamlabs;

        public Controller(Bindings dataBindings)
        {
            this.dataBindings = dataBindings;
            this.screenBlanker = new ScreenBlanker(this.CollectScreens());
            // this.streamlabs = new StreamLabs();

            this.osuStatePresenter = new OsuPresenter(HandleOsuGameStateCreated);
            this.SetupOsuStatePresenter(this.osuStatePresenter);
        }

        private void SetupOsuStatePresenter(OsuPresenter osuPresenter)
        {
            // disable nodes not required for this app
            osuPresenter.TryGetNode(typeof(OsuStatePresenter.Nodes.BpmNode), out Node bpmNode);
            bpmNode.DisablePresentation();

            osuPresenter.TryGetNode(typeof(OsuStatePresenter.Nodes.ModsNode), out Node modsNode);
            modsNode.DisablePresentation();

            osuPresenter.TryGetNode(typeof(OsuStatePresenter.Nodes.PPNode), out Node ppNode);
            ppNode.DisablePresentation();

            // start presenting and scanning for node values
            osuPresenter.Start();
        }

        public void HandleFadeTimingChanged(double ms)
        {
            // TODO
            //Bindings.FadeTiming = TimeSpan.FromMilliseconds(ms);
        }

        public void HandleOsuGameStateCreated(State newState)
        {
            this.ProcessOsuGameState(newState);
        }

        private FocusuScreenCollection CollectScreens()
        {
            var focusuScreenCollection = new FocusuScreenCollection();

            foreach (var screen in System.Windows.Forms.Screen.AllScreens)
            {
                var focusuScreen = new FocusuScreen(screen);

                if (screen.Primary)
                {
                    focusuScreen.IsEnabled = false;
                    focusuScreenCollection.PrimaryScreen = focusuScreen;
                }
                else
                {
                    focusuScreen.IsEnabled = true;
                    focusuScreenCollection.SecondaryScreens.Add(focusuScreen);
                }
            }

            return focusuScreenCollection;
        }

        private void ProcessOsuGameState(State newState)
        {
            this.osuStatePresenter.TryGetNode(typeof(OsuStatePresenter.Nodes.StatusNode), out Node statusNode);

            this.dataBindings.OsuStatus = (OsuStatus)statusNode.GetValue();

            //Debug.WriteLine($"\n{newState.ToString()}");

            // handle automatic controls
            if (IsAutomaticControls())
            {
                if (ShouldAutomaticallyBlankNow(this.dataBindings.OsuStatus))
                {
                    DoBlanking();
                }
                else if (IsBlanked())
                {
                    DoUnblanking();
                }

                return;
            }

            // handle manual controls
            if (IsBlanked() && AlwaysShow())
            {
                DoUnblanking();
            }
            else if (IsNotBlanked() && AlwaysBlank())
            {
                DoBlanking();
            }
        }

        private bool IsAutomaticControls()
        {
            return this.dataBindings.ControlMethod == Options.ControlMethod.Automatic;
        }
        private bool IsBlanked()
        {
            return this.dataBindings.IsBlanked;
        }

        private bool IsNotBlanked()
        {
            return !this.dataBindings.IsBlanked;
        }

        private bool AlwaysBlank()
        {
            return this.dataBindings.ManualControlType == Options.ManualControlType.AlwaysBlank;
        }

        private bool AlwaysShow()
        {
            return this.dataBindings.ManualControlType == Options.ManualControlType.AlwaysShow;
        }

        private void DoUnblanking()
        {
            this.dataBindings.IsBlanked = !this.screenBlanker.Unfocus(unfocusEventArgs: null);
            // this.streamlabs.Unfocus(unfocusEventArgs: null);
        }

        private void DoBlanking()
        {
            this.dataBindings.IsBlanked = this.screenBlanker.Focus(focusEventArgs: null);
            // this.streamlabs.Focus(focusEventArgs: null);
        }

        private bool ShouldAutomaticallyBlankNow(OsuStatus osuStatus)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (osuStatus)
            {
                case OsuStatus.SongPaused when !this.dataBindings.UnblankForSongPaused:
                case OsuStatus.InMapBreak when !this.dataBindings.UnblankForMapBreak:
                case OsuStatus.MapStart when !this.dataBindings.UnblankForMapStart:
                case OsuStatus.Playing:
                    return true;
                default:
                    return false;
            }
        }

        //private OsuStatus GetOsuStatusFromGameState(State newState)
        //{
        //    if (!TryCastOsuStateProperty(newState, "GameStatus", out string status))
        //    {
        //        status = "Unknown";
        //    }

        //    if (!TryCastOsuStateProperty(newState, "SongIsPaused", out bool isPaused))
        //    {
        //        isPaused = false;
        //    }

        //    if (!TryCastOsuStateProperty(newState, "IsMapBreak", out bool isMapBreak))
        //    {
        //        isMapBreak = false;
        //    }

        //    if (!TryCastOsuStateProperty(newState, "IsMapStart", out bool isMapStart))
        //    {
        //        isMapStart = false;
        //    }

        //    if (isPaused == false && status.Equals("Unknown"))
        //    {
        //        return OsuStatus.Unknown;
        //    }

        //    if (isPaused == false && status.Equals("Playing") && isMapBreak == true)
        //    {
        //        return OsuStatus.InMapBreak;
        //    }

        //    if (status.Equals("Playing") && isPaused == true)
        //    {
        //        return OsuStatus.SongPaused;
        //    }

        //    if (status.Equals("Playing") && isMapStart == true)
        //    {
        //        return OsuStatus.MapStart;
        //    }

        //    if (status.Length < 1)
        //    {
        //        return OsuStatus.Unknown;
        //    }

        //    // try to get the enum property name as a string
        //    return !Enum.TryParse(status, ignoreCase: true, out OsuStatus osuStatus) ? OsuStatus.Unknown : osuStatus;
        //}

        //private bool TryCastOsuStateProperty<T>(State state, string statePropName, out T result)
        //{
        //    try
        //    {
        //        object oResult = state[statePropName];

        //        if (oResult != null)
        //        {
        //            result = (T)oResult;
        //            return true;
        //        }

        //    }
        //    catch
        //    {
        //        Debug.WriteLine($"ERROR: Unable to cast {statePropName} to type {typeof(T).ToString()}");
        //    }

        //    result = default;
        //    return false;
        //}

    }
}
