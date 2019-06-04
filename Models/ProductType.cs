using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ca.abcmufflerandhitch.Models
{
    public class ProductType
    {
        public int ProductTypeID { get; set; }

        [Required, Display(Name = "Type")]
        public string ProductTypeName { get; set; }

        [Required]
        public CategoryEnum Category { get; set; }
    }
    public enum CategoryEnum
    {
        [Description("Accessory")]
        ACCESSORY,

        [Description("Exhaust")]
        EXHAUST,

        [Description("Towing")]
        TOWING
    }
}
