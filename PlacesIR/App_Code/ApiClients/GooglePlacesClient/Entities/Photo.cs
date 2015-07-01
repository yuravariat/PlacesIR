using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR.GooglePlaces
{
    [Serializable]
    public class Photo
    {
        public Photo() { }
        public string[] html_attributions { get; set; }
        public string photo_reference { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
}