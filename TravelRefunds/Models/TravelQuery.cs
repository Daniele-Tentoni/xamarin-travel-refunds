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
        /// Gets or sets the distance of the Travel.
        /// </summary>
        public Distance Distance { get; set; }

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
