using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using relmon.Models;

namespace relmon.Controllers.FrontEnd
{
    public class ProfilDitGasController : Controller
    {
        private RelmonStoreEntities db = new RelmonStoreEntities();
        //
        // GET: /ProfilDitGas/

        public ActionResult Index()
        {
            ViewBag.profil = db.dashboard_default
                .Where(x => x.tipe == "profil")
                .Select(x => x.konten)
                .FirstOrDefault();
            ViewBag.selectedMenu = "profil";
            return View();
        }

    }
}
