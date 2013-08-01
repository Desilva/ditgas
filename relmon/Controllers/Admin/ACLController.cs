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
using System.Web.Script.Serialization;

namespace relmon.Controllers.Admin
{
    [Authorize]
    public class ACLController : Controller
    {
        private RelmonStoreEntities db = new RelmonStoreEntities();

        //
        // GET: /User/

        public ActionResult Index()
        {
            var items = (from o in db.users
                         join p in db.users_jabatan on o.rm_role equals p.id
                         join q in db.companies on p.company_id equals q.id
                         where o.username == null
                         orderby o.fullname
                         select new {o, p, q})
                        .AsEnumerable()
                        .Select(u => new SelectListItem
                        {
                            Value = u.o.id.ToString(),
                            Text = u.o.fullname + ", " + u.q.nama+ ", " + u.p.nama
                        });
            
            ViewBag.employee = items;
            ViewBag.edit_employee = items;

            return PartialView(db.users.ToList());
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
            var result = from o in db.users
                         join e in db.users_jabatan on o.rm_role equals e.id
                         join f in db.companies on e.company_id equals f.id
                         where o.username != null
                         where o.rm_role >= 0
                         orderby o.fullname
                         select new user_rm_role
                         {
                             id = o.id,
                             username = o.username,
                             fullname = o.fullname,
                             rm_role = o.rm_role,
                             users_jabatan_parent = new users_jabatan_parent
                             {
                                 id = e.id,
                                 nama = e.nama,
                                 parent = e.parent,
                                 deskripsi = e.deskripsi,
                                 company2 = new company2
                                 {
                                     nama = f.nama
                                 }
                             }
                         };
            List<user_rm_role> temp = result.ToList();
            List<user> u = new List<user>();

            foreach (var item in temp)
            {
                user utemp = new user();
                utemp.id = item.id;
                utemp.username = item.username;
                utemp.fullname = item.fullname;
                utemp.rm_role = item.rm_role;
                utemp.users_jabatan_parent = item.users_jabatan_parent;
                utemp.users_jabatan_parent.company2 = item.users_jabatan_parent.company2;
                u.Add(utemp);
            }

            return View(new GridModel<user>
            {
                Data = u
            });
        }

        //insert data user
        [HttpPost]
        public void create(acl ACL)
        {
            db.acls.Add(ACL);
            db.SaveChanges();
        }

        //update data user
        [HttpPost]
        public void update(acl ACL)
        {
            acl a = db.acls.Where(x => (x.user_id == ACL.user_id) && (x.page_name == ACL.page_name)).First();
            a.allow_view = ACL.allow_view;
            a.allow_create = ACL.allow_create;
            a.allow_update = ACL.allow_update;
            a.allow_delete = ACL.allow_delete;
            a.allow_print = ACL.allow_print;
            db.Entry(a).State = EntityState.Modified;
            db.SaveChanges();
        }

        //delete data user
        private void delete(int id)
        {
            acl a = db.acls.Find(id);
            db.acls.Remove(a);
            db.SaveChanges();
        }

        //delete data user
        private void deleteBatch(int userId)
        {
            List<acl> acls = db.acls.Where(a => a.user_id == userId).ToList();
            foreach (acl acl in acls)
            {
                db.acls.Remove(acl);
            }
            db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //update data user
        [HttpPost]
        public void updateBatch()
        {
            //ACLContainer aclContainer = new ACLContainer();
            //aclContainer.user_id = int.Parse(Request.Form["user_id"]);

            int userId = int.Parse(Request.Form["user_id"]);
            string[] aclDetails = Request.Form["acl"].Split('&');

            deleteBatch(userId); // hapus dulu semua
            foreach (string aclDetail in aclDetails)
            {
                string[] element = aclDetail.Split('=');

                if (element.Count() == 2)
                {
                    string pageName = Server.UrlDecode(element[0]);
                    string permission = element[1];
                    List<acl> checkACL = db.acls.Where(a => (a.user_id == userId) && (a.page_name == pageName)).ToList();

                    if (checkACL.Count() > 0) //update
                    {
                        acl currentACL = checkACL.First();
                        switch (permission)
                        {
                            case "view": currentACL.allow_view = 1; break;
                            case "create": currentACL.allow_create = 1; break;
                            case "update": currentACL.allow_update = 1; break;
                            case "delete": currentACL.allow_delete = 1; break;
                            case "print": currentACL.allow_print = 1; break;
                            default: break;
                        }
                        db.Entry(currentACL).State = EntityState.Modified;
                    }
                    else //create
                    {
                        acl currentACL = new acl();
                        currentACL.user_id = userId;
                        currentACL.page_name = pageName;
                        switch (permission)
                        {
                            case "view": currentACL.allow_view = 1; break;
                            case "create": currentACL.allow_create = 1; break;
                            case "update": currentACL.allow_update = 1; break;
                            case "delete": currentACL.allow_delete = 1; break;
                            case "print": currentACL.allow_print = 1; break;
                            default: break;
                        }
                        db.acls.Add(currentACL);
                    }

                    db.SaveChanges();
                }
            }
        }

        public JsonResult addACL()
        {
            return Json(true);
        }

        public JsonResult GetACL(int id)
        {
            return Json(new
                {
                    user_id = id,
                    acls = db.acls.Where(a => a.user_id == id).ToList()
                });
        }
    }
}