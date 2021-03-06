﻿using PlacesIR.Aylien;
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
                summary.MainSummarySources = new List<string>();


                using (AylienClient summClient = new AylienClient())
                {
                    ReqSummarise summReq = new ReqSummarise();
                    summReq.sentences_number = 6;
                    summReq.language = "auto";
                    List<string> summarizedArticles = new List<string>();
                    
                    string SourcesToSummarizeNumberStr = System.Configuration.ConfigurationManager.AppSettings["SourcesToSummarizeNumber"];
                    short SourcesToSummarizeNumber;
                    Int16.TryParse(SourcesToSummarizeNumberStr, out SourcesToSummarizeNumber);
                    if (SourcesToSummarizeNumber == 0)
                    {
                        SourcesToSummarizeNumber = 3;
                    }

                    // Try to retrieve summary from place website
                    if (!string.IsNullOrEmpty(summary.Place.website))
                    {
                        summReq.url = summary.Place.website;
                        var summResp = summClient.Summarise(summReq);
                        if (summResp.Obj != null && summResp.Obj.sentences != null && summResp.Obj.sentences.Count > 0)
                        {
                            summarizedArticles.Add(string.Join(" ", summResp.Obj.sentences));
                            summary.MainSummarySources.Add(summary.Place.website);
                        }
                    }

                    // Try to retrieve summary from google search results

                    // Google search
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
                        }
                    }

                    // Summaries from search results
                    if (googleSearchResults != null && googleSearchResults.Count > 0)
                    {
                        int maxResultsToSearchIn = 4;
                        for (int i = 0; i < googleSearchResults.Count 
                            && i < maxResultsToSearchIn 
                            && summarizedArticles.Count < SourcesToSummarizeNumber
                            && !summary.MainSummarySources.Contains(googleSearchResults[i].Link)
                            ; i++)
                        {
                            summReq.url = googleSearchResults[i].Link;
                            var summResp = summClient.Summarise(summReq);
                            if (summResp.Obj != null && summResp.Obj.sentences != null && summResp.Obj.sentences.Count > 0)
                            {
                                summarizedArticles.Add(string.Join(" ", summResp.Obj.sentences));
                                summary.MainSummarySources.Add(googleSearchResults[i].Link);
                            }
                        }
                    }

                    // Summarise all summaries to one big summary.
                    if (summarizedArticles.Count > 0)
                    {
                        ReqSummarise summFuncReq = new ReqSummarise();
                        summFuncReq.title = summary.Place.name;
                        summFuncReq.text = string.Join("\n\r\\s", summarizedArticles.OrderByDescending(s => s.Length)); // Combine all summarized articles.
                        summFuncReq.sentences_number = 5;
                        var summFuncResp = summClient.Summarise(summFuncReq);
                        if (summFuncResp.Obj != null && summFuncResp.Obj.sentences != null && summFuncResp.Obj.sentences.Count > 0)
                        {
                            summary.MainSummaryText = string.Join(" ", summFuncResp.Obj.sentences);
                        }
                        else
                        {
                            // fallback just print combined text.
                            summary.MainSummaryText = summFuncReq.text;
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