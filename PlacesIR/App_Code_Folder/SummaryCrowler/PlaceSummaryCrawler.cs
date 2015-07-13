using PlacesIR.Aylien;
using PlacesIR.GooglePlaces;
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
        public static ValidationResponse<PlaceSummary> PrepareSummary(string placeIDToSummarize,string mainPlaceNearByName, string lang = "lang_en")
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
            if (summary.Place != null)
            {
                // step 1 - search in google.
                Result webpageResult = null;
                using (GoogleSearchClient clWebPages = new GoogleSearchClient())
                {
                    ReqGoogleSearch req = new ReqGoogleSearch();
                    req.q = summary.Place.name + ", " + mainPlaceNearByName;
                    var imgResp = clWebPages.GetSearchResults(req);
                    if (imgResp.Obj != null && imgResp.Obj.Items != null && imgResp.Obj.Items.Count>0)
                    {
                        webpageResult = imgResp.Obj.Items[0];
                    }
                }

                // step 2 - Create summary
                summary.MainSummaryText = summary.Place.name;
                if (webpageResult != null)
                {
                    using (AylienClient summClient = new AylienClient())
                    {
                        // short way
                        ReqSummarise summReq = new ReqSummarise();
                        summReq.url = webpageResult.Link;
                        summReq.sentences_number = 3;
                        var summResp = summClient.Summarise(summReq);
                        if (summResp.Obj != null && !string.IsNullOrEmpty(summResp.Obj.text))
                        {
                            summary.MainSummaryText = summResp.Obj.text;
                        }

                        // long way
                        //ReqExtract extrTextReq = new ReqExtract();
                        //extrTextReq.url = webpageResult.Link;
                        //var resp = summClient.ExtractArticle(extrTextReq);
                        //if (resp.Obj != null && !string.IsNullOrEmpty(resp.Obj.article))
                        //{
                        //    ReqSummarise summReq = new ReqSummarise();
                        //    summReq.text = resp.Obj.article;
                        //    summReq.sentences_number = 3;
                        //    var summResp = summClient.Summarise(summReq);
                        //    if (summResp.Obj != null && !string.IsNullOrEmpty(summResp.Obj.text))
                        //    {
                        //        summary.MainSummaryText = summResp.Obj.text;
                        //    }
                        //}
                    }
                }
                
                // step 3 - Retrive images
                using (GoogleSearchClient clImages = new GoogleSearchClient())
                {
                    ReqGoogleSearch req = new ReqGoogleSearch();
                    req.q = summary.Place.name + ", " + mainPlaceNearByName;
                    req.searchType = PlacesIR.GoogleSearch.ReqGoogleSearch.SearchTypeEnum.image;
                    var imgResp = clImages.GetSearchResults(req);
                    if (imgResp.Obj != null && imgResp.Obj.Items != null)
                    {
                        foreach (var item in imgResp.Obj.Items)
                        {
                            Image img = new Image();
                            img.title = item.Title;
                            img.url = item.Link;
                            img.thumb_url = item.Image.ThumbnailLink;
                            summary.Images.Add(img);
                        }
                    }
                }

                // step 4 - Retrive video
                
                // step 5 - get prices if available.

                HttpRuntime.Cache.Insert(cacheKey, summary, null, DateTime.Now.AddHours(4), TimeSpan.Zero);
            }

            response.Obj = summary;
            return response;
        }
    }
}