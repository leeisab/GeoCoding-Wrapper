using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
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
                Console.WriteLine("Press Enter To Continue ...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private string WebRequest(string arg)
        {
            WebRequest request = HttpWebRequest.Create(Regex.Replace($"{BASE_URL}{arg}&key={_apiKey}", "\\s", "+"));
            WebResponse response = request.GetResponse();

            string json;
            using (var stream = new StreamReader(response.GetResponseStream())) {
                json = stream.ReadToEnd();
            }
            return json;
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
