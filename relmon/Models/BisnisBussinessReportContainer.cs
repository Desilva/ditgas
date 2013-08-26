using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace relmon.Models
{
    public class BisnisBussinessReportContainer
    {
        public int id { get; set; }
        public int company_id { get; set; }
        public string content { get; set; }
        public string bulan { get; set; }
        public int tahun { get; set; }
        public string deskripsi { get; set; }
    }
}