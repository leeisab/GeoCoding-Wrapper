using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geocode;

namespace Google_Maps_Example
{
    class Program
    {
        // set this string to your key from https://developers.google.com/maps/documentation/geocoding/start
        static string apikey=null;

        static void Main(string[] args)
        {
            //The GeocodeClient can convert from an address to GeoCoordinates
            GeocodeClient client = new GeocodeClient(apikey);

            //Setting up our address object
            GeoAddress address = new GeoAddress {
                firstline = "500 SW Jefferson Way",
                secondline = "104 Kerr Admin Building",
                city = "Corvallis",
                region = "OR",
                postalCode = "97128",
                country = "USA"
            };

            //Object that holds our Coordinates
            GeoCoordinates coord = client.GetCoordinates(address);

            Console.WriteLine("Latitude: " + coord.latitude);
            Console.WriteLine("Longitude: " + coord.longitude);

            Console.ReadKey();
        }
    }
}
