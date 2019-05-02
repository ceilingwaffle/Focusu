namespace Focusu.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Squirrel;

    internal class AppUpdater
    {
        private readonly List<Action<string>> eventHandlers_UpdateAvailable = new List<Action<string>>();

        public AppUpdater()
        {

        }

        public void CheckForUpdates()
        {
            var t = Task.Run(() => this.CheckForUpdatesTask().ConfigureAwait(false));

            t.Wait();

            var r = t.Result;
        }

        private async Task CheckForUpdatesTask()
        {
            //// https://github.com/Squirrel/Squirrel.Windows/blob/master/docs/using/github.md
            //using (var mgr = UpdateManager.GitHubUpdateManager("https://github.com/ceilingwaffle/Focusu"))
            //{
            //    UpdateInfo updateInfo = await mgr.Result.CheckForUpdate();

            //    var rel = updateInfo.ReleasesToApply;

            //    //await mgr.Result.UpdateApp();
            //}
        }

        public void AddEventHandler_UpdateAvailable(Action<string> UpdateAvailableHandler)
        {
            eventHandlers_UpdateAvailable.Add(UpdateAvailableHandler);
        }

        protected void OnUpdateAvailable(string update)
        {
            this.eventHandlers_UpdateAvailable.ForEach(eventHandler => eventHandler(update));
        }
    }
}
