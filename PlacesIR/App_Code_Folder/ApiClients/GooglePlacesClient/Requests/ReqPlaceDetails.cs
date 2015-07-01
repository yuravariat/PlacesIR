using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR.GooglePlaces
{
    //Get Place details
    //Response IReturn<GoogleApiResponse<List<Place>>>
    //Route place/details/json
    [Route("place/details/json")]
    public class ReqPlaceDetails : GoogleApiRequest, IReturn<GooglePlacesApiResponseSinlgeResult<Place>>
    {
        /// <summary>
        /// Required. A textual identifier that uniquely identifies a place, returned from a Place Search. For more information about place IDs
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string placeid { get; set; }
        /// <summary>
        /// (optional) — Indicates if the Place Details response should include additional fields. Additional fields may include premium data, requiring an additional license, or values that are not commonly requested. Extensions are currently experimental. Supported values for the extensions parameter are : review_summary includes a rich and concise review curated by Google's editorial staff.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string extensions { get; set; }
        /// <summary>
        /// (optional) — The language code, indicating in which language the results should be returned, if possible. Note that some fields may not be available in the requested language. See the list of supported languages and their codes. Note that we often update supported languages so this list may not be exhaustive.
        /// </summary>
        /// <value>
        /// The radius.
        /// </value>
        public string language { get; set; }
    }
}