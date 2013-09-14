using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using relmon.Models;

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
    }
}
