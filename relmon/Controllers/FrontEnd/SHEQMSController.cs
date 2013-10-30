using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using relmon.Models;
using relmon.Utilities;

namespace relmon.Controllers.FrontEnd
{
    public class SHEQMSController : Controller
    {
        private RelmonStoreEntities db = new RelmonStoreEntities();
        //
        // GET: /SHEQMS/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult KebijakanHSE()
        {
            return View();
        }

        [GridAction]
        public ActionResult _SelectHSE()
        {
            List<sheqms_kebijakan_HSE> result = (from x in db.sheqms_kebijakan_HSE
                                                 where x.tipe == "Video"
                                               select x).ToList();

            return View(new GridModel<sheqms_kebijakan_HSE>
            {
                Data = result
            });
        }

        [GridAction]
        public ActionResult _SelectHSE2()
        {
            List<sheqms_kebijakan_HSE> result = (from x in db.sheqms_kebijakan_HSE
                                                 where x.tipe == "File"
                                               select x).ToList();

            return View(new GridModel<sheqms_kebijakan_HSE>
            {
                Data = result
            });
        }

        public ActionResult SafetyTalk()
        {
            return View();
        }

        [GridAction]
        public ActionResult _SelectSafety()
        {
            List<sheqms_safety_talk> result = (from x in db.sheqms_safety_talk
                                               where x.tipe == "Video"
                                               select x).ToList();

            return View(new GridModel<sheqms_safety_talk>
            {
                Data = result
            });
        }

        [GridAction]
        public ActionResult _SelectSafety2()
        {
            List<sheqms_safety_talk> result = (from x in db.sheqms_safety_talk
                                               where x.tipe == "File"
                                               select x).ToList();

            return View(new GridModel<sheqms_safety_talk>
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
    }
}
