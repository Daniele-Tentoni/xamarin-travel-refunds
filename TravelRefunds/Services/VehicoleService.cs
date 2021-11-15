namespace TravelRefunds.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using MonkeyCache.SQLite;

    public class Vehicole
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }

    public class VehicoleService
    {
        public Task<IEnumerable<Vehicole>> GetVehicoles()
        {
            return Task.Run(() =>
            {
                var keys = Barrel.Current.GetKeys();
                var queries = keys.Select(key => Barrel.Current.Get<Vehicole>(key));
                return queries;
            });
        }

        public Task<Vehicole> AddVehicole(string name)
        {
            var vehicole = new Vehicole()
            {
                Name = name
            };
            var key = vehicole.Name;
            try
            {
                return Task.Run(() =>
                {
                    Barrel.Current.Add(key.ToString(), vehicole, TimeSpan.FromDays(10));
                    return vehicole;
                });
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Fail to add vehicole to local Monkey Barrel Cache: ", e.Message);
                throw e;
            }
        }
    }
}
