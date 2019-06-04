using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ca.abcmufflerandhitch.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ABCMufflerAndHitch.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductContext _context;

        public ProductsController(ProductContext context)
        {
            _context = context;
        }
        public IActionResult Manage()
        {
            return View();
        }

        public async Task<IActionResult> Exhaust()
        {
            var productContext = _context.Exhausts.Include(e => e.Brand).Include(e => e.ProductType);

            ViewData["Magnaflow"] = _context.Exhausts.Where(e => e.Brand.BrandName == "Magnaflow" && e.ProductType.ProductTypeName == "Muffler");
            ViewData["Flowmaster"] = _context.Exhausts.Where(e => e.Brand.BrandName == "Flowmaster" && e.ProductType.ProductTypeName == "Muffler");
            ViewData["CatalyticConverter"] = _context.Exhausts.Where(e => e.ProductType.ProductTypeName == "Catalytic Converter");
            ViewData["ExhaustKit"] = _context.Exhausts.Where(e => e.ProductType.ProductTypeName == "Exhaust Kit");

            return View(await productContext.ToListAsync());
        }
    }
}