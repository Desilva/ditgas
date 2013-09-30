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
    public class AturanRegulasiController : Controller
    {
        private RelmonStoreEntities db = new RelmonStoreEntities();
        
        //
        // GET: /AturanRegulasi/

        public ActionResult Index()
        {
            ViewBag.selectedMenu = "aturanregulasi";
            ViewBag.content = Directory.EnumerateFiles(Server.MapPath("~/Upload/Aturan/Pertamina/"));        
            return View();
        }

        public ActionResult AturanKementrian()
        {
            ViewBag.selectedMenu = "aturanregulasi";
            return View();
        }


        public ActionResult Pedoman()
        {
            ViewBag.selectedMenu = "aturanregulasi";
            return View();
        }

        //public ActionResult GetPdf(int id)
        //{

        //    string[] content = new string[5];

        //    content[0] = "Charter dit GAS.pdf";
        //    content[1] = "pertamina/Pedoman Pengembangan dan Kerjasama Bisnis.pdf";
        //    content[2] = "pertamina/Pendirian Anak Perusahaan A 001.pdf";
        //    content[3] = "pertamina/TKO Pengusulan & Persetujuan Investasi Pengembangan & Kerjasama Bisnis.pdf";
        //    content[4] = "EKB.pdf";

        //    string file = Server.MapPath(Url.Content("~/Content/aturan/"+content[id]));
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

        public ActionResult Download(string file)
        {
            try
            {
                var fs = System.IO.File.OpenRead(Server.MapPath("~/Upload/Aturan/Pertamina/" + file));
                string fileType = MyTools.getFileType(file);
                return File(fs, fileType, file);
            }
            catch
            {
                throw new HttpException(404, "Couldn't find " + file);
            }
        }

        public ActionResult GetPdf(string tipe)
        {
            var result = (from x in db.aturans
                          where x.tipe == tipe
                          select x
                         ).ToList();
            if (result.Count != 0)
            {
                aturan items = result.First();
                string filename = items.filename;
                string file = Server.MapPath(Url.Content("~/Upload/Aturan/"+tipe+"/" + filename));
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
            else
            {
                return null;
            }
            
            
        }
       
    }
}
