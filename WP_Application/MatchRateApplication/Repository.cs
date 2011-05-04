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
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MatchRateAppliation.Models;
using System.ComponentModel;

namespace MatchRateAppliation
{
    public class Repository : MatchRateAppliation.IRepository
    {
        IWebHandler _webHandler;    

        public event EventsDataReady EventsReady;
        public event EventDataReady EventReady;

        public Repository(IWebHandler webHandler)
        {
            _webHandler = webHandler;
            _webHandler.EventsJsonReady += new JsonData(webHandler_EventsJsonReady);
            _webHandler.SelectedJsonReady += new JsonData(webHandler_SelectedJsonReady);
        }

        void webHandler_EventsJsonReady(string json)
        {
            byte[] byteArray = Encoding.Unicode.GetBytes(json);

            MemoryStream ms = new MemoryStream(byteArray);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Events));

            ms.Position = 0;
            Events requestedEvents = (Events)ser.ReadObject(ms);

            if (EventsReady != null)
                EventsReady(requestedEvents);
        }

        void webHandler_SelectedJsonReady(string json)
        {
            byte[] byteArray = Encoding.Unicode.GetBytes(json);

            MemoryStream ms = new MemoryStream(byteArray);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Events));

            Events requestedEvents = (Events)ser.ReadObject(ms);

            if (EventReady != null)
                EventReady(requestedEvents.EventList[0]);
        }

        public void LoadEvents()
        {
            _webHandler.GetEventsJSON();
        }

        public void LoadEvent(int id)
        {
            _webHandler.GetEventJSON(id);
        }
    }
}
