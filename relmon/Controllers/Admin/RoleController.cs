using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using relmon.Models;
using Telerik.Web.Mvc;
using System.Text;

namespace relmon.Controllers.Admin
{
    [Authorize]
    public class RoleController : Controller
    {
        private RelmonStoreEntities db = new RelmonStoreEntities();

        //
        // GET: /Role/

        public ActionResult Index()
        {
            List<SelectListItem> items_company = new List<SelectListItem>();
            items_company.Add(new SelectListItem
            {
                Value = "",
                Text = "-- Pilih Perusahaan --"
            });
            IEnumerable<SelectListItem> itemsResult_company = items_company.Concat(db.companies
                .AsEnumerable()
                .OrderBy(c => c.parent)
                .Select(c => new SelectListItem
                {
                    Value = c.id.ToString(),
                    Text = c.nama
                })
            );
            ViewBag.company = itemsResult_company;
            ViewBag.edit_company = itemsResult_company;

            List<SelectListItem> items_parent = new List<SelectListItem>();
            items_parent.Add(new SelectListItem
            {
                Value = "",
                Text = "-- Pilih Parent --"
            });
            //IEnumerable<SelectListItem> itemsResult_parent = items_parent.Concat(db.users_jabatan
            //    .AsEnumerable()
            //    .Where(uj => (uj.id >= 0) || (uj.id == 0))
            //    .OrderBy(uj => uj.parent)
            //    .Select(uj => new SelectListItem
            //    {
            //        Value = uj.id.ToString(),
            //        Text = uj.nama
            //    })
            //);
            ViewBag.parent = items_parent;
            ViewBag.edit_parent = items_parent;

            List<SelectListItem> items_cost_centre = new List<SelectListItem>();
            items_cost_centre.Add(new SelectListItem
            {
                Value = "",
                Text = "-- Pilih Cost Centre --"
            });
            //IEnumerable<SelectListItem> itemsResult_cost_centre = items_cost_centre.Concat(db.users_jabatan
            //    .AsEnumerable()
            //    .Where(uj => (uj.id >= 0) || (uj.id == 0))
            //    .OrderBy(uj => uj.parent)
            //    .Select(uj => new SelectListItem
            //    {
            //        Value = uj.id.ToString(),
            //        Text = uj.nama
            //    })
            //);
            ViewBag.cost_centre = items_cost_centre;
            ViewBag.edit_cost_centre = items_cost_centre;

            return PartialView(db.users_jabatan.ToList());
        }

        //
        // Ajax select binding
        [GridAction]
        public ActionResult _SelectAjaxEditing()
        {
            return binding();
        }

        private ViewResult _RoleHierarchyAjax(int? parentID)
        {
            return View(new GridModel<users_jabatan>
            {
                Data = db.users_jabatan
                .Where(o => o.parent == parentID)
                .ToList()
            });
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

        //select data role
        private ViewResult binding()
        {
            var result = from o in db.users_jabatan
                         join e in db.users_jabatan on o.parent equals e.id
                         join f in db.companies on o.company_id equals f.id
                         where o.id > 0
                         orderby o.parent
                         select new users_jabatan_parent
                         {
                             id = o.id,
                             deskripsi = o.deskripsi,
                             nama = o.nama,
                             parent = o.parent,
                             golongan = o.golongan,
                             cost_centre = o.cost_centre,
                             nama_parent = e.nama,
                             company2 = new company2
                             {
                                 nama = f.nama
                             }
                         };
            List<users_jabatan_parent> temp = result.ToList();
            List<users_jabatan> uj = new List<users_jabatan>();

            foreach (var item in temp)
            {
                users_jabatan ujtemp = new users_jabatan();
                ujtemp.id = item.id;
                ujtemp.nama = item.nama;
                ujtemp.parent = item.parent;
                ujtemp.nama_parent = item.nama_parent;
                ujtemp.deskripsi = item.deskripsi;
                ujtemp.golongan = item.golongan;
                ujtemp.cost_centre = item.cost_centre;
                //ujtemp.nama_cost_centre = item.nama_cost_centre;
                ujtemp.nama_cost_centre = (db.users_jabatan.Find(item.cost_centre) != null)?db.users_jabatan.Find(item.cost_centre).nama:null;
                ujtemp.company2 = item.company2;
                ujtemp.company2.nama= item.company2.nama;
                uj.Add(ujtemp);
            }

            return View(new GridModel<users_jabatan>
            {
                Data = uj
            });
        }

        public JsonResult RoleCompany(int companyId)
        {
            var roleCompany = db.users_jabatan.Where(uj => (uj.company_id == companyId) || (uj.id == 0)).ToList();
            return Json(roleCompany);
        }

        //insert data role
        [HttpPost]
        public void create(users_jabatan jabatan)
        {
            db.users_jabatan.Add(jabatan);
            db.SaveChanges();
        }

        //update data role
        [HttpPost]
        public void update(users_jabatan jabatan)
        {
            users_jabatan uj = db.users_jabatan.Find(jabatan.id);
            uj.nama = jabatan.nama;
            uj.parent = jabatan.parent;
            uj.deskripsi = jabatan.deskripsi;
            uj.golongan = jabatan.golongan;
            uj.cost_centre = jabatan.cost_centre;
            uj.company_id = jabatan.company_id;
            db.Entry(uj).State = EntityState.Modified;
            db.SaveChanges();
        }

        //delete data role
        private void delete(int id)
        {
            users_jabatan uj = db.users_jabatan.Find(id);
            db.users_jabatan.Remove(uj);
            db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public JsonResult addRole()
        {
            return Json(true);
        }

        public JsonResult GetDetail(int id)
        {
            users_jabatan uj = db.users_jabatan.Find(id);
            return Json(new
                {
                    id = uj.id,
                    nama = uj.nama,
                    parent = uj.parent.ToString() ?? "",
                    deskripsi = uj.deskripsi,
                    golongan = uj.golongan,
                    cost_centre = uj.cost_centre.ToString() ?? "",
                    company = uj.company_id.ToString() ?? ""
                });
        }
    }
}