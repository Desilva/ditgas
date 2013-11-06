using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using relmon.Models;
using System.Data;

namespace relmon.Controllers.Admin
{
    public class KontakAdminController : Controller
    {
        private RelmonStoreEntities db = new RelmonStoreEntities();
        //
        // GET: /Kontak/

        public ActionResult Index(string tipe)
        {
            ViewBag.tipe = tipe;
            return PartialView();
        }

        public ActionResult binding(string tipe)
        {
            var temp = (from x in db.kontaks
                        where (x.tipe == tipe)
                        select x).ToList();
            
            return View(new GridModel(temp));
        }

        [GridAction]
        public ActionResult _SelectAjaxEditing(string tipe)
        {
            return binding(tipe);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SaveAjaxEditing(int id)
        {
            kontak getKontak = db.kontaks.Where(x => x.id == id).SingleOrDefault();


            TryUpdateModel(getKontak);
            var tipe = getKontak.tipe;
            db.Entry(getKontak).State = EntityState.Modified;
            db.SaveChanges();
            return binding(tipe);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertAjaxEditing(string tipe)
        {
            kontak newKontak = new kontak();
            if (TryUpdateModel(newKontak))
            {
                newKontak.tipe = tipe;
                db.kontaks.Add(newKontak);
                db.SaveChanges();
            }
            return binding(tipe);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteAjaxEditing(int id)
        {
            kontak getKontak = db.kontaks.Where(x => x.id == id).SingleOrDefault();
            var tipe = getKontak.tipe;
            db.kontaks.Remove(getKontak);
            db.SaveChanges();
            //Rebind the grid
            return binding(tipe);
        }
    }
}
