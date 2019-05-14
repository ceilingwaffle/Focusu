using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Text;

namespace Focusu.GUI.StreamAlerts
{
    public class StreamLabs : IFocusBehaviour
    {
        private readonly string apiKey;

        public StreamLabs(string apiKey)
        {
            this.apiKey = apiKey;

            this.Authenticate(apiKey);
        }

        private bool Authenticate(string apiKey)
        {
            return false;
        }

        public bool Focus(EventArgs focusEventArgs)
        {
            //throw new NotImplementedException();
            return true;
        }

        public bool Unfocus(EventArgs unfocusEventArgs)
        {
            //throw new NotImplementedException();
            return true;
        }
    }
}
