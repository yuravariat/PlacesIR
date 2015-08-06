using PlacesIR.GooglePlaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using ServiceStack.Text;
using PlacesIR.Summary;

namespace PlacesIR.Controllers.Api
{
    [RoutePrefix("local-api/places")]
    public class PlacesController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "now works" };
        }
        [Route("nearby")]
        [HttpPost]
        public object NearByPlaces(RequestNearByPlaces req)
        {
            ValidationResponse<List<Place>> nearPlacesResp = new ValidationResponse<List<Place>>();
            if (string.IsNullOrEmpty(req.Place))
            {
                nearPlacesResp.Errors.Add("place name", "place name can not be empty");
                return Json(nearPlacesResp);
            }
            if (req.Distance < 0 || req.Distance > 10000)
            {
                nearPlacesResp.Errors.Add("distance limit", "distanse can be only between 0.1 and 10 km");
                return Json(nearPlacesResp);
            }

            try
            {
                using (GooglePlacesClient placesClient = new GooglePlacesClient())
                {
                    ValidationResponse<List<Place>> placesResp = placesClient.GetPlacesByQuery(new ReqQueryPlaces()
                    {
                        query = req.Place
                    });
                    if (!placesResp.Obj.IsNullOrEmpty())
                    {
                        Place pl = placesResp.Obj.FirstOrDefault();
                        nearPlacesResp = placesClient.GetNearByPlaces(new ReqNearByPlaces()
                        {
                            location = pl.geometry.location.lat + "," + pl.geometry.location.lng,
                            radius = req.Distance, // req.Rankby == RankBy.distance ? new Nullable<int>() : req.Distance,
                            keyword = req.Keywords
                            //rankby = req.Rankby.ToString()
                        });
                        if (nearPlacesResp.IsValid && !nearPlacesResp.Obj.IsNullOrEmpty())
                        {
                            return Json(new { IsValid = true, NearByPlaces = nearPlacesResp.Obj, CenterPlace = pl });
                        }
                    }
                    else
                    {
                        return Json(placesResp);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.WriteLog("Place controller NearByPlaces error", "", ex, Level.Error);
                nearPlacesResp.Errors.Add("Unexpected error", "Unexpected error " + ex.Message);
            }
            return Json(nearPlacesResp);
        }

        [Route("details")]
        [HttpPost]
        public object DetailsPlaces(RequestPlaceDetails req)
        {
            ValidationResponse<PlaceSummary> response = new ValidationResponse<PlaceSummary>();
            if (string.IsNullOrEmpty(req.PlaceID))
            {
                response.Errors.Add("placeID", "placeID can not be empty");
                return Json(response);
            }
            if (string.IsNullOrEmpty(req.Lang))
            {
                req.Lang = "en";
            }
            try
            {
                response = PlaceSummaryCrawler.PrepareSummary(req.PlaceID, req.MainPlaceName, req.Lang);
            }
            catch (Exception ex)
            {
                LogHandler.WriteLog("Place controller PlacesDetails error", "", ex, Level.Error);
                response.Errors.Add("Unexpected error", "Unexpected error " + ex.Message);
            }
            return Json(response);
        }
    }
}
