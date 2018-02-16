using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Geocode
{
    public class GeocodeClient
    {
        private string _apiKey = "";
        private readonly string BASE_URL = "https://maps.googleapis.com/maps/api/geocode/json?";

        public GeocodeClient(string apiKey = null)
        {
            if (apiKey != null) {
                _apiKey = apiKey;
            }
            else {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[Warning] API key Missing. Your requests may be throttled or limited");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }


        private string WebRequest(string arg) => GetRequest(arg).GetAwaiter().GetResult();

        private async Task<string> GetRequest(string arg)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync($"{BASE_URL}{arg}");
            return await response.Content.ReadAsStringAsync();
        }

        public GeoCoordinates GetCoordinates(GeoAddress address)
        {
            string json = WebRequest($"address={address.firstline}+{address.secondline}+{address.region}+{address.postalCode}+{address.country}");
            GeoObj objs = new GeoObj();
            JsonConvert.PopulateObject(json, objs);
            return new GeoCoordinates(objs);
        }
    }
}
