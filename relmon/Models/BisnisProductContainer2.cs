using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace relmon.Models
{
    public class BisnisProductContainer2
    {
        public string product { get; set; }
        public string month { get; set; }
        public int sum { get; set; }
    }
}