using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace relmon.Controllers.Admin
{
    public class DataBisnisAfiliasiAdminController : DataBisnisApAdminController
    {
        public override ActionResult Index()
        {
            ViewBag.selectedMenu = "databisnis";
            
            return PartialView();
        }
    }
}
