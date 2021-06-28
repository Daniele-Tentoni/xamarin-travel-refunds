﻿namespace TravelRefunds.Models
{
    public class TravelQuery
    {
        public string From { get; set; }
        public string To { get; set; }
        public DistanceUnit DistanceUnit { get; set; }
        public int? Distance { get; set; }
    };

    public enum DistanceUnit
    {
        Kilometer
    }
}
