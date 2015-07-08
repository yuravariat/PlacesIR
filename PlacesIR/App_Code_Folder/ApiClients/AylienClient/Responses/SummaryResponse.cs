using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR.Aylien
{
    public class SummaryResponse
    {
        /// <summary>
        /// Gets or sets the extracted key sentences.
        /// </summary>
        /// <value>
        /// The sentences.
        /// </value>
        public List<string> sentences { get; set; }
        /// <summary>
        /// Gets or sets the analyzed text for reference.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string text { get; set; }
    }
}