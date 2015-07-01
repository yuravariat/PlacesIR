using ServiceStack.ServiceClient.Web;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;

namespace PlacesIR.GooglePlaces
{
    public class GooglePlacesClient : IDisposable
    {
        #region Properties and fields
        private string GoogleApiKey;
        private ValidationResponse<object> validationResponse;
        public const int MAXIMUM_PAGE_RESULTS = 20;
        public const int DEFAULT_RESULTS = MAXIMUM_PAGE_RESULTS;
        public const int MAXIMUM_RESULTS = 60;
        public const double MAXIMUM_RADIUS = 50000;
        public const int DELAY_BETWEEN_REQUESTS = 2000;

        protected JsonServiceClient sApi;
        protected string apiUrl;
        #endregion

        #region Init
        static GooglePlacesClient()
        {
            JsConfig.DateHandler = JsonDateHandler.ISO8601;
            JsConfig.AssumeUtc = false;
            JsConfig.AppendUtcOffset = false;

            //JsConfig.JsonParseDatesOnlyToJson = true;
        }
        public string ApiUrl
        {
            get { return apiUrl; }
            set
            {
                if (string.IsNullOrEmpty(apiUrl) && Uri.IsWellFormedUriString(apiUrl, UriKind.Absolute))
                {
                    validationResponse.Errors.AddError("SilverWS.Url", "SilverWS.Url (" + (apiUrl == null ? "null" : apiUrl) + ") Cannot be null or invalid");
                }
                else
                {
                    apiUrl = value;
                    sApi.BaseUri = value;
                }
            }
        }
        public System.Net.WebProxy ApiProxy
        {
            get { return (System.Net.WebProxy)sApi.Proxy; }
            set
            {
                sApi.Proxy = value;
            }
        }
        public GooglePlacesClient(int serviceTimeOutSeconds = 25, string apiUrl = null, string user = null, string pass = null)
        {
            if (serviceTimeOutSeconds <= 0)
            {
                serviceTimeOutSeconds = 25;
            }
            validationResponse = new ValidationResponse<object>();
            sApi = new JsonServiceClient();
            apiUrl = apiUrl ?? ConfigurationManager.AppSettings["GooglePlacesAPIUrl"];
            sApi.BaseUri = apiUrl;
            GoogleApiKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
            sApi.Timeout = TimeSpan.FromSeconds(serviceTimeOutSeconds);
        }
        private ValidationResponse<T> ServiceCall<T>(ServiceStack.ServiceHost.IReturn<T> request, RequestMethods method = RequestMethods.GET)
        {
            ValidationResponse<T> response = new ValidationResponse<T>();
            T res = default(T);
            //sApi.Proxy = new System.Net.WebProxy("127.0.0.1", 8888); //Fiddler
            try
            {
                switch (method)
                {
                    case RequestMethods.POST:
                        res = sApi.Post<T>(request);
                        break;
                    case RequestMethods.PUT:
                        res = sApi.Put<T>(request);
                        break;
                    case RequestMethods.DELETE:
                        res = sApi.Delete<T>(request);
                        break;
                    case RequestMethods.GET:
                    default:
                        res = sApi.Get<T>(request);
                        break;
                }
            }
            catch (WebServiceException ex)
            {
                //WebServiceException webEx = ex as WebServiceException;
                /*
                  webEx.StatusCode  = 400
                  webEx.ErrorCode   = ArgumentNullException
                  webEx.Message     = Value cannot be null. Parameter name: Name
                  webEx.StackTrace  = (your Server Exception StackTrace - if DebugMode is enabled)
                  webEx.ResponseDto = (your populated Response DTO)
                  webEx.ResponseStatus   = (your populated Response Status DTO)
                  webEx.GetFieldErrors() = (individual errors for each field if any)
                */
                response.Errors.AddError("Api Error", "request=" + (request != null ? request.ToJson() : "null") + " => statuscode=" + ex.StatusCode + ", " + ex.ResponseBody, level: Level.Error);
            }
            catch (Exception ex)
            {
                response.Errors.AddError("Api Error", "request=" + (request != null ? request.ToJson() : "null") + " =>" + ex, level: Level.Error);
            }
            response.Obj = res;
            return response;
        }
        private ValidationResponse<List<Place>> EvaluateResponse(ref ValidationResponse<List<Place>> response, ValidationResponse<GoogleApiResponse<List<Place>>> res)
        {
            if (response==null)
            {
                response = new ValidationResponse<List<Place>>();
            }
            if (res.Obj != null && res.Obj.status == "OK")
            {
                if (!response.Obj.IsNullOrEmpty())
                {
                    response.Obj.AddRange(res.Obj.results);
                }
                else
                {
                    response.Obj = res.Obj.results;
                }
            }
            else
            {
                if (res.Errors != null)
                {
                    foreach (var key in res.Errors.Keys)
                    {
                        if (!validationResponse.Errors.ContainsKey(key))
                        {
                            response.Errors.Add(key, res.Errors[key]);
                        }
                    }
                }
                response.Errors.Add("Request error", res.Obj != null ? res.Obj.status + " " + res.Obj.error_message : "");
            }
            return response;
        }
        public ValidationResponse<T> CreateValidationResponse<T>()
        {
            ValidationResponse<T> ValidationResponse = new ValidationResponse<T>();
            if (!validationResponse.IsValid)
            {
                ValidationResponse.Errors.AddErrors(validationResponse.Errors);
            }
            return ValidationResponse;
        }
        public void Dispose()
        {
            if (sApi != null)
            {
                sApi.Dispose();
                sApi = null;
            }
        }
        #endregion

