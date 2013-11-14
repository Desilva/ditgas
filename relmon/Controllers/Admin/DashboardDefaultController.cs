using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using relmon.Models;
using Telerik.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;
using System.IO;

namespace relmon.Controllers.Admin
{
    [Authorize]
    public class DashboardDefaultController : Controller
    {
        private RelmonStoreEntities db = new RelmonStoreEntities();

        //
        // GET: /DashboardAdmin/

        public ActionResult Index()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Value = "",
                Text = "-- Pilih Parent --"
            });

            IEnumerable<SelectListItem> itemsResult = items.Concat(db.companies
                .AsEnumerable()
                .Where(c => c.id >= 0)
                .OrderBy(c => c.nama)
                .Select(c => new SelectListItem
                {
                    Value = c.id.ToString(),
                    Text = c.nama
                })
                .ToList()
            );

            ViewBag.parent = itemsResult;
            ViewBag.edit_parent = itemsResult;

            return PartialView(db.companies.ToList());
        }

        //
        // Ajax select binding
        [GridAction]
        public ActionResult _SelectAjaxEditing(string tipe)
        {
            return binding(tipe);
        }

        //
        // Ajax delete binding
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteAjaxEditing(int id)
        {
            string tipe = delete(id);
            return binding(tipe);
        }

        //select data user
        private ViewResult binding(string tipe)
        {
            var result = from o in db.dashboard_default
                         where o.tipe == tipe
                         select o;

            return View(new GridModel<dashboard_default>
            {
                Data = result
            });
        }

        //insert data user
        [HttpPost]
        public void create(dashboard_default dashboard_default)
        {
            db.dashboard_default.Add(dashboard_default);
            db.SaveChanges();
        }

        //update data user
        [HttpPost]
        public void update(dashboard_default dashboard_default)
        {
            dashboard_default x = db.dashboard_default.Find(dashboard_default.id);
            x.konten = dashboard_default.konten;
            db.Entry(x).State = EntityState.Modified;
            db.SaveChanges();
        }

        //delete data user
        private string delete(int id)
        {
            dashboard_default x = db.dashboard_default.Find(id);
            string tipe = x.tipe;
            db.dashboard_default.Remove(x);
            db.SaveChanges();
            return tipe;
        }

        //delete data user
        [HttpPost]
        public void deleteAjax(int id)
        {
            dashboard_default x = db.dashboard_default.Find(id);
            string tipe = x.tipe;
            db.dashboard_default.Remove(x);
            db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public JsonResult add()
        {
            return Json(true);
        }

        public JsonResult GetDetail(int id)
        {
            dashboard_default x = db.dashboard_default.Find(id);
            return Json(new
            {
                id = x.id,
                konten = x.konten
            });
        }

        #region admin home tab

        public ActionResult Home_Index()
        {
            return PartialView();
        }

        public ActionResult Home_Slideshow()
        {
            return PartialView();
        }

        public ActionResult Home_Slideshow_Content()
        {
            ViewBag.slideshow = db.dashboard_default.Where(x => x.tipe == "slideshow").ToList();
            return PartialView();
        }

        public ActionResult Home_Banner()
        {
            return PartialView();
        }

        public ActionResult Home_Banner_Content()
        {
            ViewBag.banner_judul = db.dashboard_default.Where(x => x.tipe == "banner_judul").Select(x => x.konten).FirstOrDefault();
            ViewBag.banner_isi = db.dashboard_default.Where(x => x.tipe == "banner_isi").Select(x => x.konten).FirstOrDefault();
            return PartialView();
        }

        public ActionResult Home_RoadMap()
        {
            return PartialView();
        }

        public ActionResult Home_RoadMap_Content()
        {
            ViewBag.pdfroadmap = db.dashboard_default.Where(x => x.tipe == "pdfroadmap").Select(x => x.konten).FirstOrDefault();
            return PartialView();
        }

        public ActionResult Home_Highlight()
        {
            return PartialView();
        }

        public ActionResult Home_Highlight_Content()
        {
            ViewBag.highlight = db.dashboard_default.Where(x => x.tipe == "highlight").ToList();
            return PartialView();
        }

        [HttpPost]
        public void saveBanner(string judul, string isi)
        {
            dashboard_default dashboardDefault = db.dashboard_default.Where(x => x.tipe == "banner_judul").FirstOrDefault();
            dashboardDefault.konten = judul;
            db.Entry(dashboardDefault).State = EntityState.Modified;

            dashboard_default dashboardDefault2 = db.dashboard_default.Where(x => x.tipe == "banner_isi").FirstOrDefault();
            dashboardDefault2.konten = isi;
            db.Entry(dashboardDefault2).State = EntityState.Modified;
            db.SaveChanges();
        }

        [HttpPost]
        public void uploadSlideshow(HttpPostedFileBase userfile)
        {
            var fileName = Path.GetFileName(userfile.FileName);
            var path = Path.Combine(Server.MapPath("~/Content/image/"), fileName);
            userfile.SaveAs(path);

            dashboard_default dashboardDefault = new dashboard_default();
            dashboardDefault.tipe = "slideshow";
            dashboardDefault.konten = "/Content/image/" + fileName;
            create(dashboardDefault);
        }

        [HttpPost]
        public void uploadHighlight(HttpPostedFileBase userfile)
        {
            var fileName = Path.GetFileName(userfile.FileName);
            var path = Path.Combine(Server.MapPath("~/Content/image/"), fileName);
            userfile.SaveAs(path);

            dashboard_default dashboardDefault = new dashboard_default();
            dashboardDefault.tipe = "highlight";
            dashboardDefault.konten = "/Content/image/" + fileName;
            create(dashboardDefault);
        }

        [HttpPost]
        public void uploadRoadMap(HttpPostedFileBase userfile)
        {
            var fileName = Path.GetFileName(userfile.FileName);
            var path = Path.Combine(Server.MapPath("~/Content/data/road-map/"), fileName);
            userfile.SaveAs(path);

            dashboard_default dashboardDefault = db.dashboard_default.Where(x => x.tipe == "pdfroadmap").FirstOrDefault();
            dashboardDefault.konten = "~/Content/data/road-map/" + fileName;
            db.Entry(dashboardDefault).State = EntityState.Modified;
            db.SaveChanges();
        }

        #endregion

        #region admin profil tab

        public ActionResult Profil_Index()
        {
            return PartialView();
        }

        public ActionResult Profil_Profil()
        {
            return PartialView();
        }

        public ActionResult Profil_Profil_Content()
        {
            ViewBag.profil = db.dashboard_default.Where(x => x.tipe == "profil").Select(x => x.konten).FirstOrDefault();
            return PartialView();
        }

        public ActionResult Profil_VisiMisi()
        {
            return PartialView();
        }

        public ActionResult Profil_VisiMisi_Content()
        {
            ViewBag.visimisi = db.dashboard_default.Where(x => x.tipe == "visimisi").Select(x => x.konten).FirstOrDefault();
            return PartialView();
        }

        [HttpPost]
        public void saveProfil(string konten)
        {
            dashboard_default dashboardDefault = db.dashboard_default.Where(x => x.tipe == "profil").FirstOrDefault();
            dashboardDefault.konten = HttpUtility.UrlDecode(konten);
            db.Entry(dashboardDefault).State = EntityState.Modified;
            db.SaveChanges();
        }

        [HttpPost]
        public void uploadVisiMisi(HttpPostedFileBase userfile)
        {
            var fileName = Path.GetFileName(userfile.FileName);
            var path = Path.Combine(Server.MapPath("~/Content/data/visi-misi/"), fileName);
            userfile.SaveAs(path);

            dashboard_default dashboardDefault = db.dashboard_default.Where(x => x.tipe == "visimisi").FirstOrDefault();
            dashboardDefault.konten = "~/Content/data/visi-misi/" + fileName;
            db.Entry(dashboardDefault).State = EntityState.Modified;
            db.SaveChanges();
        }

        #endregion
    }
}