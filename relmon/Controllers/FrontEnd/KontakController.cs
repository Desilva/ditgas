using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using relmon.Models;

namespace relmon.Controllers.FrontEnd
{
    public class KontakController : Controller
    {
        private RelmonStoreEntities db = new RelmonStoreEntities();
        public ActionResult Index()
        {
            ViewBag.selectedMenu = "kontak";
            return View();
        }

        public JsonResult GetKontak(string tipe)
        {
            var temp = db.kontaks.Where(x=>x.tipe == tipe).ToList();
            return Json(temp);
        }
    }
}