        #region Functions

        public ValidationResponse<List<Place>> GetPlacesByQuery(ReqQueryPlaces request)
        {
            ValidationResponse<List<Place>> validationResponse = CreateValidationResponse<List<Place>>();
            #region Validation

            if (!validationResponse.IsValid)
            {
                return validationResponse;
            }
            if (request == null)
            {
                validationResponse.Errors.AddError("Request object null", "Request object cannot be null", level: Level.Error);
                return validationResponse;
            }

            #endregion

            #region Retrive data
            request.key = GoogleApiKey;
            var res = ServiceCall(request, RequestMethods.GET);
            validationResponse = EvaluateResponse(ref validationResponse, res);
            return validationResponse;

            #endregion
        }
        public ValidationResponse<List<Place>> GetNearByPlaces(ReqNearByPlaces request, int limit = 40)
        {
            limit = Math.Min(limit, MAXIMUM_RESULTS); // max of 60 results possible
            int pages = (int)Math.Ceiling(limit / (double)MAXIMUM_PAGE_RESULTS);

            ValidationResponse<List<Place>> validationResponse = CreateValidationResponse<List<Place>>();
            #region Validation

            if (!validationResponse.IsValid)
            {
                return validationResponse;
            }
            if (request == null)
            {
                validationResponse.Errors.AddError("Request object null", "Request object cannot be null", level: Level.Error);
                return validationResponse;
            }

            #endregion

            #region Retrive data
            request.key = GoogleApiKey;
            ValidationResponse<GoogleApiResponse<List<Place>>> res = null;
            for (int i = 0; i < pages; i++)
            {
                res = ServiceCall(request, RequestMethods.GET);
                validationResponse = EvaluateResponse(ref validationResponse, res);
                if ((i + 1) < pages && res.Obj != null && !string.IsNullOrEmpty(res.Obj.next_page_token))
                {
                    request.pagetoken = res.Obj.next_page_token;
                    Thread.Sleep(DELAY_BETWEEN_REQUESTS);  // Page tokens have a delay before they are available.
                }
                else
                {
                    break;
                }
            }
            return validationResponse;

            #endregion
        }

        #endregion
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