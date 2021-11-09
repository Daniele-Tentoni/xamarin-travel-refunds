namespace TravelRefunds.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Distance expressed as Measure and Unit Value.
    /// </summary>
    /// <example>
    /// You can express 17 Km distance as:
    /// <code>
    /// var distance = new Distance() {
    ///     Measure = 17,
    ///     Unit = "Km",
    /// };
    /// </code>
    /// </example>
    internal class Distance
    {
        /// <summary>
        /// Gets or sets measure value.
        /// </summary>
        [DataMember(Name = "measure")]
        internal double Measure { get; set; }

        /// <summary>
        /// Gets or sets unit value.
        /// </summary>
        [DataMember(Name = "unit")]
        internal string Unit { get; set; }
    }
}
