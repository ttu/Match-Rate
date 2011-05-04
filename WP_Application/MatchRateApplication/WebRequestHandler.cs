using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Threading;
using System.ComponentModel;

namespace MatchRateAppliation
{
    public class WebRequestHandler : IWebHandler
    {
        public event JsonData EventsJsonReady;
        public event JsonData SelectedJsonReady;

        WebResponse response;
        HttpWebRequest request;

        public void GetEventsJSON()
        {
            request = (HttpWebRequest)WebRequest.Create("http://tomi.viuhka.fi/MatchRate/getevents.php");
            request.Method = "GET";

            BackgroundWorker bgw = new BackgroundWorker();
            bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_GetEvents_RunWorkerCompleted);
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);

            bgw.RunWorkerAsync();
        }

        public void GetEventJSON(int id)
        {
            request = (HttpWebRequest)WebRequest.Create("http://tomi.viuhka.fi/MatchRate/getevent.php?Id=" + id);
            request.Method = "GET";

            BackgroundWorker bgw = new BackgroundWorker();
            bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_GetEvent_RunWorkerCompleted);
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);

            bgw.RunWorkerAsync();
        }

        public void SendFightVote(int id, bool up)
        {
            // Do nada
        }

        private void getResponse(IAsyncResult result)
        {
            response = ((HttpWebRequest)result.AsyncState).EndGetResponse(result);
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            IAsyncResult result = request.BeginGetResponse(new AsyncCallback(getResponse), request);

            while (!result.IsCompleted)
                System.Threading.Thread.Sleep(1000);
        }

        void bgw_GetEvents_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StreamReader input = new StreamReader(response.GetResponseStream());
            string json = input.ReadToEnd();

            if (EventsJsonReady != null)
                EventsJsonReady(json);
        }

        void bgw_GetEvent_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StreamReader input = new StreamReader(response.GetResponseStream());
            string json = input.ReadToEnd();

            if (SelectedJsonReady  != null)
                SelectedJsonReady(json);
        }
    }
}
