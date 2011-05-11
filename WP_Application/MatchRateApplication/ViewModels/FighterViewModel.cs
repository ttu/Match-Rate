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
    public class FighterViewModel : ViewModelBase
    {      
        public string Name { get; set; }
        public string Url { get; set; }

        public FighterViewModel(IRepository repo):base(repo)
        {}
    }
}
