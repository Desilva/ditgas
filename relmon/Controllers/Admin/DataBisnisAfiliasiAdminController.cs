using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using relmon.Utilities;
using relmon.Models;

namespace relmon.Controllers.Admin
{
    public class DataBisnisAfiliasiAdminController : DataBisnisApAdminController
    {
        public override ActionResult Index()
        {
            ViewBag.selectedMenu = "databisnis";
            
            return PartialView();
        }

        public override bool checkACL(string nama)
        {
            return ACLHandler.isUserAllowedTo(PageItem.DataBisnis_AfiliasiCompany.name + nama, (int)Session["id"], "view");
        }
    }
}
