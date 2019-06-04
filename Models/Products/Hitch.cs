using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ca.abcmufflerandhitch.Models.Products
{
    public class Hitch : Product
    {
        [Display(Name = "Hitch Class")]
        public int HitchClass { get; set; }

        [Display(Name = "Weight Rating (lbs.)")]
        public int WeightRatingPounds { get; set; }
    }
}
