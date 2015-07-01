using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR.GooglePlaces
{
    //Get Currencies
    //Response ApiResult<List<Currency>>
    //Route /api/currencies
    [Route("/place/textsearch/json")]
    public class ReqQueryPlaces : GoogleApiRequest, IReturn<GoogleApiResponse<List<Place>>>
    {
        /// <summary>
        /// Required. The text string on which to search, for example: "restaurant". The Google Places service will return candidate matches based on this string and order the results based on their perceived relevance.
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        public string query { get; set; }
        /// <summary>
        /// Optional. The latitude/longitude around which to retrieve place information. This must be specified as latitude,longitude. If you specify a location parameter, you must also specify a radius parameter.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string location { get; set; }
        /// <summary>
        /// Optional. Defines the distance (in meters) within which to bias place results. The maximum allowed radius is 50 000 meters. Results inside of this region will be ranked higher than results outside of the search circle; however, prominent results from outside of the search radius may be included.
        /// </summary>
        /// <value>
        /// The radius.
        /// </value>
        public int? radius { get; set; }
        /// <summary>
        /// Optional. The language code, indicating in which language the results should be returned, if possible. See the list of supported languages and their codes. Note that we often update supported languages so this list may not be exhaustive.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public string language { get; set; }
        /// <summary>
        /// Optional. Restricts results to only those places within the specified price level. Valid values are in the range from 0 (most affordable) to 4 (most expensive), inclusive. The exact amount indicated by a specific value will vary from region to region.
        /// </summary>
        /// <value>
        /// The minprice.
        /// </value>
        public double? minprice { get; set; }
        /// <summary>
        /// Optional. Restricts results to only those places within the specified price level. Valid values are in the range from 0 (most affordable) to 4 (most expensive), inclusive. The exact amount indicated by a specific value will vary from region to region.
        /// </summary>
        /// <value>
        /// The maxprice.
        /// </value>
        public double? maxprice { get; set; }
        /// <summary>
        /// Optional. Returns only those places that are open for business at the time the query is sent. places that do not specify opening hours in the Google Places database will not be returned if you include this parameter in your query.
        /// </summary>
        /// <value>
        /// The opennow.
        /// </value>
        public bool? opennow { get; set; }
        /// <summary>
        /// Optional. Restricts the results to places matching at least one of the specified types. Types should be separated with a pipe symbol (type1|type2|etc). See the list of supported types.
        /// </summary>
        /// <value>
        /// The types.
        /// </value>
        public List<string> types { get; set; }
    }
}