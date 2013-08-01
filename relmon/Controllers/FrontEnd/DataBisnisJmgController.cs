using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using relmon.Utilities;
using iTextSharp.text.pdf;
using System.IO;

namespace relmon.Controllers.FrontEnd
{
    public class DataBisnisJmgController : Controller
    {
        //
        // GET: /DataBisnisJmg/

        public ActionResult Index()
        {
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
            string file ="";
            if(id == 0){
                file = Server.MapPath(Url.Content("~/Content/kinerja/Kinerja Perusahaan 2007 -2010.pdf"));
            }else if(id == 1){
                file = Server.MapPath(Url.Content("~/Content/data/bisnis-report/September_2012.pdf"));
            }
            else if (id == 2)
            {
                file = Server.MapPath(Url.Content("~/Content/data/rkap/rkap.pdf"));
            }
            else if (id == 3)
            {
                file = Server.MapPath(Url.Content("~/Content/data/rkap/adart.pdf"));
            }
            else
            {
                file = Server.MapPath(Url.Content("~/Content/data/bisnis-report/September_2012.pdf"));
            }
             
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
