namespace TravelRefunds.Services
{
    using System.Threading.Tasks;

    using Refit;

    using TravelRefunds.Models;

    internal interface ITravelApi
    {
        [Get("/REST/V1/Routes/Driving?wp.0={from}&wp.1={to}&avoid=minimizeTolls&key={bingMapsKey}")]
        Task<Root> GetTravelDistanceAsync(string from, string to, string bingMapsKey);
    }
}
