using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text.pdf;
using relmon.Utilities;
using System.IO;
using Telerik.Web.Mvc;
using relmon.Models;

namespace relmon.Controllers.FrontEnd
{
    public class DataBisnisDitGasController : Controller
    {
        private RelmonStoreEntities db = new RelmonStoreEntities();
        //
        // GET: /DataBisnisDitGas/
        public ActionResult Index()
        {
            ViewBag.selectedMenu = "databisnis";
            return View();
        }

        public ActionResult AdArt()
        {
            return PartialView();
        }

        public ActionResult RencanaKerja()
        {
            return PartialView();
        }

        public ActionResult Rjpp()
        {
            return PartialView();
        }

        public ActionResult Rkap()
        {
            return PartialView();
        }

        public ActionResult BussinessReport()
        {
            return PartialView();
        }

        public ActionResult Kinerja()
        {
            return PartialView();
        }

        public ActionResult ProjectStatus()
        {
            return PartialView();
        }

        public ActionResult Product()
        {
            return PartialView();
        }

        //
        // 0 -> kinerja
        // 1 -> bisnis report
        //
        public ActionResult GetPdf(int id)
        {
            var getId = 0;
            var result = (from x in db.bisnis_main
                          where x.company_id == getId
                          select x
                         ).ToList();
            var get = result.First();
            string file ="";
            if(id == 1){//adart
         //       string abs = Config.upload + "\\DataBisnis\\" + getId + "\\adart\\" + get.ad_art;
         //       String RelativePath = "~/" + abs.Substring(HttpContext.Request.PhysicalApplicationPath.Length)
         //.Replace("\\", "/");
                //var a = Config.upload + "\\DataBisnis\\" + getId + "\\adart\\" + get.ad_art;

                file = Server.MapPath(Url.Content("~/upload/Data Bisnis/" + getId + "/adart/" + get.ad_art));
            }else if(id == 2){
                //file = Server.MapPath(Url.Content(Config.upload+"\\DataBisnis\\"+getId+"\\rkap\\"+get.rkap));
                //file = Server.MapPath(Url.Content("~/Content/data/rkap/rkap.pdf"));
                file = Server.MapPath(Url.Content("~/upload/Data Bisnis/"+getId+"/rkap/" + get.rkap));
            }
            //else if (id == 3)
            //{
            //    file = Server.MapPath(Url.Content("~/Content/data/rkap/rkap.pdf"));
            //}
            //else if (id == 4)
            //{

            //    file = Server.MapPath(Url.Content("~/Content/data/rkap/adart.pdf"));
            //}
            //else
            //{
            //    file = Server.MapPath(Url.Content("~/Content/data/bisnis-report/September_2012.pdf"));
            //}
            try
            {

                PdfReader reader = new PdfReader(file);
                MemoryStream pdfStream = new MemoryStream();

                PdfStamper pdfStamper = new PdfStamper(reader, pdfStream);

                reader.Close();
                pdfStamper.Close();
                pdfStream.Flush();
                pdfStream.Close();

                byte[] pdfArray = pdfStream.ToArray();
                return new BinaryContentResult(pdfArray, "application/pdf");
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }


        public string GetRjpp()
        {
            var getId = 0;
            var result = (from x in db.bisnis_main
                          where x.company_id == getId
                          select x
                         ).ToList();
            var get = result.First();
            return get.rjpp;
        }


        public string GetReportType(int tahun, string bulan)
        {
            var getId = 0;
            var result = (from x in db.bisnis_bussiness_report
                          where x.company_id == getId && x.tahun == tahun && x.bulan.Equals(bulan)
                          select x
                         ).ToList();
            var get = result.First();
            if (get.content.Substring(get.content.Length - 3).Equals("pdf"))
            {
                return "pdf";
            }
            else if (get.content.Substring(get.content.Length - 3).Equals("jpg") || get.content.Substring(get.content.Length - 3).Equals("png"))
            {
                return "image";
            }
            else
            {
                //ViewBag.report = Directory.EnumerateFiles(Server.MapPath(Config.upload + "\\DataBisnis\\" + getId + "\\Bussiness Report\\" + get.tahun + "\\" + get.bulan + "\\" + get.content));
                //List<string> result = new List<string>();

                //foreach (string file in Directory.EnumerateFiles(path, "*.*",
                //      SearchOption.AllDirectories)
                //      .Where(s => s.EndsWith(".mp3") || s.EndsWith(".wma")))
                //{
                //    result.Add(file);
                //}
                return "other";
            }
        }
        public ActionResult GetPdfReport(int tahun, string bulan)
        {
            var getId = 0;
            var result = (from x in db.bisnis_bussiness_report
                          where x.company_id == getId && x.tahun == tahun && x.bulan.Equals(bulan)
                          select x
                         ).ToList();
            var get = result.First();
                string file = Server.MapPath(Url.Content("~/upload/Data Bisnis/" + getId + "/Bussiness Report/" + get.tahun + "/"+get.bulan +"/"+get.content));
                try
                {

                    PdfReader reader = new PdfReader(file);
                    MemoryStream pdfStream = new MemoryStream();

                    PdfStamper pdfStamper = new PdfStamper(reader, pdfStream);

                    reader.Close();
                    pdfStamper.Close();
                    pdfStream.Flush();
                    pdfStream.Close();

                    byte[] pdfArray = pdfStream.ToArray();
                    return new BinaryContentResult(pdfArray, "application/pdf");
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                } 
        }

        public string GetImage(int tahun, string bulan)
        {
            var getId = 0;
            var result = (from x in db.bisnis_bussiness_report
                          where x.company_id == getId && x.tahun == tahun && x.bulan.Equals(bulan)
                          select x
                         ).ToList();
            var get = result.First();
            return "../../Upload/Data Bisnis/"+getId+"/Bussiness Report/"+get.tahun+"/"+get.bulan+"/"+get.content;
                
        }


        public string GetKinerjaType(int tahun)
        {
            var getId = 0;
            var result = (from x in db.bisnis_kpi
                          where x.company_id == getId && x.tahun == tahun
                          select x
                         ).ToList();
            var get = result.First();
            if (get.content.Substring(get.content.Length - 3).Equals("pdf"))
            {
                return "pdf";
            }
            else if (get.content.Substring(get.content.Length - 3).Equals("jpg") || get.content.Substring(get.content.Length - 3).Equals("png"))
            {
                return "image";
            }
            else
            {
                //ViewBag.report = Directory.EnumerateFiles(Server.MapPath(Config.upload + "\\DataBisnis\\" + getId + "\\Bussiness Report\\" + get.tahun + "\\" + get.bulan + "\\" + get.content));
                //List<string> result = new List<string>();

                //foreach (string file in Directory.EnumerateFiles(path, "*.*",
                //      SearchOption.AllDirectories)
                //      .Where(s => s.EndsWith(".mp3") || s.EndsWith(".wma")))
                //{
                //    result.Add(file);
                //}
                return "other";
            }
        }
        public ActionResult GetPdfKinerja(int tahun)
        {
            var getId = 0;
            var result = (from x in db.bisnis_kpi
                          where x.company_id == getId && x.tahun == tahun
                          select x
                         ).ToList();
            var get = result.First();
            string file = Server.MapPath(Url.Content("~/upload/Data Bisnis/" + getId + "/Kinerja/" + get.tahun + "/"+ get.content));
            try
            {

                PdfReader reader = new PdfReader(file);
                MemoryStream pdfStream = new MemoryStream();

                PdfStamper pdfStamper = new PdfStamper(reader, pdfStream);

                reader.Close();
                pdfStamper.Close();
                pdfStream.Flush();
                pdfStream.Close();

                byte[] pdfArray = pdfStream.ToArray();
                return new BinaryContentResult(pdfArray, "application/pdf");
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public string GetImageKinerja(int tahun, string bulan)
        {
            var getId = 0;
            var result = (from x in db.bisnis_kpi
                          where x.company_id == getId && x.tahun == tahun
                          select x
                         ).ToList();
            var get = result.First();
            return "../../Upload/Data Bisnis/" + getId + "/Kinerja/" + get.tahun + "/"+ get.content;

        }
        public ActionResult Download(string file, string id)
        {
            try
            {
                var fs = System.IO.File.OpenRead(Server.MapPath("~/Content/data/kinerja/ap/" + id + "/" + file));
                string fileType = MyTools.getFileType(file);
                return File(fs, fileType, file);
            }
            catch
            {
                throw new HttpException(404, "Couldn't find " + file);
            }
        }
    }
}
