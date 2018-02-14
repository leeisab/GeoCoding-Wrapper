using System;
using System.Collections.Generic;
using System.Text;

namespace Geocode
{
    public class GeoCoordinates
    {
        public float latitude { get; set; }
        public float longitude { get; set; }

        public GeoCoordinates(float lat, float lng)
        {
            latitude = lat;
            longitude = lng;
        }

        internal GeoCoordinates(GeoObj objs)
        {
            latitude = objs.results[0].geometry.location.lat;
            longitude = objs.results[0].geometry.location.lng;
        }

        public bool isValid() { return (latitude != 0 || longitude != 0); }
    }

   

}
