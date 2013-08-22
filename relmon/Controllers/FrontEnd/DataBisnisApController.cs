using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using relmon.Utilities;
using iTextSharp.text.pdf;
using relmon.Models;

namespace relmon.Controllers.FrontEnd
{
    public class DataBisnisApController : Controller
    {
        private RelmonStoreEntities db = new RelmonStoreEntities();
        //
        // GET: /DataBisnisAp/

        public ActionResult Index()
        {
            ViewBag.selectedMenu = "databisnis";
            return View();
        }

        public ActionResult Connector(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }

        public ActionResult Profil(int id)
        {
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();
            var get = result.First();
            ViewBag.content = get.profile;
            return PartialView();
        }

        public ActionResult StrukturOrganisasi(int id)
        {
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();
            var get = result.First();
            ViewBag.content = "../. /upload/Data Bisnis/" + id + "/struktur organisasi/" +get.struktur;
            
            return PartialView();
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

        public ActionResult RencanaKerja(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }

        public ActionResult Rjpp(int id)
        {
            var result = (from x in db.bisnis_main
                         where x.company_id == id
                         select x
                         ).ToList();
            var get = result.First();
            ViewBag.content = get.rjpp;
            return PartialView();
        }

        public ActionResult BussinessReport(int id)
        {

            ViewBag.id = id;
            //ViewBag.exist = false;
            //string filePath = Request.MapPath(Url.Content("~/Content/data/business-report/ap/" + id + "/business.pdf"));
            //if (System.IO.File.Exists(filePath))
            //{
            //    ViewBag.exist = true;
            //}
            return PartialView();
        }

        public ActionResult Kinerja(int id)
        {
            ViewBag.id = id;
            //switch (id)
            //{
            //    case 0://badak
            //        ViewBag.content = Directory.EnumerateFiles(Server.MapPath("~/Content/data/kinerja/ap/" + id + "/"));
            //        ViewBag.id = id;
            //        break;
            //    case 1://arun
            //        ViewBag.content = Directory.EnumerateFiles(Server.MapPath("~/Content/data/kinerja/ap/" + id + "/"));
            //        ViewBag.id = id;
            //        break;
            //    case 2://dongi
            //        ViewBag.content = Directory.EnumerateFiles(Server.MapPath("~/Content/data/kinerja/ap/" + id + "/"));
            //        ViewBag.id = id;
            //        break;
            //    case 3://nusantara regas
            //        ViewBag.content = Directory.EnumerateFiles(Server.MapPath("~/Content/data/kinerja/ap/" + id + "/"));
            //        ViewBag.id = id;
            //        break;
            //    case 4://pertagas
            //        ViewBag.content = Directory.EnumerateFiles(Server.MapPath("~/Content/data/kinerja/ap/" + id + "/"));
            //        ViewBag.id = id;
            //        break;
            //}
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

        public ActionResult Download(string file, string id) {
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

        public ActionResult GetPdf(int id,int nomer)
        {
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();
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
            else if(nomer == 3)
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

        public string GetReportType(int id,int tahun, string bulan)
        {
            
            var result = (from x in db.bisnis_bussiness_report
                          where x.company_id == id && x.tahun == tahun && x.bulan.Equals(bulan)
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



        public ActionResult GetPdfReport(int id, int tahun, string bulan)
        {
            
            var result = (from x in db.bisnis_bussiness_report
                          where x.company_id == id && x.tahun == tahun && x.bulan.Equals(bulan)
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

        public string GetImage(int id,int tahun, string bulan)
        {
            var result = (from x in db.bisnis_bussiness_report
                          where x.company_id == id && x.tahun == tahun && x.bulan.Equals(bulan)
                          select x
                         ).ToList();
            var get = result.First();
            return "../../Upload/Data Bisnis/" + id + "/Bussiness Report/" + get.tahun + "/" + get.bulan + "/" + get.content;

        }


        public string GetKinerjaType(int id,int tahun)
        {
            var result = (from x in db.bisnis_kpi
                          where x.company_id == id && x.tahun == tahun
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
        public ActionResult GetPdfKinerja(int id, int tahun)
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

        public string GetImageKinerja(int id, int tahun, string bulan)
        {
            var result = (from x in db.bisnis_kpi
                          where x.company_id == id && x.tahun == tahun
                          select x
                         ).ToList();
            var get = result.First();
            return "../../Upload/Data Bisnis/" + id + "/Kinerja/" + get.tahun + "/" + get.content;

        }
        //public ActionResult Download(string file, string id)
        //{
        //    try
        //    {
        //        var fs = System.IO.File.OpenRead(Server.MapPath("~/Content/data/kinerja/ap/" + id + "/" + file));
        //        string fileType = MyTools.getFileType(file);
        //        return File(fs, fileType, file);
        //    }
        //    catch
        //    {
        //        throw new HttpException(404, "Couldn't find " + file);
        //    }
        //}

        //public ActionResult GetPdfBusiness(int id)
        //{
        //    string file = Server.MapPath(Url.Content("~/Content/data/business-report/ap/" + id + "/business.pdf"));
        //    PdfReader reader = new PdfReader(file);
        //    MemoryStream pdfStream = new MemoryStream();

        //    PdfStamper pdfStamper = new PdfStamper(reader, pdfStream);

        //    reader.Close();
        //    pdfStamper.Close();
        //    pdfStream.Flush();
        //    pdfStream.Close();

        //    byte[] pdfArray = pdfStream.ToArray();
        //    return new BinaryContentResult(pdfArray, "application/pdf");
        //}

        //public ActionResult GetPdfVisiMisi(int id)
        //{
        //    string file = Server.MapPath(Url.Content("~/Content/data/visi-misi/ap/" + id + "/visimisi.pdf"));
        //    PdfReader reader = new PdfReader(file);
        //    MemoryStream pdfStream = new MemoryStream();

        //    PdfStamper pdfStamper = new PdfStamper(reader, pdfStream);

        //    reader.Close();
        //    pdfStamper.Close();
        //    pdfStream.Flush();
        //    pdfStream.Close();

        //    byte[] pdfArray = pdfStream.ToArray();
        //    return new BinaryContentResult(pdfArray, "application/pdf");
        //}


        //public string GetStruktur(int id)
        //{
        //    var result = (from x in db.bisnis_main
        //                  where x.company_id == id
        //                  select x
        //                 ).ToList();
        //    var get = result.First();
        //    return Config.upload+"\\Data Bisnis\\" + id + "\\struktur organisasi\\"+get.struktur;
        //}
    }
}
