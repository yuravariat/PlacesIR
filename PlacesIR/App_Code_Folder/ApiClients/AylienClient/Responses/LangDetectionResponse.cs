using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR.Aylien
{
    public class LangDetectionResponse
    {
        public string lang { get; set; }
        public string text { get; set; }
        public float confidence { get; set; }
    }
}