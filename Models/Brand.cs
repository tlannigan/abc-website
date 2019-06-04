using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ca.abcmufflerandhitch.Models
{
    public class Brand
    {

        public int BrandID { get; set; }

        [Required, Display(Name = "Brand Name")]
        public string BrandName { get; set; }
    }
}
