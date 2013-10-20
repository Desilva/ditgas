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
using Telerik.Web.Mvc.UI.Fluent;

namespace relmon.Controllers.Admin
{
    [Authorize]
    public class UserController : Controller
    {
        private RelmonStoreEntities db = new RelmonStoreEntities();

        //
        // GET: /User/

        public ActionResult Index()
        {
            //IEnumerable<SelectListItem> items = db.users
            //    .AsEnumerable()
            //    .Where(u => u.username is null)
            //    .OrderBy(u => u.fullname)
            //    .Select(u => new SelectListItem
            //    {
            //        Value = u.id.ToString(),
            //        Text = u.fullname+", "+u.users_jabatan_parent.company.nama+", "+u.users_jabatan_parent.nama
            //    });
            //IEnumerable<SelectListItem> items = (from o in db.users
            //                                     where string.IsNullOrEmpty(o.username)
            //                                     orderby o.fullname
            //                                     select o)
            //            .AsEnumerable()
            //            .Select(u => new SelectListItem
            //            {
            //                Value = u.id.ToString(),
            //                Text = u.fullname + ", " + u.users_jabatan_parent.company.nama + ", " + u.users_jabatan_parent.nama
            //            });
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
            ACLContainer aclContainer = new ACLContainer();
            ViewBag.actionTreeViewItemFactory = new Action<TreeViewItemFactory>(item =>
                buildTreeViewItemFactory(item, aclContainer.pageList));

            return PartialView(db.users.ToList());
        }

        private TreeViewItemFactory buildTreeViewItemFactory(TreeViewItemFactory item, PageTree pageTree, string parentIdStr="")
        {
            string checkView = (pageTree.defaultView ? "checked='checked'" : ""); 
            string checkCreate = (pageTree.defaultCreate ? "checked='checked'" : "");
            string checkUpdate = (pageTree.defaultUpdate ? "checked='checked'" : "");
            string checkDelete = (pageTree.defaultDelete ? "checked='checked'" : "");
            if (pageTree.isUpload)
            {
                item.Add()
                    .Expanded(parentIdStr == "")
                    .Encoded(false)
                    .Text("<table class='acl_table'><tr><td class='acl_title'>" + pageTree.pageItem.text + "</td><td class='acl_checkbox'>"
                    + "<input type='checkbox' " + (pageTree.enableView ? "" : "disabled='disabled' " + checkView) + "name='" + pageTree.pageItem.name + "' value='view' id='ACL-view" + parentIdStr + "-" + pageTree.pageItem.name + "'>" + "View "
                    + "<input type='checkbox' " + (pageTree.enableUpdate ? "" : "disabled='disabled' " + checkUpdate) + "name='" + pageTree.pageItem.name + "' value='update' id='ACL-update" + parentIdStr + "-" + pageTree.pageItem.name + "'>" + "Upload "
                    + "</td></tr></table>"
                    )
                    .Value(pageTree.pageItem.name)
                    .Items(subItem =>
                    {
                        foreach (PageTree pageTreeChild in pageTree.child)
                        {
                            buildTreeViewItemFactory(subItem, pageTreeChild, parentIdStr + "-" + pageTree.pageItem.name);
                        }
                    });
            }
            else
            {
                item.Add()
                    .Expanded(parentIdStr == "")
                    .Encoded(false)
                    .Text("<table class='acl_table'><tr><td class='acl_title'>" + pageTree.pageItem.text + "</td><td class='acl_checkbox'>"
                    + "<input type='checkbox' " + (pageTree.enableView ? "" : "disabled='disabled' " + checkView) + "name='" + pageTree.pageItem.name + "' value='view' id='ACL-view" + parentIdStr + "-" + pageTree.pageItem.name + "'>" + "View "
                    + "<input type='checkbox' " + (pageTree.enableCreate ? "" : "disabled='disabled' " + checkCreate) + "name='" + pageTree.pageItem.name + "' value='create' id='ACL-create" + parentIdStr + "-" + pageTree.pageItem.name + "'>" + "Create "
                    + "<input type='checkbox' " + (pageTree.enableUpdate ? "" : "disabled='disabled' " + checkUpdate) + "name='" + pageTree.pageItem.name + "' value='update' id='ACL-update" + parentIdStr + "-" + pageTree.pageItem.name + "'>" + "Update "
                    + "<input type='checkbox' " + (pageTree.enableDelete ? "" : "disabled='disabled' " + checkDelete) + "name='" + pageTree.pageItem.name + "' value='delete' id='ACL-delete" + parentIdStr + "-" + pageTree.pageItem.name + "'>" + "Delete "
                    //+ "<input type='checkbox' " + (pageTree.enablePrint ? "" : "disabled='disabled' checked='checked'") + "name='" + pageTree.pageItem.name + "' value='print' id='ACL-print" + parentIdStr + "-" + pageTree.pageItem.name + "'>" + "Print "
                    + "</td></tr></table>"
                    )
                    .Value(pageTree.pageItem.name)
                    .Items(subItem =>
                    {
                        foreach (PageTree pageTreeChild in pageTree.child)
                        {
                            buildTreeViewItemFactory(subItem, pageTreeChild, parentIdStr + "-" + pageTree.pageItem.name);
                        }
                    });
            }
            return item;
            //if (objs.Count() == 3)
            //{
            //    foreach (object obj in objs[2])
            //    {
            //        object[] element = (object[])obj;
            //        if (element.Count() == 3)
            //        {
            //            item.Add()
            //                .Encoded(false)
            //                .Text(element[0].ToString()
            //                + "<span id='acl_checkbox'>"
            //                + "<input type='checkbox' name='" + element[1].ToString() + "' value='view' id='ACL-view" + parentIdStr + "-" + element[1].ToString() + "'>" + "View "
            //                + "<input type='checkbox' name='" + element[1].ToString() + "' value='create' id='ACL-create" + parentIdStr + "-" + element[1].ToString() + "'>" + "Create "
            //                + "<input type='checkbox' name='" + element[1].ToString() + "' value='update' id='ACL-update" + parentIdStr + "-" + element[1].ToString() + "'>" + "Update "
            //                + "<input type='checkbox' name='" + element[1].ToString() + "' value='delete' id='ACL-delete" + parentIdStr + "-" + element[1].ToString() + "'>" + "Delete "
            //                + "<input type='checkbox' name='" + element[1].ToString() + "' value='print' id='ACL-print" + parentIdStr + "-" + element[1].ToString() + "'>" + "Print "
            //                + "</span>"
            //                )
            //                .Value(element[1].ToString())
            //                .Items(subItem =>
            //                {
            //                    buildTreeViewItemFactory(subItem, element, parentIdStr + "-" + element[1].ToString());
            //                });
            //        }
            //    }
            //}
            //return item;
        }

