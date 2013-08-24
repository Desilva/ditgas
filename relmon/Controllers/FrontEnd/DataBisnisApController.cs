using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using relmon.Utilities;
using iTextSharp.text.pdf;
using relmon.Models;

namespace relmon.Controllers.FrontEnd
{
    public class DataBisnisApController : DataBisnisDitGasController
    {
        public ActionResult Connector(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }

        public ActionResult Profil(int id)
        {
            ViewBag.id = id;
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();
            var get = result.First();
            ViewBag.content = get.profile;
            if (string.IsNullOrWhiteSpace(get.profile))
            {
                ViewBag.content = "Profil belum tersedia";
            }
            return PartialView();
        }

        public ActionResult StrukturOrganisasi(int id)
        {
            ViewBag.id = id;
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();
            var get = result.First();
            ViewBag.content = "../../upload/Data Bisnis/" + id + "/struktur organisasi/" + get.struktur;

            return PartialView();
        }

        public ActionResult RencanaKerja(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }
    }
}
