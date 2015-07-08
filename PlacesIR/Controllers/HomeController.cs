using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.Text;
using PlacesIR.GoogleSearch;
using PlacesIR.Summary;
using PlacesIR.Aylien;

namespace PlacesIR
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Title = "Places IR";
            return View("Home");
        }
    }
}