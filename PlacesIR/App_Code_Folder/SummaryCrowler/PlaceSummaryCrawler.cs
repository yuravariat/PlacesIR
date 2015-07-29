using PlacesIR.Aylien;
using PlacesIR.GooglePlaces;
using PlacesIR.GoogleSearch;
using PlacesIR.YouTube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace PlacesIR.Summary
{
    public class PlaceSummaryCrawler
    {
        public static ValidationResponse<PlaceSummary> PrepareSummary(string placeIDToSummarize, string mainPlaceNearByName, string lang = "en")
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
                var placeRes = placesClient.GetPlacesDetails(new ReqPlaceDetails()
                {
                    placeid = summary.PlaceIDToSummarize,
                    language = lang
                });
                if (placeRes.Obj != null)
                {
                    summary.Place = placeRes.Obj;
                }
                else
                {
                    foreach (var error in placeRes.Errors)
                    {
                        response.Errors.Add(error.Key, error.Value);
                    }
                }
            }
            if (summary.Place != null)
            {
                // step 2 - Create summary
                summary.MainSummaryText = summary.Place.name;
                summary.MainSummarySourceUrl = "";

                using (AylienClient summClient = new AylienClient())
                {
                    ReqSummarise summReq = new ReqSummarise();
                    summReq.sentences_number = 4;
                    summReq.language = "auto";

                    // Try to retrieve summary from place website
                    if (!string.IsNullOrEmpty(summary.Place.website))
                    {
                        summReq.url = summary.Place.website;
                        var summResp = summClient.Summarise(summReq);
                        if (summResp.Obj != null && !string.IsNullOrEmpty(summResp.Obj.text))
                        {
                            summary.MainSummaryText = summResp.Obj.text;
                            summary.MainSummarySourceUrl = summary.Place.website;
                        }
                    }

                    // Try to retrieve summary from google search results
                    if (string.IsNullOrEmpty(summary.MainSummarySourceUrl) || summary.MainSummaryText.Length < 50)
                    {
                        // Google search
                        Result webpageResult = null;
                        List<Result> googleSearchResults = null;
                        using (GoogleSearchClient clWebPages = new GoogleSearchClient())
                        {
                            ReqGoogleSearch req = new ReqGoogleSearch();
                            req.q = summary.Place.name + ", " + mainPlaceNearByName;
                            req.hl = lang;
                            req.lr = "lang_" + lang;
                            var googleSearchResultsResp = clWebPages.GetSearchResults(req);
                            if (googleSearchResultsResp.Obj != null && googleSearchResultsResp.Obj.Items != null && googleSearchResultsResp.Obj.Items.Count > 0)
                            {
                                googleSearchResults = googleSearchResultsResp.Obj.Items.ToList();
                                webpageResult = googleSearchResults[0];
                            }
                        }

                        // Summary
                        if (webpageResult != null)
                        {
                            summReq.url = webpageResult.Link;
                            var summResp = summClient.Summarise(summReq);
                            if (summResp.Obj != null && !string.IsNullOrEmpty(summResp.Obj.text))
                            {
                                summary.MainSummaryText = summResp.Obj.text;
                                summary.MainSummarySourceUrl = webpageResult.Link;
                            }
                            if ((string.IsNullOrEmpty(summary.MainSummarySourceUrl) || summary.MainSummaryText.Length < 50)
                                && googleSearchResults != null && googleSearchResults.Count > 0)
                            {
                                // try to summarize another result if first was failed or too short.
                                webpageResult = googleSearchResults[0];
                                summReq.url = webpageResult.Link;
                                summResp = summClient.Summarise(summReq);
                                if (summResp.Obj != null && !string.IsNullOrEmpty(summResp.Obj.text) && summResp.Obj.text.Length > summary.MainSummaryText.Length)
                                {
                                    summary.MainSummaryText = summResp.Obj.text;
                                    summary.MainSummarySourceUrl = webpageResult.Link;
                                }
                            }
                        }
                    }
                    // try to imrove summary
                    if (!string.IsNullOrEmpty(summary.MainSummarySourceUrl))
                    {
                        ReqSummarise summFuncReq = new ReqSummarise();
                        summFuncReq.text = summary.MainSummaryText;
                        summFuncReq.sentences_number = 3;
                        var summFuncResp = summClient.Summarise(summFuncReq);
                        if (summFuncResp.Obj != null && !string.IsNullOrEmpty(summFuncResp.Obj.text))
                        {
                            summary.MainSummaryText = summFuncResp.Obj.text;
                        }
                    }
                }

                // step 3 - Retrieve images
                using (GoogleSearchClient clImages = new GoogleSearchClient())
                {
                    ReqGoogleSearch req = new ReqGoogleSearch();
                    req.q = summary.Place.name + ", " + mainPlaceNearByName;
                    req.searchType = PlacesIR.GoogleSearch.ReqGoogleSearch.SearchTypeEnum.image;
                    req.hl = lang;
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

                // step 4 - Retrieve videos
                using (YouTubeClient YouTubeClient = new YouTubeClient())
                {
                    ReqSearch req = new ReqSearch();
                    req.q = summary.Place.name + ", " + mainPlaceNearByName;
                    req.relevanceLanguage = lang;
                    var youResp = YouTubeClient.GetSearchResults(req);
                    if (youResp.Obj != null && youResp.Obj.items != null)
                    {
                        summary.Videos = youResp.Obj.items;
                    }
                }


                // step 5 - get prices if available.
                // TODO Retrieve prices if available

                HttpRuntime.Cache.Insert(cacheKey, summary, null, DateTime.Now.AddHours(4), TimeSpan.Zero);
            }

            response.Obj = summary;
            return response;
        }
    }
}