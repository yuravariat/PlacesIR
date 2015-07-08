using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR.Aylien
{
    // Language detection
    // Response IReturn<LangDetectionResponse>
    // Route /language   
    /// <summary>
    /// Language detection.
    /// </summary>
    [Route("/language")]
    public class ReqLangDetect : BaseReq, IReturn<LangDetectionResponse>
    {
        public string url { get; set; }
        public string text { get; set; }
    }
}