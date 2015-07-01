using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR.GooglePlaces
{
    public class GooglePlacesApiResponse<T>
    {
        public string[] html_attributions { get; set; }
        public T results { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// OK indicates that no errors occurred; the place was successfully detected and at least one result was returned.
        /// ZERO_RESULTS indicates that the search was successful but returned no results. This may occur if the search was passed a latlng in a remote location.
        /// OVER_QUERY_LIMIT indicates that you are over your quota.
        /// REQUEST_DENIED indicates that your request was denied, generally because of lack of an invalid key parameter.
        /// INVALID_REQUEST generally indicates that a required query parameter (location or radius) is missing.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string status { get; set; }
        /// <summary>
        /// Gets or sets the error_message. When the Google Places service returns a status code other than OK, there may be an additional error_message field within the search response object. This field contains more detailed information about the reasons behind the given status code.
        /// </summary>
        /// <value>
        /// The error_message.
        /// </value>
        public string error_message { get; set; }
        public string next_page_token { get; set; }
    }
    public class GooglePlacesApiResponseSinlgeResult<T> : GooglePlacesApiResponse<T>
    {
        public T result { get; set; }
    }
}