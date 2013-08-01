using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace relmon.Controllers.FrontEnd
{
    public class DataBisnisAfiliasiController : Controller
    {
        //
        // GET: /DataBisnisAfiliasi/

        public ActionResult Index()
        {
            ViewBag.selectedMenu = "databisnis";
            return View();
        }

        public ActionResult Connector(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }

        public ActionResult Profil(int id)
        {
            switch (id)
            {
                case 0:
                    ViewBag.content = System.IO.File.ReadAllText(Server.MapPath("~/Content/data/profile/af/profile-PertaDayaGas.txt"));
                    break;
                case 1:
                    ViewBag.content = System.IO.File.ReadAllText(Server.MapPath("~/Content/data/profile/af/profile-PertaSamtanGas.txt"));
                    break;
                case 2:
                    ViewBag.content = System.IO.File.ReadAllText(Server.MapPath("~/Content/data/profile/af/profile-Pertagas.txt"));
                    break;
                case 3:
                    ViewBag.content = System.IO.File.ReadAllText(Server.MapPath("~/Content/data/profile/af/profile-PertaKalimantanGas.txt"));
                    break;
            }
            return PartialView();
        }

        public ActionResult StrukturOrganisasi()
        {

            return PartialView();
        }

        public ActionResult AdArt()
        {
            return PartialView();
        }

        public ActionResult RencanaKerja()
        {
            return PartialView();
        }

        public ActionResult Rjpp()
        {
            return PartialView();
        }

        public ActionResult Rkap()
        {
            return PartialView();
        }

        public ActionResult BussinessReport()
        {
            return PartialView();
        }

        public ActionResult Kinerja()
        {
            return PartialView();
        }

        public ActionResult ProjectStatus()
        {
            return PartialView();
        }

        public ActionResult Product()
        {
            return PartialView();
        }
    }
}
