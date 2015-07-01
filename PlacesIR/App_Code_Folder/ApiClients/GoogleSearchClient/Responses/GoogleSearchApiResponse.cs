using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PlacesIR.GoogleSearch
{
    [Serializable]
    public class GoogleSearchApiResponse
    {
        public GoogleSearchApiResponse() { }

        [DataMember(Name = "context")]
        public Context Context { get; set; }
        //
        // Summary:
        //     The ETag of the item.
        public string ETag { get; set; }
        [DataMember(Name = "items")]
        public IList<Result> Items { get; set; }
        [DataMember(Name = "kind")]
        public string Kind { get; set; }
        [DataMember(Name = "promotions")]
        public IList<Promotion> Promotions { get; set; }
        [DataMember(Name = "queries")]
        public IDictionary<string, IList<Query>> Queries { get; set; }
        [DataMember(Name = "searchInformation")]
        public SearchInformationData SearchInformation { get; set; }
        [DataMember(Name = "spelling")]
        public SpellingData Spelling { get; set; }
        [DataMember(Name = "url")]
        public UrlData Url { get; set; }
    }
}