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
    public partial class users_pengalaman_kerja
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string nama_perusahaan { get; set; }
        public string jabatan { get; set; }
        public string lokasi { get; set; }
        public Nullable<System.DateTime> tgl_awal { get; set; }
        public Nullable<System.DateTime> tgl_akhir { get; set; }

        public virtual user user { internal get; set; }
    }
    
}