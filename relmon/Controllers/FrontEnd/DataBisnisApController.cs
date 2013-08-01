using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using relmon.Utilities;
using iTextSharp.text.pdf;

namespace relmon.Controllers.FrontEnd
{
    public class DataBisnisApController : Controller
    {
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

            switch (id)
            {
                case 0:
                    ViewBag.content = System.IO.File.ReadAllText(Server.MapPath("~/Content/data/profile/ap/profile-Badak.txt"));
                    break;
                case 1:
                    ViewBag.content = System.IO.File.ReadAllText(Server.MapPath("~/Content/data/profile/ap/profile-Arun.txt"));
                    break;
                case 2:
                    ViewBag.content = System.IO.File.ReadAllText(Server.MapPath("~/Content/data/profile/ap/profile-Donggi.txt"));
                    break;
                case 3:
                    ViewBag.content = System.IO.File.ReadAllText(Server.MapPath("~/Content/data/profile/ap/profile-NusantaraRegas.txt"));
                    break;
                case 4:
                    ViewBag.content = System.IO.File.ReadAllText(Server.MapPath("~/Content/data/profile/ap/profile-Pertagas.txt"));
                    break;
            }
            return PartialView();
        }

        public ActionResult StrukturOrganisasi(int id)
        {

            switch (id)
            {
                case 0:
                    ViewBag.content = "../../Content/data/struktur-org/ap/" + id + "/struktur.png";
                    break;
                case 1:
                    ViewBag.content = "../../Content/data/struktur-org/ap/" + id + "/struktur.png";
                    break;
                case 2:
                    ViewBag.content = "../../Content/data/struktur-org/ap/" + id + "/struktur.png";
                    break;
                case 3:
                    ViewBag.content = "../../Content/data/struktur-org/ap/" + id + "/struktur.png";
                    break;
                case 4:
                    ViewBag.content = "../../Content/data/struktur-org/ap/" + id + "/struktur.png";
                    break;
            }
            return PartialView();
        }

        public ActionResult AdArt()
        {
            return PartialView();
        }

        public ActionResult Rkap(int id)
        {
            ViewBag.id = id;
            ViewBag.exist = false;
            string filePath = Request.MapPath(Url.Content("~/Content/data/rkap/ap/" + id + "/rkap.pdf"));
            if (System.IO.File.Exists(filePath))
            {
                ViewBag.exist = true;
            }
            return PartialView();
        }

        public ActionResult RencanaKerja(int id)
        {
            ViewBag.id = id;
            ViewBag.exist = false;
            string filePath = Request.MapPath(Url.Content("~/Content/data/visi-misi/ap/" + id + "/visimisi.pdf"));
            if (System.IO.File.Exists(filePath))
            {
                ViewBag.exist = true;
            }
            return PartialView();
        }

        public ActionResult Rjpp(int id)
        {
            ViewBag.id = id;
            ViewBag.exist = false;
            string filePath = Request.MapPath(Url.Content("~/Content/data/rjpp/ap/" + id + "/rjpp.pdf"));
            if (System.IO.File.Exists(filePath))
            {
                ViewBag.exist = true;
            }
            return PartialView();
        }

        public ActionResult BussinessReport(int id)
        {

            ViewBag.id = id;
            ViewBag.exist = false;
            string filePath = Request.MapPath(Url.Content("~/Content/data/business-report/ap/" + id + "/business.pdf"));
            if (System.IO.File.Exists(filePath))
            {
                ViewBag.exist = true;
            }
            return PartialView();
        }

        public ActionResult Kinerja(int id)
        {

            switch (id)
            {
                case 0://badak
                    ViewBag.content = Directory.EnumerateFiles(Server.MapPath("~/Content/data/kinerja/ap/" + id + "/"));
                    ViewBag.id = id;
                    break;
                case 1://arun
                    ViewBag.content = Directory.EnumerateFiles(Server.MapPath("~/Content/data/kinerja/ap/" + id + "/"));
                    ViewBag.id = id;
                    break;
                case 2://dongi
                    ViewBag.content = Directory.EnumerateFiles(Server.MapPath("~/Content/data/kinerja/ap/" + id + "/"));
                    ViewBag.id = id;
                    break;
                case 3://nusantara regas
                    ViewBag.content = Directory.EnumerateFiles(Server.MapPath("~/Content/data/kinerja/ap/" + id + "/"));
                    ViewBag.id = id;
                    break;
                case 4://pertagas
                    ViewBag.content = Directory.EnumerateFiles(Server.MapPath("~/Content/data/kinerja/ap/" + id + "/"));
                    ViewBag.id = id;
                    break;
            }
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

        public ActionResult GetPdf(int id)
        {
            string file = Server.MapPath(Url.Content("~/Content/data/rkap/ap/" + id + "/rkap.pdf"));
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

        public ActionResult GetPdfBusiness(int id)
        {
            string file = Server.MapPath(Url.Content("~/Content/data/business-report/ap/" + id + "/business.pdf"));
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

        public ActionResult GetPdfVisiMisi(int id)
        {
            string file = Server.MapPath(Url.Content("~/Content/data/visi-misi/ap/" + id + "/visimisi.pdf"));
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
    }
}
