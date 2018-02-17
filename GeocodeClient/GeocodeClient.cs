using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Geocode
{
    /// <summary>
    /// Contains methods for interacting with the Google Maps GeoCordinates API
    /// </summary>
    public class GeocodeClient
    {
        private string _apiKey;
        private readonly string BASE_URL = "https://maps.googleapis.com/maps/api/geocode/json?";

        public GeocodeClient(string apiKey = null)
        {
            if (apiKey != null)
                _apiKey = apiKey;
            else 
                Console.WriteLine("[Warning] API key Missing. Your requests may be throttled or limited");  
        }

        private GeoObj WebRequest(string arg) => GetRequest(arg).GetAwaiter().GetResult();

        private async Task<GeoObj> GetRequest(string arg)
        {
            arg = Regex.Replace(arg, "\\s", "+");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync($"{BASE_URL}{arg}");
            var geo = new GeoObj();
            JsonConvert.PopulateObject(await response.Content.ReadAsStringAsync(), geo);
            return geo;
        }

        /// <summary>
        /// Converts an address object into a set of coordinates
        /// </summary>
        public MapLocation GetMapLocation(Address address)
        {
            return new MapLocation(
                WebRequest($"address={address.Street}+{address.Apt}+{address.Region}+{address.PostalCode}+{address.Country}"));

        }

        /// <summary>
        /// Converts a string address to a set of Coordinates
        /// </summary>
        public MapLocation GetMapLocation(string address)
        {
            return new MapLocation(
                WebRequest($"address={address}"));
        }

        /// <summary>
        /// Converts Coordinates into an address object
        /// </summary>
        public MapLocation GetMapLocation(float lat,float lng)
        {
            return new MapLocation(
                WebRequest($"latlng={lat},{lng}"));
        }
    }
}
