using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace relmon.Utilities
{
    public class MyTools
    {
        public static string getFileType(string filename) {
            string[] temp = filename.Split('.');
            switch(temp.Last()){
                case "pdf":
                    return "application/pdf";
                case "xls":
                    return "application/msexcel";
                case "xlsx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case "docx":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                default :
                    break;
            };
            return "";
        }
    }
}