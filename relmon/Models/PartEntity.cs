using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace relmon.Models
{
    public class PartEntity
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string tag_number { get; set; }
        [Required]
        public string nama { get; set; }
        public Nullable<double> assurance_level { get; set; }
        public string vendor { get; set; }
        public Nullable<int> warranty { get; set; }
    }
}