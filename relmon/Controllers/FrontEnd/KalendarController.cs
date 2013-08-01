using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace relmon.Controllers.FrontEnd
{
    public class KalendarController : Controller
    {
        //
        // GET: /Kalendar/

        public ActionResult Index()
        {
            ViewBag.selectedMenu = "kalendar";
            return View();
        }

    }
}
