using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ca.abcmufflerandhitch.Models.Products
{
    public class Exhaust : Product
    {
        [Display(Name = "Diameter (in.)")]
        public float? DiameterInches { get; set; }

        [Display(Name = "Bend (deg.)")]
        public float? DegreeBend { get; set; }
        
        public string Material { get; set; }
    }
}