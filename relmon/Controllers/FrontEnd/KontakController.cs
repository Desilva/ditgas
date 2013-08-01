using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace relmon.Controllers.FrontEnd
{
    public class KontakController : Controller
    {
        //
        // GET: /Kontrak/

        public ActionResult Index()
        {
            ViewBag.selectedMenu = "kontak";
            return View();
        }

    }
}
