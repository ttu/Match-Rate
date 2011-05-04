using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using MatchRateAppliation.Models;

namespace MatchRateAppliation
{
    public delegate void EventsDataReady(Events events);
    public delegate void EventDataReady(Event eventData);

    public partial class MainPage : PhoneApplicationPage
    {
        IRepository _repo;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            _repo = new Repository(new WebRequestHandler());
            _repo.EventsReady += new EventsDataReady(_repo_EventsReady);
            _repo.EventReady += new EventDataReady(_repo_EventReady);

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void _repo_EventsReady(Events events)
        {
            App.ViewModel.LoadEventsData(events.EventList);
        }

        void _repo_EventReady(Event eventData)
        {
            App.ViewModel.LoadSelectedEventData(eventData);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            _repo.LoadEvents();
        }

        private void StackPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Pages.SelectedIndex = 1;

            int id = (int)((StackPanel)sender).Tag;
            _repo.LoadEvent(id);
        }
    }
}