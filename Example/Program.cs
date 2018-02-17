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
        static string apikey = "";

        static void Main(string[] args)
        {
            //The GeocodeClient can convert from an address to GeoCoordinates
            GeocodeClient client = new GeocodeClient(apikey);

            Console.WriteLine("Enter an Address");
            string address = Console.ReadLine();

            //Object that holds our Coordinates
            MapLocation coord = client.GetCoordinates(address);

            Console.WriteLine("Latitude: " + coord.latitude);
            Console.WriteLine("Longitude: " + coord.longitude);
            Console.ReadKey();
        }
    }
}
