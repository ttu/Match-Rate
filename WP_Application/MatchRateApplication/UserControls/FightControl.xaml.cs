using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MatchRateAppliation
{
    public partial class FightControl : UserControl
    {
        public static readonly DependencyProperty FightSourceProperty =
           DependencyProperty.Register("FightSource",
                                       typeof(FightViewModel),
                                       typeof(FightControl),
                                       new PropertyMetadata(null));

        public FightViewModel FightSource
        {
            get { return (FightViewModel)GetValue(FightSourceProperty); }
            set { SetValue(FightSourceProperty, value); }
        }

        public FightControl()
        {
            InitializeComponent();
        }
    }
}
