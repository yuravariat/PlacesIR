using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace PlacesIR
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Web Api
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Site
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            LogHandler.WriteLog("Application start");
        }
    }
}
