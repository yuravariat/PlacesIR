using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.Text;
using PlacesIR.GoogleSearch;
using PlacesIR.Summary;

namespace PlacesIR
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //PlaceSummaryCrawler crw = new PlaceSummaryCrawler("ChIJN1t_tDeuEmsRUsoyG83frY4");
            //crw.PrepareSummary();

            ViewBag.Title = "Places IR";
            return View("Home");
        }
    }
}