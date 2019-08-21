using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;

namespace Focusu.GUI.StreamAlerts
{
    public class StreamLabs : IFocusBehaviour
    {
        private readonly HttpClient httpClient;
        private bool authenticated = false;

        public StreamLabs()
        {
            this.httpClient = new HttpClient();

            // open user's browser with StreamLabs auth page

            // Start HTTP server
            // Wait for user to grant Focusu StreamLabs access (sending request with auth details)
            StartStreamlabsAuthReplyHttpListener();

            Debug.WriteLine("Done?");

            //this.authenticated = this.Authenticate(apiKey).Result;
        }

        //private async Task<bool> Authenticate(string apiKey)
        //{
        //    this.authenticated = false;

        //    HttpResponseMessage response = await this.httpClient.GetAsync(
        //        "https://streamlabs.com/api/v1.0/authorize?");
        //}

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

        internal static void StartStreamlabsAuthReplyHttpListener()
        {
            var prefixes = new List<string>()
            {
                "http://localhost:60403/auth/streamlabs/"
            };

            if (!HttpListener.IsSupported)
            {
                throw new Exception("ERROR: HttpListener not supported.");
            }

            if (prefixes == null || prefixes.Count == 0)
            {
                throw new Exception("prefixes must not be empty in HttpListener");
            }

            var listener = new HttpListener();
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
            listener.Start();
            Debug.WriteLine("HTTP server listening...");

            //IAsyncResult result = listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
            //// Applications can do some work here while waiting for the 
            //// request. If no work can be done until you have processed a request,
            //// use a wait handle to prevent this thread from terminating
            //// while the asynchronous operation completes.
            //Debug.WriteLine("Waiting for request to be processed asyncronously.");
            //result.AsyncWaitHandle.WaitOne();
            //Debug.WriteLine("Request processed asyncronously.");

            var goodAuthResponseReceived = false;
            while (!goodAuthResponseReceived)
            {
                Debug.WriteLine("Waiting for HTTP request...");
                // Note: The GetContext method blocks while waiting for a request. 
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                if (request.QueryString.Count == 0)
                {
                    Debug.WriteLine("Oops - Request received contained no query strings.");
                    continue;
                }
                using System.IO.Stream body = request.InputStream;
                using var reader = new System.IO.StreamReader(body, request.ContentEncoding);
                string contents = reader.ReadToEnd();
                Debug.WriteLine("Request-contents received: ");
                Debug.WriteLine(contents);

                Debug.WriteLine("Request query strings received:");
                var codes = request.QueryString.GetValues("code");
                if (codes.Length < 1)
                {
                    Debug.WriteLine("Oops - Request did not contain a query string param named 'code'.");
                    continue;
                }

                var code = codes[0];
                Debug.WriteLine($"code: ${code}");

                goodAuthResponseReceived = true;
            }

            // TODO: Handle "code" by redirecting HttpListener user browser to StreamLabs auth page
            // If the user approved access, take the code parameter and use the /token endpoint to receive an access_token and refresh_token:
            // https://dev.streamlabs.com/reference#token-1

            listener.Close();
        }

        //private static void ListenerCallback(IAsyncResult result)
        //{
        //    HttpListener listener = (HttpListener)result.AsyncState;

        //    Debug.WriteLine("Waiting for HTTP request...");
        //    // Note: The GetContext method blocks while waiting for a request. 
        //    HttpListenerContext context = listener.GetContext();
        //    HttpListenerRequest request = context.Request;
        //    if (!request.HasEntityBody)
        //    {
        //        Debug.WriteLine("Oops - Request has no body data.");
        //        return;
        //    }
        //    using System.IO.Stream body = request.InputStream;
        //    using var reader = new System.IO.StreamReader(body, request.ContentEncoding);
        //    string contents = reader.ReadToEnd();
        //    Debug.WriteLine("Request-contents received: ");
        //    Debug.WriteLine(contents);
        //}
    }
}
