using System;
namespace MatchRateAppliation
{
    public interface IRepository
    {
        event EventDataReady EventReady;
        event EventsDataReady EventsReady;

        void LoadEvent(int id);
        void LoadEvents();
        void Vote(int fightID, bool up);
    }
}
