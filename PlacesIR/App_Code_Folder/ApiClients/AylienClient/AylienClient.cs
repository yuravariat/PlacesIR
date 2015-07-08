using ServiceStack.ServiceClient.Web;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;

namespace PlacesIR.Aylien
{
    public class AylienClient : IDisposable
    {
        #region Properties and fields
        private string XAYLIENTextAPIApplicationKey;
        private string XAYLIENTextAPIApplicationID;
        private ValidationResponse<object> validationResponse;

        protected JsonServiceClient sApi;
        protected string apiUrl;
        #endregion

        #region Init
        static AylienClient()
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
                    validationResponse.Errors.AddError("GooglePlacesClient", "GooglePlacesClient (" + (apiUrl == null ? "null" : apiUrl) + ") Cannot be null or invalid");
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
        public AylienClient(int serviceTimeOutSeconds = 25, string apiUrl = null, string user = null, string pass = null)
        {
            if (serviceTimeOutSeconds <= 0)
            {
                serviceTimeOutSeconds = 25;
            }
            validationResponse = new ValidationResponse<object>();
            sApi = new JsonServiceClient();
            apiUrl = apiUrl ?? ConfigurationManager.AppSettings["AylienAPIUrl"];
            sApi.BaseUri = apiUrl;
            XAYLIENTextAPIApplicationKey = ConfigurationManager.AppSettings["XAYLIENTextAPIApplicationKey"];
            XAYLIENTextAPIApplicationID = ConfigurationManager.AppSettings["XAYLIENTextAPIApplicationID"];
            sApi.Headers.Add("X-AYLIEN-TextAPI-Application-Key", XAYLIENTextAPIApplicationKey);
            sApi.Headers.Add("X-AYLIEN-TextAPI-Application-ID", XAYLIENTextAPIApplicationID);
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

        public ValidationResponse<ExtractResponse> ExtractArticle(ReqExtract request)
        {
            ValidationResponse<ExtractResponse> validationResponse = CreateValidationResponse<ExtractResponse>();
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
            return ServiceCall(request, RequestMethods.GET);
            #endregion
        }
        public ValidationResponse<SummaryResponse> Summarise(ReqSummarise request)
        {
            ValidationResponse<SummaryResponse> validationResponse = CreateValidationResponse<SummaryResponse>();
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
            return ServiceCall(request, RequestMethods.GET);
            #endregion
        }
        public ValidationResponse<LangDetectionResponse> DetectLanguage(ReqLangDetect request)
        {
            ValidationResponse<LangDetectionResponse> validationResponse = CreateValidationResponse<LangDetectionResponse>();
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
            return ServiceCall(request, RequestMethods.GET);
            #endregion
        }
        public ValidationResponse<List<HCard>> GetMicroData(ReqMicroformats request)
        {
            ValidationResponse<List<HCard>> validationResponse = CreateValidationResponse<List<HCard>>();
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
            return ServiceCall(request, RequestMethods.GET);
            #endregion
        }

        #endregion
    }
}