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
    public class InfoViewModel: INotifyPropertyChanged
    {
        private string _infoLine;

        public string InfoLine
        {
            get
            {
                return _infoLine;
            }
            set
            {
                if (value != _infoLine)
                {
                    _infoLine = value;
                    NotifyPropertyChanged("InfoLine");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
