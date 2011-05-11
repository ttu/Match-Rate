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
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            App.ViewModel.LoadData();
        }

        // Commands can't be binded yet so have to use code behind
        private void ExecuteCommand(object sender, ManipulationStartedEventArgs e)
        {
            ICommand command = (ICommand)((StackPanel)sender).Tag;

            if (command != null)
            {
                command.Execute(null);
                Pages.SelectedIndex = 1;
            }
        }
    }
}