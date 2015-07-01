using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR.GooglePlaces
{
    [Serializable]
    public class AddressComponent {
        public List<string> types { get; set; }
        public string long_name { get; set; }
        public string short_name { get; set; }

        public AddressComponent() 
        {
            types = new List<string>();
        }
    }
}