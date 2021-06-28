namespace TravelRefunds.Models
{
    using System;

    /// <summary>
    /// Travel query with locations and distance.
    /// </summary>
    public class TravelQuery
    {
        /// <summary>
        /// From location, where user start to travel.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// To location, where user finish to travel.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Distance Unit of the query. This value may change, accordingly to the distance.
        /// </summary>
        public DistanceUnit DistanceUnit { get; set; }

        /// <summary>
        /// Travel distance of the query. This value may change, accordingly to the distance.
        /// </summary>
        public double? Distance { get; set; }

        /// <summary>
        /// The Datetime when user has request the query.
        /// </summary>
        public DateTime RequestTime { get; set; }
    };

    /// <summary>
    /// Distance Units, their number may increase.
    /// </summary>
    public enum DistanceUnit
    {
        Kilometer
    }
}
