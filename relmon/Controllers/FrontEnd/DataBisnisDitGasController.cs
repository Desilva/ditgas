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
        protected RelmonStoreEntities db = new RelmonStoreEntities();

        public ActionResult Index()
        {
            ViewBag.selectedMenu = "databisnis";
            return View();
        }

        
        public ActionResult AdArt(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }

        public ActionResult Rkap(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }

        

        public ActionResult Rjpp(int id)
        {
            ViewBag.id = id;
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();
            var get = result.First();
            ViewBag.content = get.rjpp;
            if (string.IsNullOrWhiteSpace(get.rjpp))
            {
                ViewBag.content = "RJPP belum tersedia";
            }
            return PartialView();
        }

        public ActionResult BussinessReport(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }

        public ActionResult Kinerja(int id)
        {
            ViewBag.id = id;
            return PartialView();
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

        public ActionResult GetPdf(int id, int nomer)
        {
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();

            if (result.Count != 0)
            {
                var get = result.First();
                string file = "";
                if (nomer == 1)
                {//adart
                    file = Server.MapPath(Url.Content("~/upload/Data Bisnis/" + id + "/adart/" + get.ad_art));
                }
                else if (nomer == 2)
                {
                    file = Server.MapPath(Url.Content("~/upload/Data Bisnis/" + id + "/rkap/" + get.rkap));
                }
                else if (nomer == 3)
                {
                    file = Server.MapPath(Url.Content("~/upload/Data Bisnis/" + id + "/rencana kerja/" + get.visimisi));
                }
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
            else
            {
                return null;
            }
        }

        public string GetReportType(int id, int tahun, string bulan,string type)
        {
            var result = (from x in db.bisnis_bussiness_report
                          where x.company_id == id && x.tahun == tahun && x.bulan.Equals(bulan) && x.reportType == type
                          select x
                         ).ToList();
            if (result.Count != 0)
            {
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
                    return "other";
                }
            }
            else
            {
                return "blank";
            }
        }



        public ActionResult GetPdfReport(int id, int tahun, string bulan,string type)
        {

            var result = (from x in db.bisnis_bussiness_report
                          where x.company_id == id && x.tahun == tahun && x.bulan.Equals(bulan) && x.reportType == type
                          select x
                         ).ToList();
            var get = result.First();
            string file = Server.MapPath(Url.Content("~/upload/Data Bisnis/" + id + "/Bussiness Report/" + get.tahun + "/" + get.bulan + "/" + get.content));
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

        public string GetImage(int id, int tahun, string bulan,string type)
        {
            var result = (from x in db.bisnis_bussiness_report
                          where x.company_id == id && x.tahun == tahun && x.bulan.Equals(bulan) && x.reportType == type
                          select x
                         ).ToList();
            var get = result.First();
            return "../../Upload/Data Bisnis/" + id + "/Bussiness Report/" + get.tahun + "/" + get.bulan + "/" + get.content;

        }

        public string GetOther(int id, int tahun, string bulan,string type)
        {
            var result = (from x in db.bisnis_bussiness_report
                          where x.company_id == id && x.tahun == tahun && x.bulan.Equals(bulan) && x.reportType == type
                          select x
                         ).ToList();
            var get = result.First();
            return get.content;
        }

        public ActionResult Download(string filename, string id,string bulan,string tahun)
        {
            try
            {
                var fs = System.IO.File.OpenRead(Server.MapPath("~/Upload/Data Bisnis/"+id+"/Bussiness Report/"+tahun+"/" +bulan+"/"+ filename));
                string fileType = MyTools.getFileType(filename);
                return File(fs, fileType, filename);
            }
            catch
            {
                throw new HttpException(404, "Couldn't find " + filename);
            }
        }


        public string GetReportType2(int id, int tahun)
        {
            var result = (from x in db.bisnis_kpi
                          where x.company_id == id && x.tahun == tahun
                          select x
                         ).ToList();
            if (result.Count != 0)
            {
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
                    return "other";
                }
            }
            else
            {
                return "blank";
            }
        }



        public ActionResult GetPdfReport2(int id, int tahun)
        {

            var result = (from x in db.bisnis_kpi
                          where x.company_id == id && x.tahun == tahun
                          select x
                         ).ToList();
            var get = result.First();
            string file = Server.MapPath(Url.Content("~/upload/Data Bisnis/" + id + "/Kinerja/" + get.tahun + "/" + get.content));
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

        public string GetImage2(int id, int tahun)
        {
            var result = (from x in db.bisnis_kpi
                          where x.company_id == id && x.tahun == tahun
                          select x
                         ).ToList();
            var get = result.First();
            return "../../Upload/Data Bisnis/" + id + "/Kinerja/" + get.tahun + "/" + get.content;

        }

        public string GetOther2(int id, int tahun)
        {
            var result = (from x in db.bisnis_kpi
                          where x.company_id == id && x.tahun == tahun
                          select x
                         ).ToList();
            var get = result.First();
            return get.content;
        }

        public ActionResult Download2(string filename, string id,string tahun)
        {
            try
            {
                var fs = System.IO.File.OpenRead(Server.MapPath("~/Upload/Data Bisnis/" + id + "/Kinerja/" + tahun + "/" + filename));
                string fileType = MyTools.getFileType(filename);
                return File(fs, fileType, filename);
            }
            catch
            {
                throw new HttpException(404, "Couldn't find " + filename);
            }
        }
    }
}
