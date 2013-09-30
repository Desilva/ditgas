using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using relmon.Models;
using System.Data;
using Telerik.Web.Mvc;
using relmon.Utilities;
using relmon.Controllers.Utilities;

namespace relmon.Controllers.Admin
{
    public class AturanRegulasiAdminController : Controller
    {
        private RelmonStoreEntities db = new RelmonStoreEntities();
        //
        // GET: /AturanRegulasiAdmin/

        public ActionResult Index()
        {
            return PartialView();
        }

        

        #region Pertamina
        public ActionResult Pertamina()
        {
            return PartialView();
        }


        [HttpPost]
        public string UploadAturan(string filename)
        {
            aturan newAturan = new aturan();
            newAturan.create_by = Session["name"].ToString();
            newAturan.date = DateTime.Now;
            newAturan.filename = filename;
            newAturan.tipe = "pertamina";
            db.aturan.Add(newAturan);
            try
            {
                db.SaveChanges();
                return "true";
            }
            catch (Exception a)
            {
                Console.Write(a.Message);
                return "false";
            }
        }

        [GridAction]
        public ActionResult _SelectPertamina()
        {
            return bindingPertamina();
        }

        //
        // Ajax delete binding
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeletePertamina(int id) //row_id
        {
            int aclId = (int)Session["id"];
            if (ACLHandler.isUserAllowedTo(PageItem.AturanRegulasi_Pertamina.name, aclId, "delete")){
                //delete dr database
                var tempResult = (from x in db.aturan
                                  where x.id == id
                                  select x).ToList();
                if (tempResult.Count != 0)
                {
                    aturan result = tempResult.First();

                    //delete file
                    UploadController upload = new UploadController();
                    string[] file = new string[1];
                    file[0] = result.filename;
                    if (!string.IsNullOrWhiteSpace(file[0]))
                    {
                        upload.Remove(file, "Aturan\\Pertamina");
                    }

                    db.aturan.Remove(result);
                    db.SaveChanges();
                }
            }
            return bindingPertamina();
        }

        //select data user
        protected ViewResult bindingPertamina()
        {
            List<aturan> result = (from x in db.aturan
                                   where x.tipe == "pertamina"
                                   select x).ToList();

            return View(new GridModel<aturan>
            {
                Data = result
            });
        }


        public ActionResult DownloadAturan(int id)
        {
            aturan x = db.aturan.Find(id);
            try
            {
                var fs = System.IO.File.OpenRead(Server.MapPath("~/Upload/Aturan\\Pertamina/" + x.filename));
                string fileType = MyTools.getFileType(x.filename);
                return File(fs, fileType, x.filename);
            }
            catch
            {
                throw new HttpException(404, "Couldn't find " + x.filename);
            }
        }

        public int GetId(int id)
        {
            return id;
        }
        #endregion

        #region Regulasi

        public ActionResult Regulasi()
        {
            var result = (from x in db.aturan
                          where x.tipe == "regulasi"
                          select x
                         ).ToList();
            if (result.Count == 0)
            {
                aturan e = new aturan();
                e.filename = "";
                return PartialView(e);
            }
            else
            {
                aturan e = result.First();
                return PartialView(e);

            }
        }


        [HttpPost]
        public bool UploadRegulasi(string filename)
        {
            var result = (from x in db.aturan
                          where x.tipe == "regulasi"
                          select x
                         ).ToList();

            if (result.Count == 0)
            {
                aturan items = new aturan();
                items.date = DateTime.Now;
                items.tipe = "regulasi";
                items.create_by = Session["name"].ToString();
                items.filename = filename;
                db.aturan.Add(items);

            }
            else
            {
                aturan items = result.First();
                items.date = DateTime.Now;
                items.tipe = "regulasi";
                items.create_by = Session["name"].ToString();
                items.filename = filename;
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

        public bool DeleteRegulasi()
        {
            var result = (from x in db.aturan
                          where x.tipe == "regulasi"
                          select x
                         ).ToList();

            if (result.Count != 0)
            {
                aturan items = result.First();
                db.aturan.Remove(items);
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

        #region Charter

        public ActionResult Charter()
        {
            var result = (from x in db.aturan
                          where x.tipe == "charter"
                          select x
                         ).ToList();
            if (result.Count == 0)
            {
                aturan e = new aturan();
                e.filename = "";
                return PartialView(e);
            }
            else
            {
                aturan e = result.First();
                return PartialView(e);

            }
        }


        [HttpPost]
        public bool UploadCharter(string filename)
        {
            var result = (from x in db.aturan
                          where x.tipe == "charter"
                          select x
                         ).ToList();

            if (result.Count == 0)
            {
                aturan items = new aturan();
                items.date = DateTime.Now;
                items.tipe = "charter";
                items.create_by = Session["name"].ToString();
                items.filename = filename;
                db.aturan.Add(items);

            }
            else
            {
                aturan items = result.First();
                items.date = DateTime.Now;
                items.tipe = "charter";
                items.create_by = Session["name"].ToString();
                items.filename = filename;
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

        public bool DeleteCharter()
        {
            var result = (from x in db.aturan
                          where x.tipe == "charter"
                          select x
                         ).ToList();

            if (result.Count != 0)
            {
                aturan items = result.First();
                db.aturan.Remove(items);
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
