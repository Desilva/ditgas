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
            Session["company_view"] = id;
            return PartialView();
        }

        public override int getCompanyId()
        {
            return Int32.Parse(Session["company_view"].ToString());
        }

        #region Profil
        public ActionResult Profil(int id)
        {
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
        public bool UploadProfil(string profil)
        {
            int getId = this.getCompanyId();
            var result = (from x in db.bisnis_main
                          where x.company_id == getId
                          select x
                        ).ToList();

            if (result.Count == 0)
            {
                bisnis_main items = new bisnis_main();
                items.company_id = getId;
                items.profile = HttpUtility.UrlDecode(profil);

                db.bisnis_main.Add(items);
            }
            else
            {
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
        public bool UploadStruktur()
        {
            var filename = Request["filename"];
            int getId = this.getCompanyId();
            var result = (from x in db.bisnis_main
                          where x.company_id == getId
                          select x
                         ).ToList();

            if (result.Count == 0)
            {
                bisnis_main items = new bisnis_main();
                items.company_id = getId;
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

        public bool DeleteStruktur()
        {
            int getId = this.getCompanyId();
            var result = (from x in db.bisnis_main
                          where x.company_id == getId
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
        #endregion

        #region visi misi
        public ActionResult RencanaKerja(int id)
        {
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
        public bool UploadRencanaKerja()
        {
            var filename = Request["filename"];
            int getId = this.getCompanyId();
            var result = (from x in db.bisnis_main
                          where x.company_id == getId
                          select x
                         ).ToList();

            if (result.Count == 0)
            {
                bisnis_main items = new bisnis_main();
                items.company_id = getId;
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

        public bool DeleteRencanaKerja()
        {
            int getId = this.getCompanyId();
            var result = (from x in db.bisnis_main
                          where x.company_id == getId
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


        #region inherited view
        //public ActionResult AdArt(int id)
        //{
        //    ViewBag.id = id;
        //    return AdArt();
        //}

        //public ActionResult Rkap(int id)
        //{
        //    ViewBag.id = id;
        //    return Rkap();
        //}

        //public ActionResult Rjpp(int id)
        //{
        //    ViewBag.id = id;
        //    return Rjpp();
        //}

        //public ActionResult BussinessReport(int id)
        //{

        //    ViewBag.id = id;
        //    return BussinessReport();
        //}

        //public ActionResult Kinerja(int id)
        //{
        //    ViewBag.id = id;
        //    return Kinerja();
        //}
        #endregion


        [HttpPost]
        public JsonResult GetCompanyList()
        {
            string find = Request["parent"];
            int parent = Int32.Parse(find);
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
                listResult.Add(new
                {
                    id = result.id,
                    member = result.nama
                });
            }

            return Json(listResult);
        }


        //public ActionResult StrukturOrganisasi(int id)
        //{

        //    switch (id)
        //    {
        //        case 0:
        //            ViewBag.content = "../../Content/data/struktur-org/ap/" + id + "/struktur.png";
        //            break;
        //        case 1:
        //            ViewBag.content = "../../Content/data/struktur-org/ap/" + id + "/struktur.png";
        //            break;
        //        case 2:
        //            ViewBag.content = "../../Content/data/struktur-org/ap/" + id + "/struktur.png";
        //            break;
        //        case 3:
        //            ViewBag.content = "../../Content/data/struktur-org/ap/" + id + "/struktur.png";
        //            break;
        //        case 4:
        //            ViewBag.content = "../../Content/data/struktur-org/ap/" + id + "/struktur.png";
        //            break;
        //    }
        //    return PartialView();
        //}


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