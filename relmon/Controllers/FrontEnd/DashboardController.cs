using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using relmon.Models;
using System.Collections.Specialized;
using System.Diagnostics;
using iTextSharp.text.pdf;
using System.IO;
using relmon.Utilities;

namespace relmon.Controllers.FrontEnd
{
    public class DashboardController : Controller
    {
        private RelmonStoreEntities db = new RelmonStoreEntities();
        //
        // GET: /Dasboard/

        const int MAX_KONTEN_INFORMASI = 200;
        const int MAX_KONTEN_KEBIJAKAN = 200;
        const int MAX_RINGKASAN_INFORMASI = 200;
        const int MAX_RINGKASAN_KEBIJAKAN = 200;

        public ActionResult Index()
        {
            Config.menu = Config.MenuFrontEnd.DASHBOARD;
            ViewBag.nama = "Home";
            ViewBag.selectedMenu = "home";

            return View();
        }

        public JsonResult GetSlideshow()
        {
            List<string> listResult = db.dashboard_default
                .Where(x => x.tipe == "slideshow")
                .Select(x => x.konten)
                .ToList();
            return Json(listResult);
        }

        public JsonResult GetHighlight()
        {
            List<string> listResult = db.dashboard_default
                .Where(x => x.tipe == "highlight")
                .Select(x => x.konten)
                .ToList();
            return Json(listResult);
        }

        public JsonResult GetBanner()
        {
            string result = db.dashboard_default
                .Where(x => x.tipe == "banner_judul")
                .Select(x => x.konten)
                .FirstOrDefault();
            return Json(new
            {
                judul = db.dashboard_default.Where(x => x.tipe == "banner_judul").Select(x => x.konten).FirstOrDefault(),
                isi = db.dashboard_default.Where(x => x.tipe == "banner_isi").Select(x => x.konten).FirstOrDefault()
            });
        }

        public JsonResult GetNews()
        {
            var listResultTemp = (from x in db.dashboard_text
                                  join y in db.users on x.poster_user_id equals y.id
                                  where (x.tipe == "news")
                                  orderby x.tgl_update descending
                                  select new
                                  {
                                      id = x.id,
                                      tgl_update = x.tgl_update,
                                      ringkasan = x.ringkasan,
                                      konten = x.konten,
                                      kategori = x.kategori,
                                      poster = y.username
                                  }).ToList().Take(10);

            List<object> listResult = new List<object>();
            foreach (var result in listResultTemp)
            {
                listResult.Add(new
                {
                    id = result.id,
                    tgl_update = result.tgl_update.ToString("MMM dd, yyyy"),
                    ringkasan = (result.ringkasan.Length > MAX_RINGKASAN_INFORMASI) ? result.ringkasan.Substring(0, MAX_RINGKASAN_INFORMASI) + "..." : result.ringkasan,
                    konten = (result.konten.Length > MAX_KONTEN_INFORMASI) ? result.konten.Substring(0, MAX_KONTEN_INFORMASI) + "..." : result.konten,
                    kategori = result.kategori,
                    poster = result.poster
                });
            }

            return Json(listResult);
        }

        public JsonResult GetInformasi()
        {
            var listResultTemp = (from x in db.dashboard_text
                                  join y in db.users on x.poster_user_id equals y.id
                                  where (x.tipe == "informasi")
                                  orderby x.tgl_update descending
                                  select new
                                  {
                                      id = x.id,
                                      tgl_update = x.tgl_update,
                                      ringkasan = x.ringkasan,
                                      konten = x.konten,
                                      kategori = x.kategori,
                                      poster = y.username
                                  }).ToList();

            List<object> listResult = new List<object>();
            foreach (var result in listResultTemp)
            {
                listResult.Add(new
                {
                    id = result.id,
                    tgl_update = result.tgl_update.ToString("MMM dd, yyyy"),
                    ringkasan = (result.ringkasan.Length > MAX_RINGKASAN_INFORMASI) ? result.ringkasan.Substring(0, MAX_RINGKASAN_INFORMASI) + "..." : result.ringkasan,
                    konten = (result.konten.Length > MAX_KONTEN_INFORMASI) ? result.konten.Substring(0, MAX_KONTEN_INFORMASI) + "..." : result.konten,
                    kategori = result.kategori,
                    poster = result.poster
                });
            }

            return Json(listResult);
        }

        public JsonResult GetKebijakan()
        {
            var listResultTemp = (from x in db.dashboard_text
                                  join y in db.users on x.poster_user_id equals y.id
                                  where (x.tipe == "kebijakan")
                                  orderby x.tgl_update descending
                                  select new
                                  {
                                      id = x.id,
                                      tgl_update = x.tgl_update,
                                      ringkasan = x.ringkasan,
                                      konten = x.konten,
                                      poster = y.username
                                  }).ToList();

            List<object> listResult = new List<object>();
            foreach (var result in listResultTemp)
            {
                listResult.Add(new
                {
                    id = result.id,
                    tgl_update = result.tgl_update.ToString("MMM dd, yyyy"),
                    ringkasan = (result.ringkasan.Length > MAX_RINGKASAN_KEBIJAKAN) ? result.ringkasan.Substring(0, MAX_RINGKASAN_KEBIJAKAN) + "..." : result.ringkasan,
                    konten = (result.konten.Length > MAX_KONTEN_KEBIJAKAN) ? result.konten.Substring(0, MAX_KONTEN_KEBIJAKAN) + "..." : result.konten,
                    poster = result.poster
                });
            }

            return Json(listResult);
        }

        public ActionResult GetPdfRoadMap()
        {
            string result = (from x in db.dashboard_default
                                  where (x.tipe == "pdfroadmap")
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

        public ActionResult ReadMore(int id)
        {
            dashboard_text dashboardText = db.dashboard_text.Where(x => x.id == id).FirstOrDefault();
            ViewBag.tipe = char.ToUpper(dashboardText.tipe[0]) + dashboardText.tipe.Substring(1);
            ViewBag.kategori = dashboardText.kategori ?? "";
            ViewBag.konten = dashboardText.konten;
            ViewBag.tgl_update = dashboardText.tgl_update.ToString("MMM dd, yyyy");
            ViewBag.poster_username = db.users.Where(x => x.id == dashboardText.poster_user_id).Select(x => x.username).FirstOrDefault();

            return View();
        }
    }
}
