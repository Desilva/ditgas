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
    public partial class equipment
    {
        public equipment()
        {
            this.equipment_event = new HashSet<equipment_event>();
            this.equipment_paf = new HashSet<equipment_paf>();
            this.equipment_part = new HashSet<equipment_part>();
            this.equipment_readiness_nav = new HashSet<equipment_readiness_nav>();
        }
    
        public int id { get; set; }
        public int id_equipment_group { get; set; }
        public string tag_num { get; set; }
        public string nama { get; set; }
        public Nullable<byte> id_category { get; set; }
        public string pdf { get; set; }
        public Nullable<double> tetha { get; set; }
        public Nullable<double> beta { get; set; }
        public Nullable<double> mean { get; set; }
        public Nullable<double> varian { get; set; }
        public Nullable<double> lamda { get; set; }
        public Nullable<int> id_discipline { get; set; }
        public Nullable<int> mtbf { get; set; }
        public Nullable<int> mttr { get; set; }
        public Nullable<int> mdt { get; set; }
        public Nullable<byte> status { get; set; }
        public string method { get; set; }
        public Nullable<int> econ { get; set; }
        public string ram_crit { get; set; }
        public Nullable<System.DateTime> installed_date { get; set; }
        public Nullable<System.DateTime> obsolete_date { get; set; }
        public Nullable<int> warranty { get; set; }
        public string vendor { get; set; }
        public Nullable<int> id_tag_type { get; set; }
        public Nullable<byte> status_read_nav { get; set; }
        public string data_sheet_path { get; set; }
        public Nullable<System.DateTime> sertifikasi { get; set; }
    
        public virtual discipline discipline { get; set; }
        public virtual ICollection<equipment_event> equipment_event { get; set; }
        public virtual equipment_groups equipment_groups { get; set; }
        public virtual ICollection<equipment_paf> equipment_paf { get; set; }
        public virtual ICollection<equipment_part> equipment_part { get; set; }
        public virtual ICollection<equipment_readiness_nav> equipment_readiness_nav { get; set; }
        public virtual tag_types tag_types { get; set; }
    }
    
}
