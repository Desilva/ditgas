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
    public partial class failure_modes
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Nullable<int> id_tag_type { get; set; }
    
        public virtual tag_types tag_types { get; set; }
    }
    
}
