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
    public partial class bisnis_kinerja_pertumbuhan
    {
        public int id { get; set; }
        public int company_id { get; set; }
        public int tahun { get; set; }
        public double ebit { get; set; }
        public double other_income { get; set; }
        public double total_assets_berjalan { get; set; }
        public double total_assets_sebelum { get; set; }
        public double net_sales_berjalan { get; set; }
        public double net_sales_sebelum { get; set; }
        public double net_profit_margin_berjalan { get; set; }
        public double net_profit_margin_sebelum { get; set; }
        public double sales_to_total_assets_berjalan { get; set; }
        public double sales_to_total_assets_sebelum { get; set; }
        public double net_profit_berjalan { get; set; }
        public double net_profit_sebelum { get; set; }
        public double aspg { get; set; }
        public double salg { get; set; }
        public double npmg { get; set; }
        public double stag { get; set; }
        public double npg { get; set; }
        public double nkp { get; set; }
        public string klasifikasi { get; set; }
    
        public virtual company company { get; set; }
    }
    
}