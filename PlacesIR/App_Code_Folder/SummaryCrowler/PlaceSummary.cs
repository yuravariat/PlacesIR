using PlacesIR.GooglePlaces;
using PlacesIR.YouTube;
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
        public List<Image> Images { get; set; }
        public string MainSummaryText { get; set; }
        public List<VideoItem> Videos { get; set; }
        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(PlaceIDToSummarize) && Place != null;
            }
        }
        public PlaceSummary() 
        {
            Images = new List<Image>();
        }
        public PlaceSummary(string placeIDToSummarize) : this()
        {
            PlaceIDToSummarize = placeIDToSummarize;
        }
    }
}