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
    public class EventViewModel : ViewModelBase
    {
        private string _name;
        private ICommand _selectCommand;

        public EventViewModel(IRepository repo)
            : base(repo)
        { }

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

        public ICommand SelectEventCommand
        {
            get
            {
                if (_selectCommand == null)
                {
                    _selectCommand = new RelayCommand(param => this.LoadEvent(), param => this.CanExecute());
                }

                return _selectCommand;
            }
        }

        public void LoadEvent()
        {     
            base.repo.LoadEvent(this.ID);
        }

        public bool CanExecute()
        {
            return ID > 0;
        }

      
    }
}
