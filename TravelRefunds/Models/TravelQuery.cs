using System;

namespace TravelRefunds.Models
{
    public class TravelQuery
    {
        public string From { get; set; }
        public string To { get; set; }
        public DistanceUnit DistanceUnit { get; set; }
        public double? Distance { get; set; }
        public DateTime RequestTime { get; set; }
    };

    public enum DistanceUnit
    {
        Kilometer
    }
}
