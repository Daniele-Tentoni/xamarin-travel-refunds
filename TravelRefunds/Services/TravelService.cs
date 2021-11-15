namespace TravelRefunds.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using MonkeyCache.SQLite;

    using Refit;

    using TravelRefunds.Models;

    using Xamarin.Essentials;

    public class TravelService
    {
        private const string API_KEY = "Aic4dtL2AdXUjQj6Scb7q6_22ainrPIrvIy1-_Yff70E2_BvDzUI52l0bF9-nNTS";

        /// <summary>
        /// Retrieve the local history of previous travels.
        /// </summary>
        /// <returns>List of travels.</returns>
        public async Task<IEnumerable<TravelQuery>> GetTravelHistoryAsync()
        {
            return await Task.Run(() =>
            {
                var keys = Barrel.Current.GetKeys();
                var queries = keys.Select(s => Barrel.Current.Get<TravelQuery>(s));
                var ordered = queries.OrderByDescending(o => o.RequestTime);
                return queries;
            });
        }

        /// <summary>
        /// Add a travel query to the local history.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <param name="distanceUnit"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public Task<TravelQuery> AddToTravelHistoryAsync(string start, string finish, Distance distance)
        {
            var key = ComputeKey(start, finish);
            try
            {
                return Task.Run(() =>
                {
                    var query = new TravelQuery { From = start, To = finish, Distance = distance, RequestTime = DateTime.Now };
                    Barrel.Current.Add(key, query, expireIn: TimeSpan.FromDays(1));
                    return query;
                });
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Fail add query to travel history. Exception: {e.Message}");
                throw e;
            }
        }

        /// <summary>
        /// Get informations about travel from apis.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <returns></returns>
        public async Task<string> GetTravelAsync(string start, string finish)
        {
            var key = ComputeKey(start, finish);
            // https://docs.microsoft.com/en-us/xamarin/essentials/connectivity?tabs=android#using-connectivity
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                // Connection to internet is available and I can ask to API
                var apiResponse = RestService.For<ITravelApi>("http://dev.virtualearth.net");
                var distance = await apiResponse.GetTravelDistanceAsync(start, finish, API_KEY);
                var set = distance.resourceSets.FirstOrDefault();
                var res = set.resources.FirstOrDefault();
                var unit = (DistanceUnit)Enum.Parse(typeof(DistanceUnit), res.distanceUnit);
                var dis = res?.travelDistance ?? 0;
                var dist = new Distance(dis, unit);
                _ = await AddToTravelHistoryAsync(start, finish, dist);
                return $"{res.travelDistance} {res.distanceUnit}";
            }

            if (!Barrel.Current.IsExpired(key))
            {
                // Connection to internet is not available and I have to ask to monkeys.
                return Barrel.Current.Get<string>(key);
            }

            // Cache is not available and I have to add a new key.
            return "No internet connection available and no cached queries.";
        }

        private string ComputeKey(string from, string to) => $"{from} - {to}";
    }

    public static class TravelExtensions
    {
        public static string TravelTo(this string start, string finish) => $"{start} - {finish}";
    }
}
