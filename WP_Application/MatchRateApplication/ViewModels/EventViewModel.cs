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
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MatchRateAppliation
{
    public class EventViewModel: INotifyPropertyChanged
    {
        private string _name;

        public int ID { get; set; }
        public string Name 
        {
            get
            {
                return _name; 
            }
            set 
            {
                _name = value;
                NotifyPropertyChanged("Name");
            } 
        }

        public DateTime Date
        {
            get;
            set;
        }

        public string DateShort 
        {
            get {return Date.ToShortDateString();}
        }

        public List<FightViewModel> Fights { get; set; }

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
