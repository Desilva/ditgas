using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace relmon.Models
{
    public class EmployeeWrapper
    {
        public int id { get; set; }
        public string nama { get; set; }
        public string no_pekerja { get; set; }
        public string jabatan { get; set; }
        public string lokasi { get; set; }
        public string direktorat { get; set; }
        public string tempat_lahir { get; set; }
        public string jenis_kelamin { get; set; }
        public string agama { get; set; }
        public string npwp { get; set; }
        public DateTime tanggal_dinas_aktif { get; set; }
        public string notice { get; set; }
    }
}