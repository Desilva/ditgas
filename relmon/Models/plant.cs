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
    public partial class plant
    {
        public plant()
        {
            this.focs = new HashSet<foc>();
        }
    
        public int id { get; set; }
        public string nama { get; set; }
    
        public virtual ICollection<foc> focs { get; set; }
    }
    
}
