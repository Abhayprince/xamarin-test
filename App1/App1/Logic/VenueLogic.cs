using App1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App1.Logic
{
    public class VenueLogic
    {
        public static async Task<List<Venue>> GetVenuesAsync(double latitude, double longitude)
        {
            var venues = new List<Venue>();
            var url = VenueRoot.GenerateURL(latitude, longitude);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(json);
                venues = venueRoot.response.venues as List<Venue>;
            }
            return venues;
        }
    }
}
