using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR.GooglePlaces
{
    [Serializable]
    public class Geometry
    {
        public Location location { get; set; }
        public ViewPort viewport { get; set; }
    }
    [Serializable]
    public class ViewPort
    {
        public Location northeast { get; set; }
        public Location southwest { get; set; }
    }
    [Serializable]
    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
}