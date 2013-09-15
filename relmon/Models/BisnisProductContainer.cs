using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace relmon.Models
{
    public class BisnisProductContainer
    {
        public int id { get; set; }
        public int company_id { get; set; }
        public string product { get; set; }
        [DataType("Integer")]
        public int januari { get; set; }
        [DataType("Integer")]
        public int februari { get; set; }
        [DataType("Integer")]
        public int maret { get; set; }
        [DataType("Integer")]
        public int april { get; set; }
        [DataType("Integer")]
        public int mei { get; set; }
        [DataType("Integer")]
        public int juni { get; set; }
        [DataType("Integer")]
        public int juli { get; set; }
        [DataType("Integer")]
        public int agustus { get; set; }
        [DataType("Integer")]
        public int september { get; set; }
        [DataType("Integer")]
        public int oktober { get; set; }
        [DataType("Integer")]
        public int november { get; set; }
        [DataType("Integer")]
        public int desember { get; set; }
    }
}