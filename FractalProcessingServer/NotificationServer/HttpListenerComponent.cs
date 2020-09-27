using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationServer
{
    public class HttpListenerComponent : Logic
    {
        private HttpListener mListener;
        private readonly string mUrl;

        public HttpListenerComponent(string url)
        {
            mUrl = url;
        }

        protected override void InnerUninitializedToInitialized()
        {
            base.InnerUninitializedToInitialized();
            mListener = CreateHttpListener();
        }

        private HttpListener CreateHttpListener()
        {
            HttpListener httpListener = new HttpListener();
            httpListener.Prefixes.Add(mUrl);
            return httpListener;
        }

        protected override void InnerInitializedToStarted()
        {
            base.InnerInitializedToStarted();

            mListener.Start();

            Task.Run(() => HandleIncomingConnections());
        }

        protected override void InnerStartedToInitialized()
        {
            base.InnerStartedToInitialized();

            mListener.Stop();
            mListener = CreateHttpListener();
        }

        protected override void InnerInitializedToUninitialized()
        {
            base.InnerInitializedToUninitialized();

            mListener = null;
        }
        
        public async Task HandleIncomingConnections()
        {
            int pageViews = 0;
            int requestCount = 0;
            const string pageData =
                "<!DOCTYPE>" +
                "<html>" +
                "  <head>" +
                "    <title>HttpListener Example</title>" +
                "  </head>" +
                "  <body>" +
                "    <p>Page Views: {0}</p>" +
                "    <form method=\"post\" action=\"shutdown\">" +
                "      <input type=\"submit\" value=\"Shutdown\" {1}>" +
                "    </form>" +
                "  </body>" +
                "</html>";


            try
            {
                bool runServer = true;

                // While a user hasn't visited the `shutdown` url, keep on handling requests
                while (runServer)
                {
                    // Will wait here until we hear from a connection
                    HttpListenerContext ctx = await mListener.GetContextAsync();

                    // Peel out the requests and response objects
                    HttpListenerRequest req = ctx.Request;
                    HttpListenerResponse resp = ctx.Response;

                    // Print out some info about the request
                    Console.WriteLine("Request #: {0}", ++requestCount);
                    Console.WriteLine(req.Url.ToString());
                    Console.WriteLine(req.HttpMethod);
                    Console.WriteLine(req.UserHostName);
                    Console.WriteLine(req.UserAgent);
                    Console.WriteLine();

                    // If `shutdown` url requested w/ POST, then shutdown the server after serving the page
                    if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/shutdown"))
                    {
                        Console.WriteLine("Shutdown requested");
                        runServer = false;
                    }

                    // Make sure we don't increment the page views counter if `favicon.ico` is requested
                    if (req.Url.AbsolutePath != "/favicon.ico")
                        pageViews += 1;

                    // Write the response info
                    string disableSubmit = !runServer ? "disabled" : "";
                    byte[] data = Encoding.UTF8.GetBytes(String.Format(pageData, pageViews, disableSubmit));
                    resp.ContentType = "text/html";
                    resp.ContentEncoding = Encoding.UTF8;
                    resp.ContentLength64 = data.LongLength;

                    // Write out to the response stream (asynchronously), then close it
                    await resp.OutputStream.WriteAsync(data, 0, data.Length);
                    resp.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public override IEvent ProcessEvent(IEvent eventToProcess)
        {
            throw new System.NotImplementedException();
        }
    }
}