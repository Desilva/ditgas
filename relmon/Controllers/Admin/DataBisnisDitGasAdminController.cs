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
            ViewBag.id = id;
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
                int aclId = (int)Session["id"];
                int category = DataBisnisDitGasAdminController.GetCategory(id);
            
                if(category==0){
                    if (!ACLHandler.isUserAllowedTo(PageItem.DataBisnis_DitGas_RJPP.name, aclId, "create")){
                        return false;
                    }
                }else if(category==1){
                    if (!ACLHandler.isUserAllowedTo(PageItem.DataBisnis_Ap_RJPP.name + id, aclId, "create"))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!ACLHandler.isUserAllowedTo(PageItem.DataBisnis_Afiliasi_RJPP.name + id, aclId, "create"))
                    {
                        return false;
                    }
                }
            
            
                bisnis_main items = new bisnis_main();
                items.company_id = id;
                items.rjpp = HttpUtility.UrlDecode(rjpp);

                db.bisnis_main.Add(items);
            }
            else
            {
                int aclId = (int)Session["id"];
                int category = DataBisnisDitGasAdminController.GetCategory(id);

                if (category == 0)
                {
                    if (!ACLHandler.isUserAllowedTo(PageItem.DataBisnis_DitGas_RJPP.name, aclId, "create"))
                    {
                        return false;
                    }
                }
                else if (category == 1)
                {
                    if (!ACLHandler.isUserAllowedTo(PageItem.DataBisnis_Ap_RJPP.name + id, aclId, "create"))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!ACLHandler.isUserAllowedTo(PageItem.DataBisnis_Afiliasi_RJPP.name + id, aclId, "create"))
                    {
                        return false;
                    }
                }
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

        public ActionResult BussinessReportNavigator(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }

        public ActionResult BussinessReport(int id, int rowId, string reportType)
        {
            ViewBag.id = id;
            ViewBag.type = reportType;
            var result = (from x in db.bisnis_bussiness_report
                          where x.id == rowId
                          select x
                        ).ToList();
            if (result.Count == 0)
            {
                bisnis_bussiness_report e = new bisnis_bussiness_report();
                e.company_id = id;
                ViewBag.baru = 1;
                return PartialView(e);
                
            }
            else
            {
                bisnis_bussiness_report e = result.First();
                ViewBag.baru = 0;
                return PartialView(e);
            }
        }

        [HttpPost]
        public string UploadBussinessReport(bisnis_bussiness_report bisnis_bussiness_report)
        {
            var result = (from x in db.bisnis_bussiness_report
                          where x.company_id == bisnis_bussiness_report.company_id && x.bulan.Equals(bisnis_bussiness_report.bulan) && x.tahun == bisnis_bussiness_report.tahun && x.reportType == bisnis_bussiness_report.reportType
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
                          where x.company_id == bisnis_bussiness_report.company_id && x.tahun == bisnis_bussiness_report.tahun && x.bulan == bisnis_bussiness_report.bulan && x.reportType == bisnis_bussiness_report.reportType
                          select x
                        ).ToList();
            bisnis_bussiness_report items = result.First();
            items.tahun = bisnis_bussiness_report.tahun;
            items.bulan = bisnis_bussiness_report.bulan;
            items.deskripsi = bisnis_bussiness_report.deskripsi;
            items.content = bisnis_bussiness_report.content;
            items.reportType = bisnis_bussiness_report.reportType; 
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
        public ActionResult _SelectBisnisReport(int id,string reportType)
        {
            return binding(id,reportType);
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
            var reportType = "";
            if (tempResult.Count != 0)
            {
                bisnis_bussiness_report result = tempResult.First();
                comp_id = result.company_id;
                reportType = result.reportType;
                int aclId = (int)Session["id"];
                int category = DataBisnisDitGasAdminController.GetCategory(comp_id);

                if (category == 0)
                {
                    if (!ACLHandler.isUserAllowedTo(PageItem.DataBisnis_DitGas_BussinessReport.name, aclId, "delete"))
                    {
                        return binding(comp_id, reportType);
                    }
                }
                else if (category == 1)
                {
                    if (!ACLHandler.isUserAllowedTo(PageItem.DataBisnis_Ap_BussinessReport.name + comp_id, aclId, "delete"))
                    {
                        return binding(comp_id, reportType);
                    }
                }
                else if(category == 2)
                {
                    if (!ACLHandler.isUserAllowedTo(PageItem.DataBisnis_Afiliasi_BussinessReport.name + comp_id, aclId, "delete"))
                    {
                        return binding(comp_id, reportType);
                    }
                }

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

            return binding(comp_id,reportType);
        }

        //select data user
        protected ViewResult binding(int id, string reportType)
        {
            //List<bisnis_bussiness_report> result = (from x in db.bisnis_bussiness_report
            //                                        where x.company_id == id && x.reportType == reportType
            //                                        select x).ToList();

            List<bisnis_bussiness_report> result = db.bisnis_bussiness_report.Where(x => x.company_id == id).Where(x => x.reportType == reportType).ToList();

            List<BisnisBussinessReportContainer> temp = new List<BisnisBussinessReportContainer>();
            foreach (bisnis_bussiness_report b in result) {
                BisnisBussinessReportContainer x = new BisnisBussinessReportContainer
                {
                    id = b.id,
                    tahun = b.tahun,
                    deskripsi = b.deskripsi,
                    content = b.content,
                    company_id = b.company_id,
                    bulan = b.bulan,
                    reportType = b.reportType
                };
                temp.Add(x);
            }

            return View(new GridModel<BisnisBussinessReportContainer>
            {
                Data = temp
            });
        }


        public JsonResult GetReport(int id)
        {
            bisnis_bussiness_report x = db.bisnis_bussiness_report.Find(id);
            ViewBag.upload = x.content;
            return Json(new
            {
                id = x.id,
                company_id = x.company_id,
                tahun = x.tahun,
                bulan = x.bulan,
                content = x.content,
                deskripsi = x.deskripsi,
                reportType = x.reportType
            });
        }
        # endregion

        # region KPI
        public ActionResult Kinerja(int id, int rowId)
        {
            ViewBag.id = id;
            var result = (from x in db.bisnis_kpi
                          where x.id == rowId
                          select x
                        ).ToList();
            if (result.Count == 0)
            {
                bisnis_kpi e = new bisnis_kpi();
                e.company_id = id;
                ViewBag.baru = 1;
                return PartialView(e);

            }
            else
            {
                bisnis_kpi e = result.First();
                ViewBag.baru = 0;
                return PartialView(e);
            }
        }

        [HttpPost]
        public string UploadKinerja(bisnis_kpi bisnis_kpi)
        {
            var result = (from x in db.bisnis_kpi
                          where x.company_id == bisnis_kpi.company_id && x.tahun == bisnis_kpi.tahun
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
                          where x.company_id == bisnis_kpi.company_id && x.tahun == bisnis_kpi.tahun
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
                int aclId = (int)Session["id"];
                int category = DataBisnisDitGasAdminController.GetCategory(comp_id);
                if (category == 0)
                {
                    if (!ACLHandler.isUserAllowedTo(PageItem.DataBisnis_DitGas_KPI.name, aclId, "delete"))
                    {
                        return bindingkinerja(comp_id);
                    }
                }
                else if (category == 1)
                {
                    if (!ACLHandler.isUserAllowedTo(PageItem.DataBisnis_Ap_KPI.name + comp_id, aclId, "delete"))
                    {
                        return bindingkinerja(comp_id);
                    }
                }
                else
                {
                    if (!ACLHandler.isUserAllowedTo(PageItem.DataBisnis_Afiliasi_KPI.name + comp_id, aclId, "delete"))
                    {
                        return bindingkinerja(comp_id);
                    }
                }
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
            List<BisnisKPIContainer> temp = new List<BisnisKPIContainer>();
            foreach(bisnis_kpi b in result ){
                BisnisKPIContainer x = new BisnisKPIContainer()
                {
                    company_id = b.company_id,
                    content = b.content,
                    deskripsi = b.deskripsi,
                    id = b.id,
                    tahun = b.tahun
                };
                temp.Add(x);
            }

            return View(new GridModel<BisnisKPIContainer>
            {
                Data = temp
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


        public static int GetCategory(int id)
        {
            RelmonStoreEntities db = new RelmonStoreEntities();
            if (id == 0)
            {
                return 0;
            }
            else
            {
                var listResultTemp = (from x in db.companies
                                      where (x.id == id)
                                      select new
                                      {
                                          id = x.id,
                                          parent = x.parent
                                      }).ToList();
                if (listResultTemp.Count == 1)
                {
                    var result = listResultTemp.First();
                    if (result.parent == 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                }
                else
                {
                    return -1;
                }
            }
        }
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
