using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using relmon.Utilities;
using iTextSharp.text.pdf;
using relmon.Models;
using System.Data;
using Telerik.Web.Mvc;

namespace relmon.Controllers.Admin
{
    public class DataBisnisApAdminController : DataBisnisDitGasAdminController
    {
        //
        // GET: /DataBisnisApAdmin/

        public override ActionResult Index()
        {
            ViewBag.selectedMenu = "databisnis";
            return PartialView();
        }

        public ActionResult Connector(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }

        #region Profil
        public ActionResult Profil(int id)
        {
            ViewBag.id = id;
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();
            if (result.Count == 0)
            {
                bisnis_main e = new bisnis_main();
                e.profile = "";
                e.company_id = id;
                return PartialView(e);
            }
            else
            {
                bisnis_main e = result.First();
                return PartialView(e);

            }
        }

        [HttpPost]
        public bool UploadProfil(int id, string profil)
        {
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                        ).ToList();

            if (result.Count == 0)
            {
                int aclId = (int)Session["id"];
                int category = DataBisnisDitGasAdminController.GetCategory(id);

                if (category == 1)
                {
                    if (!ACLHandler.isUserAllowedTo(PageItem.DataBisnis_Ap_Profil.name, aclId, "create"))
                    {
                        return false;
                    }
                }
                else if (category == 2)
                {
                    if (!ACLHandler.isUserAllowedTo(PageItem.DataBisnis_Afiliasi_Profil.name + id, aclId, "create"))
                    {
                        return false;
                    }
                }
                
                bisnis_main items = new bisnis_main();
                items.company_id = id;
                items.profile = HttpUtility.UrlDecode(profil);

                db.bisnis_main.Add(items);
            }
            else
            {
                int aclId = (int)Session["id"];
                int category = DataBisnisDitGasAdminController.GetCategory(id);

                if (category == 1)
                {
                    if (!ACLHandler.isUserAllowedTo(PageItem.DataBisnis_Ap_Profil.name, aclId, "update"))
                    {
                        return false;
                    }
                }
                else if (category == 2)
                {
                    if (!ACLHandler.isUserAllowedTo(PageItem.DataBisnis_Afiliasi_Profil.name + id, aclId, "update"))
                    {
                        return false;
                    }
                }

                bisnis_main items = result.First();
                items.profile = HttpUtility.UrlDecode(profil);
                db.Entry(items).State = EntityState.Modified;
            }
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception a)
            {
                Console.Write(a.Message);
                return false;
            }
        }

        #endregion

        #region struktur
        public ActionResult StrukturOrganisasi(int id)
        {
            ViewBag.id = id;
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();
            if (result.Count == 0)
            {
                bisnis_main e = new bisnis_main();
                e.struktur = "";
                e.company_id = id;
                return PartialView(e);
            }
            else
            {
                bisnis_main e = result.First();
                return PartialView(e);
            }

        }

        [HttpPost]
        public bool UploadStruktur(int id,string filename)
        {
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();

            if (result.Count == 0)
            {
                bisnis_main items = new bisnis_main();
                items.company_id = id;
                items.struktur = filename;
                db.bisnis_main.Add(items);

            }
            else
            {
                bisnis_main items = result.First();
                items.struktur = filename;
                db.Entry(items).State = EntityState.Modified;
            }
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception a)
            {
                Console.Write(a.Message);
                return false;
            }
        }

        public bool DeleteStruktur(int id)
        {
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();

            if (result.Count != 0)
            {
                bisnis_main items = result.First();
                items.struktur = "";
                db.Entry(items).State = EntityState.Modified;
            }
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception a)
            {
                Console.Write(a.Message);
                return false;
            }
        }

        public string GetStruktur(int id)
        {
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();
            var get = result.First();
            return "../../upload/Data Bisnis/" + id + "/struktur organisasi/" + get.struktur;
        }
        #endregion

        #region visi misi
        public ActionResult RencanaKerja(int id)
        {
            ViewBag.id = id;
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();
            if (result.Count == 0)
            {
                bisnis_main e = new bisnis_main();
                e.visimisi = "";
                e.company_id = id;
                return PartialView(e);
            }
            else
            {
                bisnis_main e = result.First();
                return PartialView(e);
            }

        }

        [HttpPost]
        public bool UploadRencanaKerja(int id,string filename)
        {
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();

            if (result.Count == 0)
            {
                bisnis_main items = new bisnis_main();
                items.company_id = id;
                items.visimisi = filename;
                db.bisnis_main.Add(items);

            }
            else
            {
                bisnis_main items = result.First();
                items.visimisi = filename;
                db.Entry(items).State = EntityState.Modified;
            }
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception a)
            {
                Console.Write(a.Message);
                return false;
            }
        }

        public bool DeleteRencanaKerja(int id)
        {
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();

            if (result.Count != 0)
            {
                bisnis_main items = result.First();
                items.visimisi = "";
                db.Entry(items).State = EntityState.Modified;
            }
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception a)
            {
                Console.Write(a.Message);
                return false;
            }
        }
        #endregion


        [HttpPost]
        public JsonResult GetCompanyList(int parent, bool wcheck)
        {
            //int parentInt = Int32.Parse(parent);
            var listResultTemp = (from x in db.companies
                                  where (x.parent == parent)
                                  select new
                                  {
                                      id = x.id,
                                      nama = x.nama
                                  }).ToList();

            List<object> listResult = new List<object>();
            foreach (var result in listResultTemp)
            {

                if (wcheck)
                {
                    if (checkACL(result.nama))
                    {
                        listResult.Add(new
                        {
                            id = result.id,
                            member = result.nama
                        });
                    }
                }
                else
                {
                    listResult.Add(new
                    {
                        id = result.id,
                        member = result.nama
                    });
                }
            }

            return Json(listResult);
        }

        public virtual bool checkACL(string nama){
            return ACLHandler.isUserAllowedTo(PageItem.DataBisnis_ApCompany.name +nama,(int) Session["id"], "view");
        }

        public ActionResult ProjectStatus(int id)
        {

            ViewBag.id = id;
            ViewBag.exist = false;
            string filePath = Request.MapPath(Url.Content("~/Content/data/project-status/ap/" + id + "/project-status.pdf"));
            if (System.IO.File.Exists(filePath))
            {
                ViewBag.exist = true;
            }
            return PartialView();
        }

        public ActionResult Product(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }

    }
}