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
    public class VoteCommand : ICommand
    {
        IRepository _repo;
        FightViewModel _fight;
        bool _up;

        public VoteCommand(IRepository repo, FightViewModel fightView, bool up)
        {
            _repo = repo;
            _fight = fightView;
            _up = up;
        }

        public bool CanExecute(object parameter)
        {
            return _repo != null && _fight.ID > 0;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (this.CanExecute(null))
            {
                _repo.Vote(_fight.ID, _up);
            }
        }
    }
}
