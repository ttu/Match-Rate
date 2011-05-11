using System.Windows;
using System.Windows.Controls;

namespace MatchRateAppliation
{
    public partial class EventControl : UserControl
    {
        public static readonly DependencyProperty EventSourceProperty =
            DependencyProperty.Register("EventSource",
                                        typeof(EventViewModel),
                                        typeof(EventControl),
                                        new PropertyMetadata(null));
        
        public EventViewModel EventSource
        {
            get { return (EventViewModel)GetValue(EventSourceProperty); }
            set { SetValue(EventSourceProperty, value); }
        }


        public EventControl()
        {
            InitializeComponent();
        }
    }
}
