using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatchRateAppliation
{
    public delegate void JsonData(string json);

    public interface IWebHandler
    {
        event JsonData EventsJsonReady;
        event JsonData SelectedJsonReady;

        void GetEventsJSON();
        void GetEventJSON(int id);
        void SendFightVote(int id, bool up);
    }
}
