using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ca.abcmufflerandhitch.Data;
using ca.abcmufflerandhitch.Models.Products;

namespace ABCMufflerAndHitch.Controllers
{
    public class AccessoriesController : Controller
    {
        private readonly ProductContext _context;

        public AccessoriesController(ProductContext context)
        {
            _context = context;
        }

        // GET: Accessories
        public async Task<IActionResult> Index()
        {
            var productContext = _context.Accessories.Include(a => a.Brand).Include(a => a.ProductType);
            return View(await productContext.ToListAsync());
        }

        // GET: Accessories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessory = await _context.Accessories
                .Include(a => a.Brand)
                .Include(a => a.ProductType)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (accessory == null)
            {
                return NotFound();
            }

            return View(accessory);
        }

        // GET: Accessories/Create
        public IActionResult Create()
        {
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandName");
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes, "ProductTypeID", "ProductTypeName");
            return View();
        }

        // POST: Accessories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,BrandID,ProductTypeID,ProductName,ProductDescription,VehicleModel,ProductCode,Price,InStock")] Accessory accessory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accessory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandName", accessory.BrandID);
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes, "ProductTypeID", "ProductTypeName", accessory.ProductTypeID);
            return View(accessory);
        }

        // GET: Accessories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessory = await _context.Accessories.FindAsync(id);
            if (accessory == null)
            {
                return NotFound();
            }
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandName", accessory.BrandID);
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes, "ProductTypeID", "ProductTypeName", accessory.ProductTypeID);
            return View(accessory);
        }

        // POST: Accessories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,BrandID,ProductTypeID,ProductName,ProductDescription,VehicleModel,ProductCode,Price,InStock")] Accessory accessory)
        {
            if (id != accessory.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accessory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccessoryExists(accessory.ProductID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandName", accessory.BrandID);
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes, "ProductTypeID", "ProductTypeName", accessory.ProductTypeID);
            return View(accessory);
        }

        // GET: Accessories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessory = await _context.Accessories
                .Include(a => a.Brand)
                .Include(a => a.ProductType)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (accessory == null)
            {
                return NotFound();
            }

            return View(accessory);
        }

        // POST: Accessories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accessory = await _context.Accessories.FindAsync(id);
            _context.Accessories.Remove(accessory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccessoryExists(int id)
        {
            return _context.Accessories.Any(e => e.ProductID == id);
        }
    }
}
