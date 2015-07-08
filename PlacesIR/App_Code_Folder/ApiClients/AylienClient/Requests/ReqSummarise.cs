using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR.Aylien
{
    // Creates summary from website or html
    // Response IReturn<SummaryResponse>
    // Route /summarize    
    /// <summary>
    /// Note that you must provide either an URL or a title and text combination.
    /// </summary>
    [Route("/summarize")]
    public class ReqSummarise : BaseReq, IReturn<SummaryResponse>
    {
        public string url { get; set; }
        public string text { get; set; }
        public string title { get; set; }
        public int sentences_number { get; set; }
        public int sentences_percentage { get; set; }
    }
}