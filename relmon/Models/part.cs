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
    public partial class part
    {
        public part()
        {
            this.equipment_part = new HashSet<equipment_part>();
        }
    
        public int id { get; set; }
        public string tag_number { get; set; }
        public string nama { get; set; }
        public Nullable<double> assurance_level { get; set; }
        public string vendor { get; set; }
        public Nullable<int> warranty { get; set; }
    
        public virtual ICollection<equipment_part> equipment_part { get; set; }
    }
    
}
