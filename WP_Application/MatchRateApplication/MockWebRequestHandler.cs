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
using System.ComponentModel;

namespace MatchRateAppliation
{
    public class MockWebRequestHandler : IWebHandler 
    {
        public event JsonData EventsJsonReady;
        public event JsonData SelectedJsonReady;

        public void GetEventsJSON()
        {
            BackgroundWorker bgw = new BackgroundWorker();
            bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerGetEventsCompleted);
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);

            bgw.RunWorkerAsync();
        }

        public void GetEventJSON(int id)
        {
            BackgroundWorker bgw = new BackgroundWorker();
            bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerGetEventCompleted);
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);

            bgw.RunWorkerAsync();
        }

        public void SendFightVote(int id, bool up)
        {
            // Do nada
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            // Just wait for a while
            System.Threading.Thread.Sleep(1000);
        }

        void bgw_RunWorkerGetEventsCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string json = "{\"events\": [ \r\n\t\t{\r\n\t\t\t\"id\": 7992,\r\n\t\t\t\"name\": \"Strikeforce: Diaz vs. Daley\",\r\n\t\t\t\"date\": \"2011-04-09\"\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": 7998,\r\n\t\t\t\"name\": \"UFC 128: Rua vs. Jones\",\r\n\t\t\t\"date\": \"2011-03-19\"\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": 321,\r\n\t\t\t\"name\": \"UFC Live: Sanchez vs. Kampmann\",\r\n\t\t\t\"date\": \"2011-03-03\"\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": 4125,\r\n\t\t\t\"name\": \"UFC 127: Penn vs. Fitch\",\r\n\t\t\t\"date\": \"2011-02-27\"\r\n\t\t}\r\n]}";

            if (EventsJsonReady != null)
                EventsJsonReady(json);
        }

        void bgw_RunWorkerGetEventCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string json = "{\"events\": [ \r\n\t\t{\r\n\t\t\t\"id\": 7992,\r\n\t\t\t\"name\": \"Strikeforce: Diaz vs. Daley\",\r\n\t\t\t\"date\": \"2011-04-09\",\r\n\t\t\t\"fights\": [\r\n\t\t\t\t\t{\r\n\t\t\t\t\t\t\"id\": 2234,\r\n\t\t\t\t\t\t\"ipvote\": 1,\r\n\t\t\t\t\t\t\"fighter1\": {\r\n\t\t\t\t\t\t\t\"name\": \"Nick Diaz\",\r\n\t\t\t\t\t\t\t\"url\": \"http://www.google.com\"\r\n\t\t\t\t\t\t},\r\n\t\t\t\t\t\t\"fighter2\" : {\r\n\t\t\t\t\t\t\t\"name\": \"Paul Daley\",\r\n\t\t\t\t\t\t\t\"url\": \"http://www.google.com\"\r\n\t\t\t\t\t\t},\r\n\t\t\t\t\t\t\"up\": 92,\r\n\t\t\t\t\t\t\"down\": 11\r\n\t\t\t\t\t},\r\n\t\t\t\t\t{\r\n\t\t\t\t\t\t\t\"id\": 3241,\r\n\t\t\t\t\t\t\t\"fighter1\": {\r\n\t\t\t\t\t\t\t\t\"name\": \"Gilbert Melendez\",\r\n\t\t\t\t\t\t\t\t\"url\": \"http://www.google.com\"\r\n\t\t\t\t\t\t\t},\r\n\t\t\t\t\t\t\t\"fighter2\" : {\r\n\t\t\t\t\t\t\t\t\"name\": \"Tatsuya Kawajiri\",\r\n\t\t\t\t\t\t\t\t\"url\": \"http://www.google.com\"\r\n\t\t\t\t\t\t\t},\r\n\t\t\t\t\t\t\t\"up\": 67,\r\n\t\t\t\t\t\t\t\"down\": 3\r\n\t\t\t\t\t}\r\n\t\t\t\t]\r\n\t\t}\r\n]}"; ;

            if (SelectedJsonReady != null)
                SelectedJsonReady(json);
        }
    }
}
