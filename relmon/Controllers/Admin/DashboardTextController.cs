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

namespace relmon.Controllers.Admin
{
    [Authorize]
    public class DashboardTextController : Controller
    {
        private RelmonStoreEntities db = new RelmonStoreEntities();

        //
        // GET: /DashboardAdmin/

        const int MAX_KONTEN_INFORMASI = 100;
        const int MAX_KONTEN_KEBIJAKAN = 100;

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
            //List<dashboard_text> result = (from x in db.dashboard_text
            //                               where x.tipe == tipe
            //                               select new dashboard_text
            //                               {
            //                                   id = x.id,
            //                                   kategori = x.kategori,
            //                                   konten = x.konten,
            //                                   poster_user_id = x.poster_user_id,
            //                                   tgl_update = x.tgl_update,
            //                                   user_rm_role = new user_rm_role
            //                                   {
            //                                       username = x.user.username
            //                                   }
            //                               }).ToList();
            List<dashboard_text> result = (from x in db.dashboard_text
                                           where x.tipe == tipe
                                           select x).ToList();

            foreach (dashboard_text res in result)
            {
                res.konten = (res.konten.Length > MAX_KONTEN_KEBIJAKAN) ? res.konten.Substring(0, MAX_KONTEN_KEBIJAKAN) + "..." : res.konten;
                res.poster_username = (from x in db.users
                                       where x.id == res.poster_user_id
                                       select x.username).FirstOrDefault();
            }

            return View(new GridModel<dashboard_text>
            {
                Data = result
            });
        }

        //insert data user
        [HttpPost]
        public void create(dashboard_text dashboard_text)
        {
            dashboard_text.tgl_update = DateTime.Now;
            dashboard_text.konten = HttpUtility.UrlDecode(dashboard_text.konten);
            db.dashboard_text.Add(dashboard_text);
            db.SaveChanges();
        }

        //update data user
        [HttpPost]
        public void update(dashboard_text dashboard_text)
        {
            dashboard_text x = db.dashboard_text.Find(dashboard_text.id);
            x.konten = HttpUtility.UrlDecode(dashboard_text.konten);
            x.ringkasan = dashboard_text.ringkasan;
            x.kategori = dashboard_text.kategori;
            x.poster_user_id = dashboard_text.poster_user_id;
            x.tgl_update = DateTime.Now;
            db.Entry(x).State = EntityState.Modified;
            db.SaveChanges();
        }

        //delete data user
        private string delete(int id)
        {
            dashboard_text x = db.dashboard_text.Find(id);
            string tipe = x.tipe;
            db.dashboard_text.Remove(x);
            db.SaveChanges();
            return tipe;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public JsonResult add()
        {
            return Json(new
            {
                poster_user_id = Session["id"]
            });
        }

        public JsonResult GetDetail(int id)
        {
            dashboard_text x = db.dashboard_text.Find(id);
            return Json(new
            {
                id = x.id,
                kategori = x.kategori,
                poster_user_id = Session["id"],
                ringkasan = x.ringkasan,
                konten = x.konten
            });
        }

        #region admin home tab

        public ActionResult Home_News()
        {
            return PartialView();
        }

        public ActionResult Home_Informasi()
        {
            return PartialView();
        }

        public ActionResult Home_Kebijakan()
        {
            return PartialView();
        }

        #endregion

        #region admin profil tab

        #endregion
    }
}