using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text.pdf;
using relmon.Utilities;
using System.IO;
using relmon.Models;
using System.Data;
using Telerik.Web.Mvc;
using relmon.Controllers.Utilities;

namespace relmon.Controllers.Admin
{
    public class DataBisnisDitGasAdminController : Controller
    {
        protected RelmonStoreEntities db = new RelmonStoreEntities();
       
        //
        // GET: /DataBisnisDitGasAdmin/
        public virtual ActionResult Index()
        {
            ViewBag.selectedMenu = "databisnis";
            ViewBag.id = 0;
            return PartialView();
        }

        # region AdArt
        public ActionResult AdArt(int id)
        {
            ViewBag.id = id;
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();
            if (result.Count == 0)
            {
                bisnis_main e = new bisnis_main();
                e.ad_art = "";
                e.company_id = id;
                return PartialView(e);
            }
            else
            {
                bisnis_main e = result.First() ;
                return PartialView(e);
            
            }
           
        }

        [HttpPost]
        public bool UploadAdArt(int id, string filename)
        {
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();

            if (result.Count == 0)
            {
                bisnis_main items = new bisnis_main();
                items.company_id = id;
                items.ad_art = filename;
                db.bisnis_main.Add(items);

            }
            else
            {
                bisnis_main items = result.First();
                items.ad_art = filename;
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

        public bool DeleteAdArt(int id)
        {
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();

            if (result.Count != 0)
            {
                bisnis_main items = result.First();
                items.ad_art = "";
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

        # region Rjpp
        public ActionResult Rjpp(int id)
        {
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();
            if (result.Count == 0)
            {
                bisnis_main e = new bisnis_main();
                e.rjpp = "";
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
        public bool UploadRjpp(int id, string rjpp)
        {
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                        ).ToList();

            if (result.Count == 0)
            {
                bisnis_main items = new bisnis_main();
                items.company_id = id;
                items.rjpp = HttpUtility.UrlDecode(rjpp);

                db.bisnis_main.Add(items);
            }
            else
            {
                bisnis_main items = result.First();
                items.rjpp = HttpUtility.UrlDecode(rjpp);
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

        # region Rkap
        public ActionResult Rkap(int id)
        {
            ViewBag.id = id;
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();
            if (result.Count == 0)
            {
                bisnis_main e = new bisnis_main();
                e.rkap = "";
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
        public bool UploadRkap(int id,string filename)
        {
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();

            if (result.Count == 0)
            {
                bisnis_main items = new bisnis_main();
                items.company_id = id;
                items.rkap = filename;
                db.bisnis_main.Add(items);

            }
            else
            {
                bisnis_main items = result.First();
                items.rkap = filename;
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

        public bool DeleteRkap(int id)
        {
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();

            if (result.Count != 0)
            {
                bisnis_main items = result.First();
                items.rkap = "";
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

        # region Bussiness Report
        public ActionResult BussinessReport()
        {
            return PartialView();
        }

        [HttpPost]
        public string UploadBussinessReport(bisnis_bussiness_report bisnis_bussiness_report)
        {
            var result = (from x in db.bisnis_bussiness_report
                          where x.company_id == bisnis_bussiness_report.company_id && x.bulan.Equals(bisnis_bussiness_report.bulan) && x.tahun == bisnis_bussiness_report.tahun
                          select x
                        ).ToList();

            if (result.Count == 0)
            {
                db.bisnis_bussiness_report.Add(bisnis_bussiness_report);
                try
                {
                    db.SaveChanges();
                    return "true";
                }
                catch (Exception a)
                {
                    Console.Write(a.Message);
                    return "false";
                }
            }
            else
            {
                return "exist";
            }

        }

        [HttpPost]
        public string UpdateBussinessReport(bisnis_bussiness_report bisnis_bussiness_report)
        {
            var result = (from x in db.bisnis_bussiness_report
                          where x.id == bisnis_bussiness_report.id
                          select x
                        ).ToList();
            bisnis_bussiness_report items = result.First();
            items.tahun = bisnis_bussiness_report.tahun;
            items.bulan = bisnis_bussiness_report.bulan;
            items.deskripsi = bisnis_bussiness_report.deskripsi;
            items.content = bisnis_bussiness_report.content;
            db.Entry(items).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                return "true";
            }
            catch (Exception a)
            {
                Console.Write(a.Message);
                return "false";
            }
        }
        

        [GridAction]
        public ActionResult _SelectBisnisReport(int id)
        {
            return binding(id);
        }

        //
        // Ajax delete binding
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteBisnisReport(int id) //row_id
        {
            //delete dr database
            var tempResult = (from x in db.bisnis_bussiness_report
                              where x.id == id
                              select x).ToList();
            var comp_id = -1;
            if (tempResult.Count != 0)
            {
                bisnis_bussiness_report result = tempResult.First();
                comp_id = result.company_id;
                //delete file
                UploadController upload = new UploadController();
                string[] file = new string[1];
                file[0] = result.content;
                if(!string.IsNullOrWhiteSpace(file[0])){
                    upload.Remove(file, "Data Bisnis\\" + result.company_id + "\\Bussiness Report\\" + result.tahun + "\\" + result.bulan);
                }
                
                db.bisnis_bussiness_report.Remove(result);
                db.SaveChanges();
            }

            return binding(comp_id);
        }

        //select data user
        protected ViewResult binding(int id)
        {
            List<bisnis_bussiness_report> result = (from x in db.bisnis_bussiness_report
                                                    where x.company_id == id
                                                    select x).ToList();

            return View(new GridModel<bisnis_bussiness_report>
            {
                Data = result
            });
        }


        public JsonResult GetReport(int id)
        {
            bisnis_bussiness_report x = db.bisnis_bussiness_report.Find(id);
            return Json(new
            {
                id = x.id,
                company_id = x.company_id,
                tahun = x.tahun,
                bulan = x.bulan,
                content = x.content,
                deskripsi = x.deskripsi
            });
        }
        # endregion

        # region KPI
        public ActionResult Kinerja()
        {
            return PartialView();
        }

        [HttpPost]
        public string UploadKinerja(bisnis_kpi bisnis_kpi)
        {
            var result = (from x in db.bisnis_kpi
                          where x.company_id == bisnis_kpi.id && x.tahun == bisnis_kpi.tahun
                          select x
                        ).ToList();

            if (result.Count == 0)
            {
                db.bisnis_kpi.Add(bisnis_kpi);
                try
                {
                    db.SaveChanges();
                    return "true";
                }
                catch (Exception a)
                {
                    Console.Write(a.Message);
                    return "false";
                }
            }
            else
            {
                return "exist";
            }

        }

        [HttpPost]
        public string UpdateKinerja(bisnis_kpi bisnis_kpi)
        {
            var result = (from x in db.bisnis_kpi
                          where x.id == bisnis_kpi.id
                          select x
                        ).ToList();
            bisnis_kpi items = result.First();
            items.tahun = bisnis_kpi.tahun;
            items.deskripsi = bisnis_kpi.deskripsi;
            items.content = bisnis_kpi.content;
            db.Entry(items).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                return "true";
            }
            catch (Exception a)
            {
                Console.Write(a.Message);
                return "false";
            }
        }
        

        [GridAction]
        public ActionResult _SelectKinerja(int id)
        {
            return bindingkinerja(id);
        }

        //
        // Ajax delete binding
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteKinerja(int id) //row_id
        {
            //string tipe = delete(id);
            var tempResult = (from x in db.bisnis_kpi
                              where x.id == id
                              select x).ToList();
            var comp_id = -1;
            if (tempResult.Count != 0)
            {
                bisnis_kpi result = tempResult.First();
                comp_id = result.company_id;
                UploadController upload = new UploadController();
                
                string[] file = new string[1];
                file[0] = result.content;
                if (!string.IsNullOrWhiteSpace(file[0]))
                {
                    upload.Remove(file, "Data Bisnis\\" + result.company_id + "\\Kinerja\\" + result.tahun);
                }

                db.bisnis_kpi.Remove(result);
                db.SaveChanges();
            }




            return bindingkinerja(comp_id);
        }

        //select data user
        protected ViewResult bindingkinerja(int id)
        {
            List<bisnis_kpi> result = (from x in db.bisnis_kpi
                                       where x.company_id == id
                                       select x).ToList();

            return View(new GridModel<bisnis_kpi>
            {
                Data = result
            });
        }


        public JsonResult GetKinerja(int id)
        {
            bisnis_kpi x = db.bisnis_kpi.Find(id);
            return Json(new
            {
                id = x.id,
                company_id = x.company_id,
                tahun = x.tahun,
                content = x.content,
                deskripsi = x.deskripsi
            });
        }
        #endregion

        //public ActionResult RencanaKerja()
        //{
        //    return PartialView();
        //}

        //public ActionResult ProjectStatus()
        //{
        //    return PartialView();
        //}

        //public ActionResult Product()
        //{
        //    return PartialView();
        //}

        //public string getCompanyName(int id)
        //{
        //    var result = (from x in db.companies
        //                  where x.id == id
        //                  select x
        //                 ).ToList();
        //    return result.First().nama;
        //}
    }
}
