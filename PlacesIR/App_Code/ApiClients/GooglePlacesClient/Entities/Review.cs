using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR.GooglePlaces
{
    [Serializable]
    public class Review
    {
        public Review()
        {
            aspects = new List<Aspect>();
        }
        public List<Aspect> aspects { get; set; }
        public string author_name { get; set; }
        public string author_url { get; set; }
        public string language { get; set; }
        public string text { get; set; }
        public short rating { get; set; }
        public long time { get; set; }
    }
    [Serializable]
    public class Aspect {
        public Aspect() { }
        public int rating { get; set; }
        public string type { get; set; }
    }
}