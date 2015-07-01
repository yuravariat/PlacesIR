using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR.GooglePlaces
{
    [Serializable]
    public class Hours
    {
        public Hours() 
        {
            periods = new List<Period>();
        }
        public List<Period> periods { get; set; }
        public bool open_now{ get; set; }
        public bool weekday_text { get; set; }
    }
    [Serializable]
    public class Period
    {
        public Period() { }
        public Day open { get; set; }
        public Day close { get; set; }
    }
    [Serializable]
    public class Day
    {
        public short day{ get; set; }
        public string time { get; set; }
    }
}