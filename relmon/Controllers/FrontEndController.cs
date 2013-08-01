using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using relmon.Models;

namespace relmon.Controllers
{
    public class FrontEndController : Controller
    {

        private RelmonStoreEntities db = new RelmonStoreEntities();
        //
        // GET: /FrontEnd/

        public ActionResult Index()
        {         
            ViewBag.Message = "Welcome to Admin Reliability Monitoring!";
            //CalculateEventData ced = new CalculateEventData(185);
            return View();
        }

    }
}
