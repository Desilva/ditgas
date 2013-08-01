using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace relmon.Controllers.FrontEnd
{
    public class OrganisasiDitGasController : Controller
    {
        //
        // GET: /OrganisasiDitGas/

        public ActionResult Index()
        {
            ViewBag.selectedMenu = "profil";
            return View();
        }

    }
}
