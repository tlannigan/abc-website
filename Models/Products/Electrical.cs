using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ca.abcmufflerandhitch.Models.Products
{
    public class Electrical : Product
    {
        [Display(Name = "Maximum Trailer Axles")]
        public int MaxTrailerAxles { get; set; }

        [Display(Name = "Estimated Install Time")]
        public float EstInstallTime { get; set; }
    }
}
