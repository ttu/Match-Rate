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

namespace MatchRateAppliation
{
    public class SelectCommand : ICommand
    {
        IRepository _repo;
        EventViewModel _event;

        public SelectCommand(IRepository repo, EventViewModel eventView)
        {
            _repo = repo;
            _event = eventView;
        }

        public bool CanExecute(object parameter)
        {
            return _repo != null && _event.ID > 0;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (this.CanExecute(null))
            {
                _repo.LoadEvent(_event.ID);
            }
        }
    }
}
