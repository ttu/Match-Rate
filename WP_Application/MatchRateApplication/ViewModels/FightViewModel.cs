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
    public class FightViewModel
    {
        public int ID { get; set; }
        public int IpVote { get; set; }
        public FighterViewModel Fighter1 { get; set; }
        public FighterViewModel Fighter2 { get; set; }
        public int Up { get; set; }
        public int Down { get; set; }

        public string Title
        { 
            get { return Fighter1.Name + " - " + Fighter2.Name; } 
        }
    }
}
