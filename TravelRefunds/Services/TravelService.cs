using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MonkeyCache.SQLite;
using Refit;
using TravelRefunds.Models;
using Xamarin.Essentials;

namespace TravelRefunds.Services
{
    interface ITravelApi
    {
        [Get("/REST/V1/Routes/Driving?wp.0={from}&wp.1={to}&avoid=minimizeTolls&key={bingMapsKey}")]
        Task<Root> GetTravelDistanceAsync(string from, string to, string bingMapsKey);
    }

    public class TravelService
    {
        private const string API_KEY = "";

        public async Task<IEnumerable<TravelQuery>> GetTravelHistoryAsync()
        {
            return await Task.Run(() =>
            {
                var keys = Barrel.Current.GetKeys(MonkeyCache.CacheState.Active);
                var queries = keys.Select(s => Barrel.Current.Get<TravelQuery>(s));
                var ordered = queries.OrderByDescending(o => o.RequestTime);
                return queries;
            });
        }

        public Task<TravelQuery> AddToTravelHistoryAsync(string start, string finish, DistanceUnit? distanceUnit, double? distance)
        {
            var key = ComputeKey(start, finish);
            var unit = distanceUnit ?? DistanceUnit.Kilometer;
            try
            {
                return Task.Run(() =>
                {
                    var query = new TravelQuery { From = start, To = finish, DistanceUnit = unit, Distance = distance, RequestTime = DateTime.Now };
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
                var dis = res?.travelDistance;
                var query = await AddToTravelHistoryAsync(start, finish, unit, dis);
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
}
