using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ca.abcmufflerandhitch.Models
{
    public abstract class Product
    {

        public int ProductID { get; set; }
        public int BrandID { get; set; }
        public int ProductTypeID { get; set; }

        [Required, Display(Name = "Name")]
        public string ProductName { get; set; }

        [Required, Display(Name = "Desc.")]
        public string ProductDescription { get; set; }

        [Display(Name = "Vehicles")]
        public string VehicleModel { get; set; }

        [Display(Name = "Code")]
        public string ProductCode { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Display(Name = "In-Stock")]
        public Boolean InStock { get; set; }

        // Navigation
        public virtual Brand Brand { get; set; }

        public virtual ProductType ProductType { get; set; }

    }
}
