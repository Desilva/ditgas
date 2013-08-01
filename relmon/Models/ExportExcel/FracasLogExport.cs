using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace relmon.Models.ExportExcel
{
    public class FracasLogExport
    {
        public DateTime? dateTimeStop { get; set; }
        public DateTime? dateTimeStart { get; set; }
        public string unitName { get; set; }
        public string areaName { get; set; }
        public double? durasi { get; set; }
        public double? downtime { get; set; }
    }
}