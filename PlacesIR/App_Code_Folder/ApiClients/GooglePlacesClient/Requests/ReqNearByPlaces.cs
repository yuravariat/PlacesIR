using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR.GooglePlaces
{
    //Get NearByPlaces
    //Response IReturn<GoogleApiResponse<List<Place>>>
    //Route /place/nearbysearch/json
    [Route("/place/nearbysearch/json")]
    public class ReqNearByPlaces : GoogleApiRequest, IReturn<GooglePlacesApiResponse<List<Place>>>
    {
        /// <summary>
        /// Required. The latitude/longitude around which to retrieve place information. This must be specified as latitude,longitude. If you specify a location parameter, you must also specify a radius parameter.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string location { get; set; }
        /// <summary>
        /// Optional. One or more terms to be matched against the names of places, separated with a space character. Results will be restricted to those containing the passed name values. Note that a place may have additional names associated with it, beyond its listed name. The API will try to match the passed name value against all of these names. As a result, places may be returned in the results whose listed names do not match the search term, but whose associated names do.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string name { get; set; }
        /// <summary>
        /// Required. Defines the distance (in meters) within which to bias place results. The maximum allowed radius is 50 000 meters. Results inside of this region will be ranked higher than results outside of the search circle; however, prominent results from outside of the search radius may be included.
        /// </summary>
        /// <value>
        /// The radius.
        /// </value>
        public int? radius { get; set; }
        /// <summary>
        /// Optional. A term to be matched against all content that Google has indexed for this place, including but not limited to name, type, and address, as well as customer reviews and other third-party content.
        /// </summary>
        /// <value>
        /// The keyword.
        /// </value>
        public string keyword { get; set; }
        /// <summary>
        /// Optional. Specifies the order in which results are listed. Possible values are:
        ///prominence (default). This option sorts results based on their importance. Ranking will favor prominent places within the specified area. Prominence can be affected by a place's ranking in Google's index, global popularity, and other factors.
        ///distance. This option sorts results in ascending order by their distance from the specified location. When distance is specified, one or more of keyword, name, or types is required.
        /// </summary>
        /// <value>
        /// The rankby.
        /// </value>
        public string rankby { get; set; }
        /// <summary>
        /// Optional. Returns the next 20 results from a previously run search. Setting a pagetoken parameter will execute a search with the same parameters used previously — all parameters other than pagetoken will be ignored.
        /// </summary>
        /// <value>
        /// The pagetoken.
        /// </value>
        public string pagetoken { get; set; }
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
    public enum RankBy
    {
        none,
        prominence=1,
        distance=2
    }
}