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
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Events = new ObservableCollection<EventViewModel>();
            this.Info = new InfoViewModel();
            this.Info.InfoLine = "Here comes some info about software. Maybe stats or info about events?";
        }

        public ObservableCollection<EventViewModel> Events { get; private set; }

        public InfoViewModel Info { get; set; }

        private EventViewModel _selectedEvent;
        public EventViewModel SelectedEvent
        {
            get
            {
                return _selectedEvent;
            }
            set
            {
                _selectedEvent = value;
                NotifyPropertyChanged("SelectedEvent");
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadData()
        { 
            // What to load in here?
        }

        // Actually no need for ViewModels to know Models, this could be dont outside
        public void LoadEventsData(List<Event> eventList)
        {
            foreach (Event e in eventList)
            {
                EventViewModel ev = e.ConvertToViewModel();
                Events.Add(ev);
            }

            //NotifyPropertyChanged("Events");
            IsDataLoaded = true;
        }

        public void LoadSelectedEventData(Event selectedEvent)
        {
            EventViewModel ev = selectedEvent.ConvertToViewModel();
            SelectedEvent = ev;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
