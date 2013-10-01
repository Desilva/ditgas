using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using relmon.Utilities;
using System.Collections.Specialized;

namespace relmon.Controllers.Admin
{
    public class LoadExcelController : Controller
    {
        //
        // GET: /LoadExcel/

        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult DataMaster()
        {
            return PartialView();
        }

        [HttpPost]
        public string Index(HttpPostedFileBase userfile)
        {
            // extract only the fielname
            var fileName = Path.GetFileName(userfile.FileName);
            string err = "";
            // store the file inside ~/App_Data/uploads folder
            var path = Path.Combine(Server.MapPath("~/Content/template/"), fileName);           
            userfile.SaveAs(path);
            err = SaveData(path);

            return err;
        }


        private string SaveData(string path)
        {
            ExcelReader excel = new ExcelReader();
            List<string> err = excel.LoadData(path);
            return excel.generateError(err);
        }

        public ActionResult Template()
        {
            // extract only the fielname
            NameValueCollection file = Request.Params;

            var fileName = file[0];
            // store the file inside ~/App_Data/uploads folder
            var path = Path.Combine(Server.MapPath("~/Content/template"), fileName);
            string[] filenameArray = fileName.Split('.');
            string contentType = "application/" + filenameArray[1];

            return File(path, contentType, fileName);
        }
    }
    
}
