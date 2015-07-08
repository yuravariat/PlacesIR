using ServiceStack.ServiceClient.Web;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;

namespace PlacesIR.GoogleSearch
{
    public class GoogleSearchClient : IDisposable
    {
        #region Properties and fields
        private string GoogleApiKey;
        private string GoogleCustomeSearchEngineID;
        private ValidationResponse<object> validationResponse;

        protected JsonServiceClient sApi;
        protected string apiUrl;
        #endregion

        #region Init
        static GoogleSearchClient()
        {
            JsConfig.DateHandler = JsonDateHandler.ISO8601;
            JsConfig.AssumeUtc = false;
            JsConfig.AppendUtcOffset = false;
            JsConfig.ExcludeTypeInfo = true;
            //JsConfig.JsonParseDatesOnlyToJson = true;
        }
        public string ApiUrl
        {
            get { return apiUrl; }
            set
            {
                if (string.IsNullOrEmpty(apiUrl) && Uri.IsWellFormedUriString(apiUrl, UriKind.Absolute))
                {
                    validationResponse.Errors.AddError("GoogleSearchClient", "GoogleSearchClient (" + (apiUrl == null ? "null" : apiUrl) + ") Cannot be null or invalid");
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
        public GoogleSearchClient(int serviceTimeOutSeconds = 25, string apiUrl = null, string user = null, string pass = null)
        {
            if (serviceTimeOutSeconds <= 0)
            {
                serviceTimeOutSeconds = 25;
            }
            validationResponse = new ValidationResponse<object>();
            sApi = new JsonServiceClient();
            apiUrl = apiUrl ?? ConfigurationManager.AppSettings["GoogleSearchAPIUrl"];
            sApi.BaseUri = apiUrl;
            GoogleApiKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
            GoogleCustomeSearchEngineID = ConfigurationManager.AppSettings["GoogleCustomeSearchEngineID"];
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

        public ValidationResponse<GoogleSearchApiResponse> GetSearchResults(ReqGoogleSearch request)
        {
            ValidationResponse<GoogleSearchApiResponse> validationResponse = CreateValidationResponse<GoogleSearchApiResponse>();
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
            request.cx = GoogleCustomeSearchEngineID;
            var res = ServiceCall(request, RequestMethods.GET);
            return res;

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