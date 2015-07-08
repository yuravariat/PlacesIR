using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR.Aylien
{
    // Extract article from website or html
    // Response IReturn<ExtractResponse>
    // Route /extract
    [Route("/extract")]
    public class ReqExtract : BaseReq, IReturn<ExtractResponse>
    {
        public string url { get; set; }
        public string html { get; set; }
        public bool best_image { get; set; }
    }
}