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
    public class CompanyController : Controller
    {
        private RelmonStoreEntities db = new RelmonStoreEntities();

        //
        // GET: /User/

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
        public ActionResult _SelectAjaxEditing()
        {
            return binding();
        }

        //
        // Ajax delete binding
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteAjaxEditing(int id)
        {
            delete(id);
            return binding();
        }

        //select data user
        private ViewResult binding()
        {
            var result = from o in db.companies
                         join p in db.companies on o.parent equals p.id
                         orderby o.nama
                         select new company2
                         {
                             id = o.id,
                             nama = o.nama,
                             parent = o.parent,
                             deskripsi = o.deskripsi,
                             nama_parent = p.nama
                         };
            List<company2> temp = result.ToList();
            List<company> u = new List<company>();

            foreach (var item in temp)
            {
                company utemp = new company();
                utemp.id = item.id;
                utemp.nama = item.nama;
                utemp.parent = item.parent;
                utemp.deskripsi = item.deskripsi;
                utemp.nama_parent = item.nama_parent;
                u.Add(utemp);
            }

            return View(new GridModel<company>
            {
                Data = u
            });
        }

        //insert data user
        [HttpPost]
        public void create(company company)
        {
            db.companies.Add(company);
            db.SaveChanges();
        }

        //update data user
        [HttpPost]
        public void update(company company)
        {
            company c = db.companies.Find(company.id);
            c.nama = company.nama;
            c.parent = company.parent;
            c.deskripsi = company.deskripsi;
            db.Entry(c).State = EntityState.Modified;
            db.SaveChanges();
        }

        //delete data user
        private void delete(int id)
        {
            company c = db.companies.Find(id);
            db.companies.Remove(c);
            db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult updateCompany()
        {
            List<object> items = new List<object>();
            items.Add(new 
            {
                id = "",
                nama = "-- Pilih Parent --"
            });
            
            IEnumerable<object> itemsResult = items.Concat(db.companies
                .AsEnumerable()
                .Where(c => c.id >= 0)
                .OrderBy(c => c.nama)
                .Select(c => new 
                {
                    id = c.id.ToString(),
                    nama = c.nama
                })
                .ToList()
            );

            return Json(itemsResult.ToList());
        }

        public JsonResult addCompany()
        {
            return Json(true);
        }

        public JsonResult GetDetail(int id)
        {
            company c = db.companies.Find(id);
            return Json(new
            {
                id = c.id,
                nama = c.nama,
                parent = c.parent.ToString() ?? "",
                deskripsi = c.deskripsi
            });
        }
    }
}