using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using relmon.Utilities;
using iTextSharp.text.pdf;
using relmon.Models;
using System.Data;
using Telerik.Web.Mvc;
using relmon.Utilities.KpiCalculation;

namespace relmon.Controllers.Admin
{
    public class DataBisnisApAdminController : DataBisnisDitGasAdminController
    {
        //
        // GET: /DataBisnisApAdmin/

        public override ActionResult Index()
        {
            ViewBag.selectedMenu = "databisnis";
            return PartialView();
        }

        public ActionResult Connector(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }

        #region Profil
        public ActionResult Profil(int id)
        {
            ViewBag.id = id;
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();
            if (result.Count == 0)
            {
                bisnis_main e = new bisnis_main();
                e.profile = "";
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
        public bool UploadProfil(int id, string profil)
        {
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                        ).ToList();

            if (result.Count == 0)
            {
                int aclId = (int)Session["id"];
                int category = DataBisnisDitGasAdminController.GetCategory(id);

                if (category == 1)
                {
                    if (!ACLHandler.isUserAllowedTo(PageItem.DataBisnis_Ap_Profil.name, aclId, "create"))
                    {
                        return false;
                    }
                }
                else if (category == 2)
                {
                    if (!ACLHandler.isUserAllowedTo(PageItem.DataBisnis_Afiliasi_Profil.name + id, aclId, "create"))
                    {
                        return false;
                    }
                }
                
                bisnis_main items = new bisnis_main();
                items.company_id = id;
                items.profile = HttpUtility.UrlDecode(profil);

                db.bisnis_main.Add(items);
            }
            else
            {
                int aclId = (int)Session["id"];
                int category = DataBisnisDitGasAdminController.GetCategory(id);

                if (category == 1)
                {
                    if (!ACLHandler.isUserAllowedTo(PageItem.DataBisnis_Ap_Profil.name, aclId, "update"))
                    {
                        return false;
                    }
                }
                else if (category == 2)
                {
                    if (!ACLHandler.isUserAllowedTo(PageItem.DataBisnis_Afiliasi_Profil.name + id, aclId, "update"))
                    {
                        return false;
                    }
                }

                bisnis_main items = result.First();
                items.profile = HttpUtility.UrlDecode(profil);
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

        #region struktur
        public ActionResult StrukturOrganisasi(int id)
        {
            ViewBag.id = id;
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();
            if (result.Count == 0)
            {
                bisnis_main e = new bisnis_main();
                e.struktur = "";
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
        public bool UploadStruktur(int id,string filename)
        {
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();

            if (result.Count == 0)
            {
                bisnis_main items = new bisnis_main();
                items.company_id = id;
                items.struktur = filename;
                db.bisnis_main.Add(items);

            }
            else
            {
                bisnis_main items = result.First();
                items.struktur = filename;
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

        public bool DeleteStruktur(int id)
        {
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();

            if (result.Count != 0)
            {
                bisnis_main items = result.First();
                items.struktur = "";
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

        public string GetStruktur(int id)
        {
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();
            var get = result.First();
            return "../../upload/Data Bisnis/" + id + "/struktur organisasi/" + get.struktur;
        }
        #endregion

        #region visi misi
        public ActionResult RencanaKerja(int id)
        {
            ViewBag.id = id;
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();
            if (result.Count == 0)
            {
                bisnis_main e = new bisnis_main();
                e.visimisi = "";
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
        public bool UploadRencanaKerja(int id,string filename)
        {
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();

            if (result.Count == 0)
            {
                bisnis_main items = new bisnis_main();
                items.company_id = id;
                items.visimisi = filename;
                db.bisnis_main.Add(items);

            }
            else
            {
                bisnis_main items = result.First();
                items.visimisi = filename;
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

        public bool DeleteRencanaKerja(int id)
        {
            var result = (from x in db.bisnis_main
                          where x.company_id == id
                          select x
                         ).ToList();

            if (result.Count != 0)
            {
                bisnis_main items = result.First();
                items.visimisi = "";
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


        [HttpPost]
        public JsonResult GetCompanyList(int parent, bool wcheck)
        {
            //int parentInt = Int32.Parse(parent);
            var listResultTemp = (from x in db.companies
                                  where (x.parent == parent)
                                  select new
                                  {
                                      id = x.id,
                                      nama = x.nama
                                  }).ToList();

            List<object> listResult = new List<object>();
            foreach (var result in listResultTemp)
            {

                if (wcheck)
                {
                    if (checkACL(result.nama))
                    {
                        listResult.Add(new
                        {
                            id = result.id,
                            member = result.nama
                        });
                    }
                }
                else
                {
                    listResult.Add(new
                    {
                        id = result.id,
                        member = result.nama
                    });
                }
            }

            return Json(listResult);
        }

        public virtual bool checkACL(string nama){
            return ACLHandler.isUserAllowedTo(PageItem.DataBisnis_ApCompany.name +nama,(int) Session["id"], "view");
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

        #region product

        public ActionResult Product(int id,int tahun)
        {
            ViewBag.id = id;
            ViewBag.tahun = tahun;
            return PartialView();
        }
        protected ViewResult bindingProduct(int id, int tahun)
        {
            List<bisnis_product> result = (from x in db.bisnis_product
                                       where x.company_id == id && x.tahun == tahun
                                       select x).ToList();
            List<BisnisProductContainer> temp = new List<BisnisProductContainer>();
            foreach (bisnis_product b in result)
            {
                BisnisProductContainer x = new BisnisProductContainer()
                {
                    company_id = b.company_id,
                    product = b.product,
                    id = b.id,
                    januari = b.januari,
                    februari = b.februari,
                    maret = b.maret,
                    april =b.april,
                    mei = b.mei,
                    juni = b.juni,
                    juli = b.juli,
                    agustus = b.agustus,
                    september = b.september,
                    oktober = b.oktober,
                    november = b.november,
                    desember = b.desember
                };
                temp.Add(x);
            }

            return View(new GridModel<BisnisProductContainer>
            {
                Data = temp
            });
        }
        [GridAction]
        public ActionResult _SelectProduct(int id, int tahun)
        {
            return bindingProduct(id,tahun);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SaveProduct(int id, int tahun)
        {
            List<bisnis_product> result = (from x in db.bisnis_product
                                           where x.id == id
                                           select x).ToList();
            var comp_id = -1;
            
            if(result.Count == 1){
                bisnis_product getResult = result.First();
                comp_id = getResult.company_id;
                TryUpdateModel(getResult);
                db.Entry(getResult).State = EntityState.Modified;
            
            }
            db.SaveChanges();
            return bindingProduct(comp_id, tahun);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertProduct(int id, int tahun)
        {
            bisnis_product newProduct = new bisnis_product();
            if(TryUpdateModel(newProduct)){
                newProduct.company_id = id;
                db.bisnis_product.Add(newProduct);
                db.SaveChanges();
            }
            return bindingProduct(id, tahun);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteProduct(int id, int tahun)
        {
            List<bisnis_product> result = (from x in db.bisnis_product
                                           where x.id == id
                                           select x).ToList();
            var comp_id = -1;
            if (result.Count == 1)
            {
                bisnis_product getResult = result.First();
                db.bisnis_product.Remove(getResult);
                comp_id = getResult.company_id;
            }

            db.SaveChanges();
            return bindingProduct(comp_id, tahun);
        }
        #endregion


        #region KPI
        public ActionResult KPI(int id,int tahun)
        {
            ViewBag.id = id;
            ViewBag.tahun = tahun;
            return PartialView();
        }

        public ActionResult KinerjaKeuangan(int id, int tahun)
        {
            ViewBag.id = id;
            ViewBag.tahun = tahun;
            return PartialView();
        }

        public JsonResult GetKinerjaKeuangan(int id,int tahun)
        {
           var listResultTemp = (from x in db.bisnis_kinerja_keuangan
                                where x.company_id == id && x.tahun ==tahun
                                select x).ToList();
            List<object> listResult = new List<object>();
            foreach (var result in listResultTemp)
            {
                listResult.Add(new
                {
                    laba_bersih = result.laba_bersih,
                    modal_sendiri = result.modal_sendiri,
                    ebtit = result.ebtit,
                    penyusutan = result.penyusutan,
                    capital_employed = result.capital_employed,
                    laba_operasi = result.laba_operasi,
                    total_pendapatan_usaha = result.total_pendapatan_usaha,
                    kas = result.kas,
                    bank = result.bank,
                    surat_berharga = result.surat_berharga,
                    current_liabillities = result.current_liabillities,
                    current_asset = result.current_asset,
                    total_piutang_usaha = result.total_piutang_usaha,
                    total_pendapatan = result.total_pendapatan,
                    total_modal_sendiri = result.total_modal_sendiri,
                    total_asset = result.total_asset,
                    ebitda = result.ebitda,
                    interest_payment = result.interest_payment,
                    roe = result.roe,
                    roi = result.roi,
                    opm = result.opm,
                    npm = result.npm,
                    cash_ratio = result.cash_ratio,
                    current_ratio = result.current_ratio,
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

        public bool SaveKeuangan(bisnis_kinerja_keuangan items)
        {
            var listResultTemp = (from x in db.bisnis_kinerja_keuangan
                                  where x.company_id == items.company_id && x.tahun == items.tahun
                                  select x).ToList();

            if (listResultTemp.Count == 0)
            {
                db.bisnis_kinerja_keuangan.Add(items);

            }
            else
            {
                bisnis_kinerja_keuangan temp = listResultTemp.First();
                temp.laba_bersih = items.laba_bersih;
                    temp.modal_sendiri = items.modal_sendiri;
                    temp.ebtit = items.ebtit;
                    temp.penyusutan = items.penyusutan;
                    temp.capital_employed = items.capital_employed;
                    temp.laba_operasi = items.laba_operasi;
                    temp.total_pendapatan_usaha = items.total_pendapatan_usaha;
                    temp.kas = items.kas;
                    temp.bank = items.bank;
                    temp.surat_berharga = items.surat_berharga;
                    temp.current_liabillities = items.current_liabillities;
                    temp.current_asset = items.current_asset;
                    temp.total_piutang_usaha = items.total_piutang_usaha;
                    temp.total_pendapatan = items.total_pendapatan;
                    temp.total_modal_sendiri = items.total_modal_sendiri;
                    temp.total_asset = items.total_asset;
                    temp.ebitda = items.ebitda;
                    temp.interest_payment = items.interest_payment;
                    temp.roe = items.roe;
                    temp.roi = items.roi;
                    temp.opm = items.opm;
                    temp.npm = items.npm;
                    temp.cash_ratio = items.cash_ratio;
                    temp.current_ratio = items.current_ratio;
                    temp.cp = items.cp;
                    temp.ito = items.ito;
                    temp.tato = items.tato;
                    temp.etta = items.etta;
                    temp.tier = items.tier;
                    temp.nkk = items.nkk;
                    temp.klasifikasi = items.klasifikasi;
                db.Entry(temp).State = EntityState.Modified;
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
        public double HitungKeuangan(double param1, double param2, double satuan)
        {
            return NilaiKinerjaKeuangan.KalkulasiData(param1, param2, satuan);
        }

        public double HitungNKK(double param1, double param2, double param3, double param4,double param5, double param6,double param7, double param8,double param9, double param10,double param11)
        {
            return NilaiKinerjaKeuangan.SumNKK(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11);
        }

        public string HitungKlasifikasi(double nkk)
        {
            return NilaiKinerjaKeuangan.KalkulasiTingkatKinerjaKeuangan(nkk);
        }

        public ActionResult KinerjaPertumbuhan(int id, int tahun)
        {
            ViewBag.id = id;
            ViewBag.tahun = tahun;
            return PartialView();
        }
        public JsonResult GetKinerjaPertumbuhan(int id, int tahun)
        {
            var listResultTemp = (from x in db.bisnis_kinerja_pertumbuhan
                                  where x.company_id == id && x.tahun == tahun
                                  select x).ToList();
            List<object> listResult = new List<object>();
            foreach (var result in listResultTemp)
            {
                listResult.Add(new
                {
                    ebit = result.ebit,
                    other_income = result.other_income,
                    total_assets_berjalan = result.total_assets_berjalan,
                    total_assets_sebelum = result.total_assets_sebelum,
                    net_sales_berjalan = result.net_sales_berjalan,
                    net_sales_sebelum = result.net_sales_sebelum,
                    net_profit_margin_berjalan = result.net_profit_margin_berjalan,
                    net_profit_margin_sebelum = result.net_profit_margin_sebelum,
                    sales_to_total_assets_berjalan = result.sales_to_total_assets_berjalan,
                    sales_to_total_assets_sebelum = result.sales_to_total_assets_sebelum,
                    net_profit_berjalan = result.net_profit_berjalan,
                    net_profit_sebelum = result.net_profit_sebelum,
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

        public bool SavePertumbuhan(bisnis_kinerja_pertumbuhan items)
        {
            var listResultTemp = (from x in db.bisnis_kinerja_pertumbuhan
                                  where x.company_id == items.company_id && x.tahun == items.tahun
                                  select x).ToList();

            if (listResultTemp.Count == 0)
            {
                db.bisnis_kinerja_pertumbuhan.Add(items);

            }
            else
            {
                bisnis_kinerja_pertumbuhan temp = listResultTemp.First();
                temp.ebit = items.ebit;
                temp.other_income = items.other_income;
                temp.total_assets_berjalan = items.total_assets_berjalan;
                temp.total_assets_sebelum = items.total_assets_sebelum;
                temp.net_sales_berjalan = items.net_sales_berjalan;
                temp.net_sales_sebelum = items.net_sales_sebelum;
                temp.net_profit_margin_berjalan = items.net_profit_margin_berjalan;
                temp.net_profit_margin_sebelum = items.net_profit_margin_sebelum;
                temp.sales_to_total_assets_berjalan = items.sales_to_total_assets_berjalan;
                temp.sales_to_total_assets_sebelum = items.sales_to_total_assets_sebelum;
                temp.net_profit_berjalan = items.net_profit_berjalan;
                temp.net_profit_sebelum = items.net_profit_sebelum;
                temp.aspg = items.aspg;
                temp.salg = items.salg;
                temp.npmg = items.npmg;
                temp.stag = items.stag;
                temp.npg = items.npg;
                temp.nkp = items.nkp;
                temp.klasifikasi = items.klasifikasi;
                db.Entry(temp).State = EntityState.Modified;
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
        public double HitungPertumbuhan2(double param1, double param2, double param3, double param4)
        {
            return NilaiKinerjaPertumbuhan.KalkulasiData(param1, param2, param3,param4);
        }

        public double HitungPertumbuhan(double param1, double param2)
        {
            return NilaiKinerjaPertumbuhan.KalkulasiData(param1, param2);
        }

        public double HitungNKP(double param1, double param2, double param3, double param4, double param5)
        {
            return NilaiKinerjaPertumbuhan.SumNKP(param1, param2, param3, param4, param5);
        }

        public string HitungKlasifikasiPertumbuhan(double nkp)
        {
            return NilaiKinerjaPertumbuhan.KalkulasiTingkatKinerjaPertumbuhan(nkp);
        }

        public ActionResult KinerjaAdministrasi(int id, int tahun)
        {
            ViewBag.id = id;
            ViewBag.tahun = tahun;
            return PartialView();
        }
        public JsonResult GetKinerjaAdministrasi(int id, int tahun)
        {
            var listResultTemp = (from x in db.bisnis_kinerja_administrasi
                                  where x.company_id == id && x.tahun == tahun
                                  select x).ToList();
            List<object> listResult = new List<object>();
            foreach (var result in listResultTemp)
            {
                listResult.Add(new
                {
                    keuangan_bulanan = result.keuangan_bulanan,
                    manajemen_bulanan = result.manajemen_bulanan,
                    keuangan_audited = result.keuangan_audited,
                    rancangan_rkap = result.rancangan_rkap,
                    nilai_keuangan_bulanan = result.nilai_keuangan_bulanan,
                    nilai_manajemen_bulanan = result.nilai_manajemen_bulanan,
                    nilai_keuangan_audited = result.nilai_keuangan_audited,
                    nilai_rancangan_rkap = result.nilai_rancangan_rkap,
                    nka = result.nka,
                    klasifikasi = result.klasifikasi
                });
            }

            return Json(listResult);
        }

        public bool SaveAdministrasi(bisnis_kinerja_administrasi items)
        {
            var listResultTemp = (from x in db.bisnis_kinerja_administrasi
                                  where x.company_id == items.company_id && x.tahun == items.tahun
                                  select x).ToList();

            if (listResultTemp.Count == 0)
            {
                db.bisnis_kinerja_administrasi.Add(items);

            }
            else
            {
                bisnis_kinerja_administrasi temp = listResultTemp.First();
                temp.keuangan_bulanan = items.keuangan_bulanan;
                temp.manajemen_bulanan = items.manajemen_bulanan;
                temp.keuangan_audited = items.keuangan_audited;
                temp.rancangan_rkap = items.rancangan_rkap;
                temp.nilai_keuangan_bulanan = items.nilai_keuangan_bulanan;
                temp.nilai_manajemen_bulanan = items.nilai_manajemen_bulanan;
                temp.nilai_keuangan_audited = items.nilai_keuangan_audited;
                temp.nilai_rancangan_rkap = items.nilai_rancangan_rkap;
                temp.nka = items.nka;
                temp.klasifikasi = items.klasifikasi;
                db.Entry(temp).State = EntityState.Modified;
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
        public double HitungLaporanBulanan(int param1)
        {
            return NilaiKinerjaAdministrasi.KalkulasiLaporanKeuanganBulanan(param1);
        }

        public double HitungLaporanManajemenBulanan(int param1)
        {
            return NilaiKinerjaAdministrasi.KalkulasiLaporanManagemenBulanan(param1);
        }

        public double HitungLaporanKeuanganAudited(int param1)
        {
            return NilaiKinerjaAdministrasi.KalkulasiLaporanKeuanganAudited(param1);
        }

        public double HitungRancanganRKAP(int param1)
        {
            return NilaiKinerjaAdministrasi.KalkulasiRancanganRKAP(param1);
        }

        public double HitungNKA(double param1, double param2, double param3, double param4)
        {
            return NilaiKinerjaAdministrasi.KalkulasiNKA(param1, param2, param3, param4);
        }

        public string HitungKlasifikasiAdministrasi(double nka)
        {
            return NilaiKinerjaAdministrasi.KalkulasiTingkatKinerjaAdministrasi(nka);
        }

        public ActionResult KinerjaKesehatan(int id, int tahun)
        {
            ViewBag.id = id;
            ViewBag.tahun = tahun;
            return PartialView();
        }

        public string HitungTingkatKesehatan(double nkk,double nkp,double nka)
        {
            return NilaiTingkatKesehatan.HitungNilaiTingkatKesehatan(nka, nkp, nkk);
        }

        public bool SaveKesehatan(bisnis_kinerja_kesehatan items)
        {
            var listResultTemp = (from x in db.bisnis_kinerja_kesehatan
                                  where x.company_id == items.company_id && x.tahun == items.tahun
                                  select x).ToList();

            if (listResultTemp.Count == 0)
            {
                db.bisnis_kinerja_kesehatan.Add(items);

            }
            else
            {
                bisnis_kinerja_kesehatan temp = listResultTemp.First();
                temp.klasifikasi = items.klasifikasi;
                db.Entry(temp).State = EntityState.Modified;
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
    }
}