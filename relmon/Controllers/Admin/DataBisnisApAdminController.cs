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

    }
}