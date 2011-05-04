using System;
namespace MatchRateAppliation
{
    interface IRepository
    {
        event EventDataReady EventReady;
        event EventsDataReady EventsReady;

        void LoadEvent(int id);
        void LoadEvents();
    }
}
