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
using System.Runtime.Serialization;

namespace MatchRateAppliation.Models
{
    [DataContract]
    public class Fight
    {
        [DataMember(Name = "id")]
        public int ID { get; set; }
        [DataMember(Name = "ipvote")]
        public int IpVote { get; set; }
         [DataMember(Name = "fighter1")]
        public Fighter Fighter1 { get; set; }
         [DataMember(Name = "fighter2")]
        public Fighter Fighter2 { get; set; }
         [DataMember(Name = "up")]
        public int Up { get; set; }
         [DataMember(Name = "down")]
        public int Down { get; set; }
    }
}
