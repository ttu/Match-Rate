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

        private string _eventsUrl = "http://tomi.viuhka.fi/MatchRate/getevents.php";
        private string _eventUrl = "http://tomi.viuhka.fi/MatchRate/getevent.php?Id={0}";
        private string _voteUrl = "http://tomi.viuhka.fi/MatchRate/vote.php?Id={0}&Vote={1}";

        public void GetEventsJSON()
        {
            request = (HttpWebRequest)WebRequest.Create(_eventsUrl);
            request.Method = "GET";

            BackgroundWorker bgw = new BackgroundWorker();
            bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_GetEvents_RunWorkerCompleted);
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);

            bgw.RunWorkerAsync();
        }

        public void GetEventJSON(int id)
        {
            request = (HttpWebRequest)WebRequest.Create(string.Format(_eventUrl, id));
            request.Method = "GET";

            BackgroundWorker bgw = new BackgroundWorker();
            bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_GetEvent_RunWorkerCompleted);
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);

            bgw.RunWorkerAsync();
        }

        public void SendFightVote(int id, bool up)
        {
            request = (HttpWebRequest)WebRequest.Create(string.Format(_voteUrl, id, up ? 1 : 0 ));
            request.Method = "GET";

            // For now no reply from vote page
            BackgroundWorker bgw = new BackgroundWorker();
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);

            bgw.RunWorkerAsync();
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
