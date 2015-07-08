using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR.Aylien
{
    public class ExtractResponse
    {
        public string title { get; set; }
        public string article { get; set; }
        public string image { get; set; }
        public List<string> videos { get; set; }
        public string author { get; set; }
        public List<string> feeds { get; set; }
    }
}