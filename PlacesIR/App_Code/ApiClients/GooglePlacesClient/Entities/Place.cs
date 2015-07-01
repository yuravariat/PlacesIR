using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR.GooglePlaces
{
    [Serializable]
    public class Place
    {
        public Place()
        {
            types = new List<string>();
            photos = new List<Photo>();
            reviews = new List<Review>();
            address_components = new List<AddressComponent>();
            alt_ids = new List<AltId>();
        }
        public List<string> types { get; set; }
        public string id { get; set; }
        public string place_id { get; set; }
        public string reference { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }

        public string scope { get; set; }
        public List<AltId> alt_ids { get; set; }
        public Hours opening_hours { get; set; }
        /// <summary>
        /// Gets or sets the price_level.
        /// 0 — Free
        /// 1 — Inexpensive
        /// 2 — Moderate
        /// 3 — Expensive
        /// 4 — Very Expensive
        /// </summary>
        /// <value>
        /// The price_level.
        /// </value>
        public short price_level { get; set; }
        public List<Photo> photos { get; set; }
        public double rating { get; set; }
        public string vicinity { get; set; }
        public List<AddressComponent> address_components { get; set; }
        public string formatted_phone_number { get; set; }
        public string international_phone_number { get; set; }
        public bool permanently_closed { get; set; }
        public string url { get; set; }
        public string website { get; set; }
        public int utc_offset { get; set; }
        public List<Review> reviews { get; set; }
        
    }
    [Serializable]
    public class AltId {
        public string place_id { get; set; }
        public string scope { get; set; }
    }
}