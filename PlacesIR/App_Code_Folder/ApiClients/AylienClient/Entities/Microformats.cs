using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR.Aylien
{
    public class HCard
    {
        public string id { get; set; }
        public string fullName { get; set; }
        public HName structuredName { get; set; }
        public string nickName { get; set; }
        public string email { get; set; }
        public string photo { get; set; }
        public string url { get; set; }
        public string telephoneNumber { get; set; }
        public string birthday { get; set; }
        public string category { get; set; }
        public string note { get; set; }
        public string logo { get; set; }
        public HLocation location { get; set; }
        public HAddress address { get; set; }
        public string organization { get; set; }
    }
    public class HName
    {
        public string id { get; set; }
        public string honorificPrefix { get; set; }
        public string givenName { get; set; }
        public string additionalName { get; set; }
        public string familyName { get; set; }
        public string honorificSuffix { get; set; }
    }
    public class HLocation
    {
        public string id { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
    public class HAddress
    {
        public string id { get; set; }
        public string streetAddress { get; set; }
        public string locality { get; set; }
        public string region { get; set; }
        public string countryName { get; set; }
        public string postalCode { get; set; }
    }
}