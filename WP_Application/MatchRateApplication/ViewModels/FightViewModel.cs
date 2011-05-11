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
        private VoteCommand _voteUpCommand;
        private VoteCommand _voteDownCommand;

        public int ID { get; set; }

        public int IpVote 
        {
            get { return _ipvote; }
            set 
            {
                _ipvote = value;
                NotifyPropertyChanged("ImageUpSource");
                NotifyPropertyChanged("ImageDownSource");
            } 
        }

        public FighterViewModel Fighter1 { get; set; }
        public FighterViewModel Fighter2 { get; set; }
        public int Up { get; set; }
        public int Down { get; set; }

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

        public VoteCommand VoteUpCommand
        {
            get
            {
                if (_voteUpCommand == null)
                {
                    _voteUpCommand = new VoteCommand(base.repo, this, true);
                }

                return _voteUpCommand;
            }
        }

        public VoteCommand VoteDownCommand
        {
            get
            {
                if (_voteDownCommand == null)
                {
                    _voteDownCommand = new VoteCommand(base.repo, this, false);
                }

                return _voteDownCommand;
            }
        }

        public FightViewModel(IRepository repo)
            : base(repo)
        {}
    }
}
