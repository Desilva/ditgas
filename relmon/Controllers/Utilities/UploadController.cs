using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace relmon.Controllers.Utilities
{
    public class UploadController : Controller
    {
        public ActionResult Save(IEnumerable<HttpPostedFileBase> attachments,string dir)
        {
            

            // The Name of the Upload component is "attachments" 
            foreach (var file in attachments)
            {
                // Some browsers send file names with full path. This needs to be stripped.
                var fileName = Path.GetFileName(file.FileName);
                //var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                var physicalPath = Path.Combine(Server.MapPath("~/Upload"), dir, fileName);
                var folderPath = Path.Combine(Server.MapPath("~/Upload"), dir);
                if (!System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                }
                // save file
                file.SaveAs(physicalPath);
                
            }
            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult Save2(IEnumerable<HttpPostedFileBase> attachments, string dir)
        {

            var fileName = "";
            // The Name of the Upload component is "attachments" 
            foreach (var file in attachments)
            {
                // Some browsers send file names with full path. This needs to be stripped.
                var fileNameOri = Path.GetFileName(file.FileName);
                fileName = fileNameOri;

                var physicalPath = Path.Combine(Server.MapPath("~/Upload"), dir, fileName);
                var folderPath = Path.Combine(Server.MapPath("~/Upload"), dir);
                bool exist = true;
                int num = 1;
                while(exist){
                    if (System.IO.File.Exists(physicalPath))
                    {
                        var split = fileNameOri.Split('.');
                        fileName = split[0] + "("+num+")." + split[1];
                        physicalPath = Path.Combine(Server.MapPath("~/Upload"), dir, fileName);
                        num++;
                    }
                    else
                    {
                        exist = false;
                    }
                }
                
                if (!System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                }
                // save file
                file.SaveAs(physicalPath);

            }
            // Return an empty string to signify success
            return Json(new { newFilename = fileName }); ;
        }

        public ActionResult Remove(string[] fileNames, string dir)
        {
            // The parameter of the Remove action must be called "fileNames"
            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    string home = System.AppDomain.CurrentDomain.BaseDirectory;
                    string physicalPath = home + "Upload\\" + dir +"\\"+ fileName;

                    //var physicalPath = Path.Combine(test, dir, fileName);

                    // TODO: Verify user permissions
                    if (System.IO.File.Exists(physicalPath))
                    {
                        //remove file
                        System.IO.File.Delete(physicalPath);
                    }
                }
            }
            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult Move(string filename, string sourceDir,string destDir)
        {
            if (!string.IsNullOrWhiteSpace(filename) && !filename.Equals("null"))
            {
                var name = Path.GetFileName(filename);
                var sourcePath = Path.Combine(Server.MapPath("~/Upload"), sourceDir, name);
                var destPath = Path.Combine(Server.MapPath("~/Upload"), destDir, name);
                if (!System.IO.Directory.Exists(Path.Combine(Server.MapPath("~/Upload"), destDir)))
                {
                    System.IO.Directory.CreateDirectory(Path.Combine(Server.MapPath("~/Upload"), destDir));
                }
                if(!System.IO.Directory.Exists(destPath)){
                    System.IO.File.Move(sourcePath, destPath);
                }
                    
                
                
            }
            return Content("");
        }

        //public ActionResult Copy(string filename, string sourceDir, string destDir)
        //{
        //    if (!string.IsNullOrWhiteSpace(filename) && !filename.Equals("null"))
        //    {
        //        var name = Path.GetFileName(filename);
        //        var sourcePath = Path.Combine(Config.upload, sourceDir, name);
        //        var destPath = Path.Combine(Config.upload, destDir, name);
                
        //            System.IO.File.Copy(sourcePath, destPath);
        //    }
        //    return Content("");
        //}

        //public ActionResult CleanMove(string filename, string sourceDir, string destDir)
        //{

        //    var destFolder = Path.Combine(Config.upload, destDir);

        //    if (System.IO.Directory.Exists(Path.Combine(Config.upload, destDir)))
        //    {
        //        System.IO.DirectoryInfo getDir = new DirectoryInfo(destFolder);
        //        foreach (FileInfo file in getDir.GetFiles())
        //        {
        //            file.Delete();
        //        }
        //    }
        //    if (!string.IsNullOrWhiteSpace(filename))
        //    {
        //        this.Move(filename, sourceDir, destDir);
        //    }
        //        return Content("");
        //}
    }
}
