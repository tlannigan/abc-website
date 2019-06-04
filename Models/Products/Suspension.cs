using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ca.abcmufflerandhitch.Models.Products
{
    public class Suspension : Product
    {
        [Display(Name = "Weight Rating (lbs.)")]
        public int WeightRatingPounds { get; set; }
    }
}
