using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR.Aylien
{
    // Language detection
    // Response IReturn<List<HCard>>
    // Route /microformats   
    /// <summary>
    /// Language detection.
    /// </summary>
    [Route("/microformats")]
    public class ReqMicroformats : BaseReq, IReturn<List<HCard>>
    {
        public string url { get; set; }
    }
}