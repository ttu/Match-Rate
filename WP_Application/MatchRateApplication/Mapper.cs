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
        public static EventViewModel ConvertToViewModel(this Event current)
        {
            EventViewModel retVal = new EventViewModel() { ID = current.ID, Name = current.Name, Date = current.Date };

            if (current.Fights != null)
            {
                retVal.Fights = new System.Collections.Generic.List<FightViewModel>();

                foreach(Fight f in current.Fights)
                {
                    FightViewModel fight = f.ConvertToViewModel();
                    retVal.Fights.Add(fight);
                }
            }

            return retVal;
        }

        public static FightViewModel ConvertToViewModel(this Fight current)
        {
            FightViewModel retVal = new FightViewModel() {   ID = current.ID, IpVote = current.IpVote, Up = current.Up, Down = current.Down };
            retVal.Fighter1 = current.Fighter1.ConvertToViewModel();
            retVal.Fighter2 = current.Fighter2.ConvertToViewModel();

            return retVal;
        }

        public static FighterViewModel ConvertToViewModel(this Fighter current)
        {
            FighterViewModel retVal = new FighterViewModel() { Name = current.Name, Url = current.Url  };

            return retVal;
        }
    }
}
