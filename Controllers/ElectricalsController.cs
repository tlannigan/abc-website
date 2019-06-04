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
    public class ElectricalsController : Controller
    {
        private readonly ProductContext _context;

        public ElectricalsController(ProductContext context)
        {
            _context = context;
        }

        // GET: Electricals
        public async Task<IActionResult> Index()
        {
            var productContext = _context.Electricals.Include(e => e.Brand).Include(e => e.ProductType);
            return View(await productContext.ToListAsync());
        }

        // GET: Electricals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electrical = await _context.Electricals
                .Include(e => e.Brand)
                .Include(e => e.ProductType)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (electrical == null)
            {
                return NotFound();
            }

            return View(electrical);
        }

        // GET: Electricals/Create
        public IActionResult Create()
        {
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandName");
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes, "ProductTypeID", "ProductTypeName");
            return View();
        }

        // POST: Electricals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaxTrailerAxles,EstInstallTime,ProductID,BrandID,ProductTypeID,ProductName,ProductDescription,VehicleModel,ProductCode,Price,InStock")] Electrical electrical)
        {
            if (ModelState.IsValid)
            {
                _context.Add(electrical);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandName", electrical.BrandID);
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes, "ProductTypeID", "ProductTypeName", electrical.ProductTypeID);
            return View(electrical);
        }

        // GET: Electricals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electrical = await _context.Electricals.FindAsync(id);
            if (electrical == null)
            {
                return NotFound();
            }
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandName", electrical.BrandID);
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes, "ProductTypeID", "ProductTypeName", electrical.ProductTypeID);
            return View(electrical);
        }

        // POST: Electricals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaxTrailerAxles,EstInstallTime,ProductID,BrandID,ProductTypeID,ProductName,ProductDescription,VehicleModel,ProductCode,Price,InStock")] Electrical electrical)
        {
            if (id != electrical.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(electrical);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElectricalExists(electrical.ProductID))
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
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandName", electrical.BrandID);
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes, "ProductTypeID", "ProductTypeName", electrical.ProductTypeID);
            return View(electrical);
        }

        // GET: Electricals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electrical = await _context.Electricals
                .Include(e => e.Brand)
                .Include(e => e.ProductType)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (electrical == null)
            {
                return NotFound();
            }

            return View(electrical);
        }

        // POST: Electricals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var electrical = await _context.Electricals.FindAsync(id);
            _context.Electricals.Remove(electrical);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElectricalExists(int id)
        {
            return _context.Electricals.Any(e => e.ProductID == id);
        }
    }
}
