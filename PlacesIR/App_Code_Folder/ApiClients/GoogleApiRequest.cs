using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR
{
    [Serializable]
    public class GoogleApiRequest
    {
        /// <summary>
        /// Gets or sets the Google api key. Required !!!
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string key { get; set; } 
    }
    public enum RequestMethods
    {
        OPTIONS,
        GET,
        HEAD,
        POST,
        PUT,
        DELETE,
        TRACE,
        CONNECT
    }
}