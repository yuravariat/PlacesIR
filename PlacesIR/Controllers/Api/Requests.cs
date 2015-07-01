using PlacesIR.GooglePlaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR.Controllers.Api
{
    [Serializable]
    public class RequestNearByPlaces
    {
        public RequestNearByPlaces() { }
        private string place;
        private string keywords;
        private int distance;
        private RankBy rankby;

        public string Place
        {
            get { return place; }
            set { place = value; }
        }
        public string Keywords
        {
            get { return keywords; }
            set { keywords = value; }
        }
        public int Distance
        {
            get { return distance; }
            set { distance = value; }
        }
        public RankBy Rankby
        {
            get { return rankby; }
            set { rankby = value; }
        }
    }
}