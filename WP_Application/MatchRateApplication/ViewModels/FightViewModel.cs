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

namespace MatchRateAppliation
{
    public class FightViewModel : ViewModelBase
    {      
        private int _ipvote;
        private ICommand _voteUpCommand;
        private ICommand _voteDownCommand;

        public FightViewModel(IRepository repo)
            : base(repo)
        { }

        public int ID { get; set; }
        public FighterViewModel Fighter1 { get; set; }
        public FighterViewModel Fighter2 { get; set; }
        public int Up { get; set; }
        public int Down { get; set; }

        public int IpVote 
        {
            get { return _ipvote; }
            set 
            {
                _ipvote = value;
                NotifyPropertyChanged("IpVote");
                NotifyPropertyChanged("ImageUpSource");
                NotifyPropertyChanged("ImageDownSource");
            } 
        }

        public string Title
        {
            get { return Fighter1.Name + " - " + Fighter2.Name; }
        }

        public string ImageUpSource
        {
            get
            {
                if (IpVote != 1)
                    return "ImageLike";
                else return "ImageLike_Selected";
            }
        }

        public string ImageDownSource
        {
            get
            {
                if (IpVote != 0)
                    return "ImageDislike";
                else return "ImageDislike_Selected";
            }
        }

        public ICommand VoteUpCommand
        {
            get
            {
                if (_voteUpCommand == null)
                {
                    _voteUpCommand = new RelayCommand(param => this.Vote(true), param => this.CanExecute());
                }

                return _voteUpCommand;
            }
        }

        public ICommand VoteDownCommand
        {
            get
            {
                if (_voteDownCommand == null)
                {
                    _voteDownCommand = new RelayCommand(param => this.Vote(false), param => this.CanExecute());
                }

                return _voteDownCommand;
            }
        }

        public void Vote(bool up)
        {
            base.repo.Vote(ID, up);
            //IpVote = up ? 1 : 0;
        }

        public bool CanExecute()
        {
            return ID > 0 && IpVote == -1;
        }
    }
}
