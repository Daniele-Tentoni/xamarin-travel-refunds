namespace TravelRefunds.Models
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Define a price with Quantity and Unit.
    /// </summary>
    /// <example>
    /// You can express a 15,5€ price as:
    /// <code>
    /// var price = new Price() {
    ///     Quantity = 15.5,
    ///     Unit = "EUR",
    /// };
    /// </code>
    /// </example>
    internal class Price
    {
        /// <summary>
        /// Gets or sets the quantity value.
        /// </summary>
        [DataMember(Name = "quantity")]
        internal double Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit value.
        /// </summary>
        [DataMember(Name = "unit")]
        internal string Unit { get; set; }

        internal double EurPrice
        {
            get
            {
                if (Unit == "EUR") { return Quantity; }
                throw new NotImplementedException();
            }
        }
    }

    /// <summary>
    /// Define a price for a specific fuel.
    /// </summary>
    /// <example>
    /// You can express a Diesel 1,5€ price per l as:
    /// <code>
    /// var fuelPrice = new FuelPrice() {
    ///     Fuel = "Diesel",
    ///     Price = new Price() {
    ///         Quantity = 1.5,
    ///         Unit = "EUR",
    ///     },
    /// };
    /// </code>
    /// </example>
    internal class FuelPrice
    {
        /// <summary>
        /// Gets or sets the fuel value.
        /// </summary>
        [DataMember(Name = "fuel")]
        internal string Fuel { get; set; }

        /// <summary>
        /// Gets or sets the price value.
        /// </summary>
        [DataMember(Name = "price")]
        internal Price Price { get; set; }
    }
}
