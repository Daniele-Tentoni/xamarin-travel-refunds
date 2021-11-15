namespace TravelRefunds.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using TravelRefunds.Models;

    internal class RefundService
    {
        public Task<double> CalculateCost(Distance distance, Price price) => Task.FromResult(distance.GetKmDistance() * price.EurPrice);
    }
}
