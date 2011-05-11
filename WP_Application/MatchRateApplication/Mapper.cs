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
using MatchRateAppliation.Models;

namespace MatchRateAppliation
{
    public static class Mapper
    {
        public static EventViewModel ConvertToViewModel(this Event current, IRepository repo)
        {
            EventViewModel retVal = new EventViewModel(repo) { ID = current.ID, Name = current.Name, Date = current.Date };

            if (current.Fights != null)
            {
                retVal.Fights = new System.Collections.Generic.List<FightViewModel>();

                foreach(Fight f in current.Fights)
                {
                    FightViewModel fight = f.ConvertToViewModel(repo);
                    retVal.Fights.Add(fight);
                }
            }

            return retVal;
        }

        public static FightViewModel ConvertToViewModel(this Fight current, IRepository repo)
        {
            FightViewModel retVal = new FightViewModel(repo) {   ID = current.ID, IpVote = current.IpVote, Up = current.Up, Down = current.Down };
            retVal.Fighter1 = current.Fighter1.ConvertToViewModel(repo);
            retVal.Fighter2 = current.Fighter2.ConvertToViewModel(repo);

            return retVal;
        }

        public static FighterViewModel ConvertToViewModel(this Fighter current, IRepository repo)
        {
            FighterViewModel retVal = new FighterViewModel(repo) { Name = current.Name, Url = current.Url  };

            return retVal;
        }
    }
}
