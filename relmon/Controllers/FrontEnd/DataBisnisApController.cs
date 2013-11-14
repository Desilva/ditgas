using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using relmon.Utilities;
using iTextSharp.text.pdf;
using relmon.Models;
using Telerik.Web.Mvc.UI;

namespace relmon.Controllers.FrontEnd
{
    public class DataBisnisApController : DataBisnisDitGasController
    {
        public ActionResult Connector(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }

        public ActionResult Profil(int id)
        {
            ViewBag.id = id;
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();
            if (result.Count != 0)
            {
                var get = result.First();
                ViewBag.content = get.profile;
                if (string.IsNullOrWhiteSpace(get.profile))
                {
                    ViewBag.content = "Profil belum tersedia";
                }
            }
            else
            {
                ViewBag.content = "Profil belum tersedia";
            }
            
            return PartialView();
        }

        public ActionResult StrukturOrganisasi(int id)
        {
            ViewBag.id = id;
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();
            if (result.Count != 0)
            {
                var get = result.First();
                ViewBag.content = "/upload/Data Bisnis/" + id + "/struktur organisasi/" + get.struktur;
            }
            return PartialView();
        }

        public ActionResult RencanaKerja(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }



        public ActionResult Product(int id,string produk,int tahun, int fromYear, int toYear, string fromMonth, string toMonth)
        {
            ViewBag.id = id;
            ViewBag.produk = produk;
            ViewBag.tahun = tahun;
            ViewBag.fromYear = fromYear;
            ViewBag.toYear = toYear;
            ViewBag.fromMonth = fromMonth;
            ViewBag.toMonth = toMonth;
            ViewBag.fromMonthNum = changeNumber(fromMonth);
            ViewBag.toMonthNum = changeNumber(toMonth);
            return PartialView();
        }

         public int changeNumber(string a) {
            if (a == "Januari") {
                return 1;
            } else if (a == "Februari") {
                return 2;
            } else if (a == "Maret") {
                return 3;
            } else if (a == "April") {
                return 4;
            } else if (a == "Mei") {
                return 5;
            } else if (a == "Juni") {
                return 6;
            } else if (a == "Juli") {
                return 7;
            } else if (a == "Agustus") {
                return 8;
            } else if (a == "September") {
                return 9;
            } else if (a == "Oktober") {
                return 10;
            } else if (a == "November") {
                return 11;
            } else if (a == "Desember") {
                return 12;
            } else {
                return -1;
            }
        }
        public ActionResult ProductNavigator(int id)
        {
            ViewBag.id = id;
            var listResultTemp = (from x in db.bisnis_product
                                  where x.company_id == id
                                  select x.product).Distinct().ToList();
            ViewData["products"] = listResultTemp;
            return PartialView();
        }
        public ActionResult _ProductData(int id, string produk,int tahun)
        {
            var prod = produk.Replace('_', ' ');
            var listResultTemp = (from x in db.bisnis_product
                         where x.company_id == id && x.product == prod && x.tahun == tahun
                         select x).ToList();
            List<object> listResult = new List<object>();
            foreach (var b in listResultTemp)
            {
                listResult.Add(new
                {
                    company_id = b.company_id,
                    product = b.product,
                    id = b.id,
                    tahun = b.tahun,
                    januari = b.januari,
                    februari = b.februari,
                    maret = b.maret,
                    april = b.april,
                    mei = b.mei,
                    juni = b.juni,
                    juli = b.juli,
                    agustus = b.agustus,
                    september = b.september,
                    oktober = b.oktober,
                    november = b.november,
                    desember = b.desember
                });
            }

            return Json(listResult);
            //return Json(SalesDataBuilder.GetCollection());

        }

        public ActionResult _ProductData2(int id, string produk, int tahun,int tahunAkhir)
        {
            var prod = produk.Replace('_', ' ');
            var listResultTemp = (from x in db.bisnis_product
                                  where x.company_id == id && x.product == prod && x.tahun >= tahun && x.tahun <= tahunAkhir
                                  select x).ToList();
            List<object> listResult = new List<object>();
            foreach (var b in listResultTemp)
            {
                listResult.Add(new
                {
                    company_id = b.company_id,
                    product = b.product,
                    id = b.id,
                    tahun = b.tahun,
                    januari = b.januari,
                    februari = b.februari,
                    maret = b.maret,
                    april = b.april,
                    mei = b.mei,
                    juni = b.juni,
                    juli = b.juli,
                    agustus = b.agustus,
                    september = b.september,
                    oktober = b.oktober,
                    november = b.november,
                    desember = b.desember
                });
            }

            return Json(listResult);
            //return Json(SalesDataBuilder.GetCollection());

        }


