using PlacesIR.GooglePlaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR.Summary
{
    [Serializable]
    public class PlaceSummary
    {
        public string PlaceIDToSummarize { get; set; }
        public Place Place { get; set; }
        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(PlaceIDToSummarize) && Place != null;
            }
        }
        public PlaceSummary() { }
        public PlaceSummary(string placeIDToSummarize)
        {
            PlaceIDToSummarize = placeIDToSummarize;
        }
    }
}