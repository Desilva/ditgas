//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace relmon.Models
{
    public partial class bisnis_product
    {
        public int id { get; set; }
        public int company_id { get; set; }
        public string product { get; set; }
        public int tahun { get; set; }
        public int januari { get; set; }
        public int februari { get; set; }
        public int maret { get; set; }
        public int april { get; set; }
        public int mei { get; set; }
        public int juni { get; set; }
        public int juli { get; set; }
        public int agustus { get; set; }
        public int september { get; set; }
        public int oktober { get; set; }
        public int november { get; set; }
        public int desember { get; set; }
    
        public virtual company company { get; set; }
    }
    
}