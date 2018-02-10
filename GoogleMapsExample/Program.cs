using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace Google_Maps_Example
{
    class Program
    {
        // Replace this string with your key from https://developers.google.com/maps/documentation/geocoding/start
        static string apikey = "";

        static void Main(string[] args)
        {
            //User Inputs an Address
            Console.WriteLine("Enter an Address");
            string address = Console.ReadLine();

            //replaces all the spaces with a '+' to match the format google wants
            address = Regex.Replace(address , "\\s" , "+");

            //Creates a new Object that can make web requests
            WebRequest request = WebRequest.Create("https://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&" + apikey);

            //makes request and stores the reponse
            WebResponse response = request.GetResponse();

            //Reads the reponse and stores it in a string called "json"
            string json;
            using(var stream = new StreamReader(response.GetResponseStream())) {
                json = stream.ReadToEnd();
            }

            Console.WriteLine(json);
            Console.ReadKey();
        }
    }
}
