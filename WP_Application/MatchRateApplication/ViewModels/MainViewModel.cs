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
using System.Collections.ObjectModel;
using MatchRateAppliation.Models;
using System.Collections.Generic;

namespace MatchRateAppliation
{
    public class MainViewModel : ViewModelBase
    {
        private EventViewModel _selectedEvent;
        private int _selectedEventID;

        public ObservableCollection<EventViewModel> Events { get; private set; }

        public InfoViewModel Info { get; set; }

        public EventViewModel SelectedEvent
        {
            get
            {
                return _selectedEvent;
            }
            private set
            {
                _selectedEvent = value;
                NotifyPropertyChanged("SelectedEvent");
            }
        }

        public int SelectedEventID 
        {
            get 
            { return _selectedEventID; }
            set 
            {
                _selectedEventID = value;
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public MainViewModel(IRepository repo)
            : base(repo)
        {

            base.repo.EventsReady += new EventsDataReady(Repo_EventsReady);
            base.repo.EventReady += new EventDataReady(Repo_EventReady);

            this.Events = new ObservableCollection<EventViewModel>();
            this.Info = new InfoViewModel(base.repo);
            this.Info.InfoLine = "Here comes some info about software. Maybe stats or info about events?";
        }

        public void LoadData()
        {
            base.repo.LoadEvents();
        }

        public void LoadEventsData(List<Event> eventList)
        {
            foreach (Event e in eventList)
            {
                EventViewModel ev = e.ConvertToViewModel(base.repo);
                Events.Add(ev);
            }

            //NotifyPropertyChanged("Events");
            IsDataLoaded = true;
        }

        public void LoadSelectedEventData(Event selectedEvent)
        {
            EventViewModel ev = selectedEvent.ConvertToViewModel(base.repo);
            SelectedEvent = ev;
        }

        void Repo_EventsReady(Events events)
        {
           LoadEventsData(events.EventList);
        }

        void Repo_EventReady(Event eventData)
        {
           LoadSelectedEventData(eventData);
        }
    }
}
