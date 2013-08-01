using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text.pdf;
using System.IO;
using relmon.Utilities;
using relmon.Models;

namespace relmon.Controllers.FrontEnd
{
    public class DirektoratDitGasController : Controller
    {
        private RelmonStoreEntities db = new RelmonStoreEntities();
        //
        // GET: /DirektoratDitGas/

        public ActionResult Index()
        {
            ViewBag.selectedMenu = "profil";
            return View();
        }

        public ActionResult GetPdfVisiMisi()
        {
            string result = (from x in db.dashboard_default
                             where (x.tipe == "visimisi")
                             select x.konten).FirstOrDefault();

            string file = Server.MapPath(Url.Content(result));
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
