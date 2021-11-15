namespace TravelRefunds.Models
{
    using System;
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
    public class Distance
    {
        /// <summary>
        /// Gets or sets measure value.
        /// </summary>
        [DataMember(Name = "measure")]
        internal double Measure { get; private set; }

        /// <summary>
        /// Gets or sets unit value.
        /// </summary>
        [DataMember(Name = "unit")]
        internal DistanceUnit DistanceUnit { get; private set; }

        internal Distance(double measure, DistanceUnit unit) => (Measure, DistanceUnit) = (measure, unit);

        /// <summary>
        /// Return the distance measure expressed in Km unit.
        /// </summary>
        /// <returns>Distance in Km.</returns>
        /// <exception cref="NotImplementedException">Other units than km.</exception>
        internal double GetKmDistance()
        {
            if(DistanceUnit == DistanceUnit.Kilometer)
            {
                return Measure;
            }

            throw new NotImplementedException();
        }
    }
}
