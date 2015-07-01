using PlacesIR.GooglePlaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using ServiceStack.Text;

namespace PlacesIR.Controllers.Api
{
    [RoutePrefix("local-api/places")]
    public class PlacesController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "works" };
        }
        [Route("nearby")]
        [HttpPost]
        public object NearByPlaces(RequestNearByPlaces req)
        {
            ValidationResponse<List<Place>> nearPlacesResp = new ValidationResponse<List<Place>>();
            if (string.IsNullOrEmpty(req.Place))
            {
                nearPlacesResp.Errors.Add("place name", "place name can not be empty");
            }
            if (req.Distance < 100 || req.Distance > 5000)
            {
                nearPlacesResp.Errors.Add("distance limit", "distanse can be only between 100 and 5000");
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
            }
            return Json(nearPlacesResp);
        }
    }
}
