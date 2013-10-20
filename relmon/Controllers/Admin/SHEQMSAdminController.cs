using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using relmon.Models;
using Telerik.Web.Mvc;
using System.Data;
using relmon.Controllers.Utilities;
using relmon.Utilities;

namespace relmon.Controllers.Admin
{
    public class SHEQMSAdminController : Controller
    {
        private RelmonStoreEntities db = new RelmonStoreEntities();
        //
        // GET: /SHEQMS/

        public ActionResult Index()
        {
            return View();
        }

        #region Kebijakan HSE
        public ActionResult KebijakanHSE()
        {
            return PartialView();
        }

        [HttpPost]
        public string UploadHSE(sheqms_kebijakan_HSE sheqms_kebijakan_HSE)
        {
            var result = (from x in db.sheqms_safety_talk
                          where x.filename == sheqms_kebijakan_HSE.filename
                          select x
                        ).ToList();

            if (result.Count == 0)
            {
                sheqms_kebijakan_HSE.date = DateTime.Now;
                db.sheqms_kebijakan_HSE.Add(sheqms_kebijakan_HSE);
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
            else
            {
                return "exist";
            }

        }

        [HttpPost]
        public string removeTemp(String name , String dir)
        {
            UploadController upload = new UploadController();

            string[] file = new string[1];
            file[0] = name;
            if (!string.IsNullOrWhiteSpace(file[0]))
            {
                upload.Remove(file, dir);
            }

            return "Success";
        }

        [GridAction]
        public ActionResult _SelectHSE()
        {
            return bindingHSE();
        }

        //
        // Ajax delete binding
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteHSE(int id) //row_id
        {

            //delete dr database
            var tempResult = (from x in db.sheqms_kebijakan_HSE
                              where x.id == id
                              select x).ToList();
            if (tempResult.Count != 0)
            {
                sheqms_kebijakan_HSE result = tempResult.First();

                //delete file
                UploadController upload = new UploadController();
                string[] file = new string[1];
                file[0] = result.filename;
                if (!string.IsNullOrWhiteSpace(file[0]))
                {
                    upload.Remove(file, "SHE-QMS\\Kebijakan HSE");
                }

                db.sheqms_kebijakan_HSE.Remove(result);
                db.SaveChanges();
            }

            return bindingHSE();
        }

        //select data user
        protected ViewResult bindingHSE()
        {
            List<sheqms_kebijakan_HSE> result = (from x in db.sheqms_kebijakan_HSE
                                               select x).ToList();

            return View(new GridModel<sheqms_kebijakan_HSE>
            {
                Data = result
            });
        }


        public ActionResult GetDLHSE(int id)
        {
            sheqms_kebijakan_HSE x = db.sheqms_kebijakan_HSE.Find(id);
            try
            {
                var fs = System.IO.File.OpenRead(Server.MapPath("~/Upload/SHE-QMS/Kebijakan HSE" + "/" + x.filename));
                string fileType = MyTools.getFileType(x.filename);
                return File(fs, fileType, x.filename);
            }
            catch
            {
                throw new HttpException(404, "Couldn't find " + x.filename);
            }
        }
#endregion

        # region Safety Talk

        public ActionResult SafetyTalk()
        {
            return PartialView();
        }

        [HttpPost]
        public string UploadSafety(sheqms_safety_talk sheqms_safety_talk)
        {
            var result = (from x in db.sheqms_safety_talk
                          where x.filename == sheqms_safety_talk.filename
                          select x
                        ).ToList();

            if (result.Count == 0)
            {
                sheqms_safety_talk.date = DateTime.Now;
                db.sheqms_safety_talk.Add(sheqms_safety_talk);
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
            else
            {
                return "exist";
            }

        }

        [GridAction]
        public ActionResult _SelectSafety()
        {
            return binding();
        }

        //
        // Ajax delete binding
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteSafety(int id) //row_id
        {

            //delete dr database
            var tempResult = (from x in db.sheqms_safety_talk
                              where x.id == id
                              select x).ToList();
            if (tempResult.Count != 0)
            {
                sheqms_safety_talk result = tempResult.First();
                
                //delete file
                UploadController upload = new UploadController();
                string[] file = new string[1];
                file[0] = result.filename;
                if (!string.IsNullOrWhiteSpace(file[0]))
                {
                    upload.Remove(file, "SHE-QMS\\Safety Talk");
                }

                db.sheqms_safety_talk.Remove(result);
                db.SaveChanges();
            }

            return binding();
        }

        //select data user
        protected ViewResult binding()
        {
            List<sheqms_safety_talk> result = (from x in db.sheqms_safety_talk
                                                    select x).ToList();

            return View(new GridModel<sheqms_safety_talk>
            {
                Data = result
            });
        }


        public ActionResult GetDL(int id)
        {
            sheqms_safety_talk x = db.sheqms_safety_talk.Find(id);
            try
            {
                var fs = System.IO.File.OpenRead(Server.MapPath("~/Upload/SHE-QMS/Safety Talk" + "/" + x.filename));
                string fileType = MyTools.getFileType(x.filename);
                return File(fs, fileType, x.filename);
            }
            catch
            {
                throw new HttpException(404, "Couldn't find " + x.filename);
            }
        }

        

        # endregion

        public int GetId(int id)
        {
            return id;
        }
    }
}