        //private TreeViewItemFactory buildTreeViewItemFactory(TreeViewItemFactory item, object[,] objs)
        //{
        //    bool isName = true;
        //    string name = "";
        //    foreach (object obj in objs)
        //    {
        //        if (isName) // obj = string
        //        {
        //            name = obj.ToString();
        //            isName = false;
        //        }
        //        else // obj = object[,]
        //        {
        //            item.Add().Text(name)
        //            .Items(subItem =>
        //            {
        //                buildTreeViewItemFactory(subItem, (object[,]) obj);
        //            });
        //            isName = true;
        //        }
        //    }
        //    return item;
        //}

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
        public void create(user user)
        {
            user u = db.users.Find(user.id);
            u.username = user.username;
            u.password = EncodePassword(user.password);
            db.Entry(u).State = EntityState.Modified;
            db.SaveChanges();
        }

        //update data user
        [HttpPost]
        public void update(user user)
        {
            user u = db.users.Find(user.id);
            u.username = user.username;
            if(user.password != null){
                
                u.password = EncodePassword(user.password);
            }
            db.Entry(u).State = EntityState.Modified;
            db.SaveChanges();
        }

        //delete data user
        private void delete(int id)
        {
            user user = db.users.Find(id);

            user.username = null;
            user.password = null;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public JsonResult addUser()
        {
            return Json(true);
        }

        public JsonResult GetDetail(int id)
        {
            user u = db.users.Find(id);
            return Json(new
            {
                id = u.id,
                username = u.username,
                fullname = u.fullname,
                rm_role = u.rm_role
            });
        }

        private string EncodePassword(string originalPassword)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes).Replace("-", "").ToLower();
        }
    }
}