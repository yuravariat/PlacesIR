﻿using PlacesIR.GooglePlaces;
using PlacesIR.GoogleSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace PlacesIR.Summary
{
    public class PlaceSummaryCrawler
    {
        public static ValidationResponse<PlaceSummary> PrepareSummary(string placeIDToSummarize, string lang = "lang_en")
        {
            ValidationResponse<PlaceSummary> response = new ValidationResponse<PlaceSummary>();

            if (string.IsNullOrEmpty(placeIDToSummarize))
            {
                throw new ArgumentException("placeIDToSummarize");
            }
            PlaceSummary summary;
            string cacheKey = "Summary-" + placeIDToSummarize + "-" + lang;
            if (HttpContext.Current.Cache[cacheKey] != null)
            {
                summary = HttpContext.Current.Cache[cacheKey] as PlaceSummary;
                if (summary != null)
                {
                    response.Obj = summary;
                    return response;
                }
            }
            summary = new PlaceSummary(placeIDToSummarize);

            // step 0 - gt place details from places api.
            using (GooglePlacesClient placesClient = new GooglePlacesClient())
            {
                var placeRes = placesClient.GetPlacesDetails(new ReqPlaceDetails() {
                    placeid = summary.PlaceIDToSummarize
                });
                if (placeRes.Obj != null)
                {
                    summary.Place = placeRes.Obj;
                }
                else
                {
                    foreach(var error in placeRes.Errors){
                        response.Errors.Add(error.Key, error.Value);
                    }
                }
            }

            // step 1 - search in google.
            using (GoogleSearchClient cl = new GoogleSearchClient())
            {
                ReqGoogleSearch request = new ReqGoogleSearch();
                request.Cx = "017576662512468239146:omuauf_lfve";
                request.Q = "lecture";
                //var test = cl.GetSearchResults(request);
            }

            // step 2 - Crowl content
            // step 3 - Create summary
            // step 3 - Retrive images
            // step 3 - Retrive video
            // step 3 - get prices if available.

            HttpRuntime.Cache.Insert(cacheKey, summary, null, DateTime.Now.AddHours(4), TimeSpan.Zero);

            response.Obj = summary;
            return response;
        }
    }
}