        public ActionResult PedomanKinerja(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }

        [HttpPost]
        public JsonResult GetKeuangan(int id, int tahun)
        {
            var listResultTemp = (from x in db.bisnis_kinerja_keuangan
                                  where x.company_id == id && x.tahun == tahun
                                  select x).ToList();

            List<object> listResult = new List<object>();
            foreach (var result in listResultTemp)
            {
                listResult.Add(new
                {
                    roe = result.roe,
                    roi = result.roi,
                    opm = result.opm,
                    npm = result.npm,
                    cash = result.cash_ratio,
                    current = result.current_ratio,
                    cp = result.cp,
                    ito = result.ito,
                    tato = result.tato,
                    etta = result.etta,
                    tier = result.tier,
                    nkk = result.nkk,
                    klasifikasi = result.klasifikasi
                });
            }

            return Json(listResult);
        }

        public JsonResult GetPertumbuhan(int id, int tahun)
        {
            var listResultTemp = (from x in db.bisnis_kinerja_pertumbuhan
                                  where x.company_id == id && x.tahun == tahun
                                  select x).ToList();

            List<object> listResult = new List<object>();
            foreach (var result in listResultTemp)
            {
                listResult.Add(new
                {
                    aspg = result.aspg,
                    salg = result.salg,
                    npmg = result.npmg,
                    stag = result.stag,
                    npg = result.npg,
                    nkp = result.nkp,
                    klasifikasi = result.klasifikasi
                });
            }

            return Json(listResult);
        }

        public JsonResult GetAdministrasi(int id, int tahun)
        {
            var listResultTemp = (from x in db.bisnis_kinerja_administrasi
                                  where x.company_id == id && x.tahun == tahun
                                  select x).ToList();

            List<object> listResult = new List<object>();
            foreach (var result in listResultTemp)
            {
                listResult.Add(new
                {
                    bulanan = result.nilai_keuangan_bulanan,
                    manajemen = result.nilai_manajemen_bulanan,
                    audited = result.nilai_keuangan_audited,
                    rkap = result.nilai_rancangan_rkap,
                    nka = result.nka,
                    klasifikasi = result.klasifikasi
                });
            }

            return Json(listResult);
        }

        public JsonResult GetKesehatan(int id, int tahun)
        {
            var listResultTemp = (from x in db.bisnis_kinerja_kesehatan
                                  where x.company_id == id && x.tahun == tahun
                                  select x).ToList();

            List<object> listResult = new List<object>();
            foreach (var result in listResultTemp)
            {
                listResult.Add(new
                {
                    klasifikasi = result.klasifikasi
                });
            }

            return Json(listResult);
        }


        public ActionResult KinerjaMain(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }

        public ActionResult Kinerja2(int id, int tahun)
        {
            ViewBag.id = id;
            ViewBag.tahun = tahun;
            return PartialView();
        }

        [HttpPost]
        public JsonResult GetDistinctTahun(int id)
        {
            //int parentInt = Int32.Parse(parent);
            var q1 = db.bisnis_kinerja_keuangan.Where(x => x.company_id == id).Select(x => new { tahun = x.tahun}).ToList();
            var q2 = db.bisnis_kinerja_pertumbuhan.Where(x => x.company_id == id).Select(x => new { tahun = x.tahun }).ToList();
            var q3 = db.bisnis_kinerja_administrasi.Where(x => x.company_id == id).Select(x => new { tahun = x.tahun }).ToList();
            var q4 = db.bisnis_kinerja_kesehatan.Where(x => x.company_id == id).Select(x => new { tahun = x.tahun }).ToList();
            var listResultTemp = q1.Union(q2).Union(q3).Union(q4).Distinct();
            List<object> listResult = new List<object>();
            foreach (var result in listResultTemp)
            {
                listResult.Add(new
                {
                    tahun = result.tahun
                });
            }

            return Json(listResult);
        }

    }
}
