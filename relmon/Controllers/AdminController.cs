using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using relmon.Models;


namespace relmon.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private RelmonStoreEntities db = new RelmonStoreEntities();

        public ActionResult Index()
        {
            if (Session.Count != 0)
            {
                //if (((int)Session["role"]) < 0)
                {
                    ViewBag.Message = "Welcome to Admin Sistem Informasi!";
                    string role = Session["role"].ToString();
                    if (role == "-1")
                    {
                        ViewBag.stat = "admin";
                    }
                    else
                    {
                        ViewBag.stat = "admin2";
                    }
                    /*st<plant> plant = db.plants.ToList();
                    plant = plant.OrderBy(a => a.nama).ToList();
                    foreach (plant p in plant)
                    {
                        p.focs = p.focs.OrderBy(a => a.nama).ToList();
                    }*/

                    return View(db.companies);
                }
            }

            return RedirectToAction("LogOn", "Account");
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
