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
    public class SuspensionsController : Controller
    {
        private readonly ProductContext _context;

        public SuspensionsController(ProductContext context)
        {
            _context = context;
        }

        // GET: Suspensions
        public async Task<IActionResult> Index()
        {
            var productContext = _context.Suspensions.Include(s => s.Brand).Include(s => s.ProductType);
            return View(await productContext.ToListAsync());
        }

        // GET: Suspensions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suspension = await _context.Suspensions
                .Include(s => s.Brand)
                .Include(s => s.ProductType)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (suspension == null)
            {
                return NotFound();
            }

            return View(suspension);
        }

        // GET: Suspensions/Create
        public IActionResult Create()
        {
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandName");
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes, "ProductTypeID", "ProductTypeName");
            return View();
        }

        // POST: Suspensions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WeightRatingPounds,ProductID,BrandID,ProductTypeID,ProductName,ProductDescription,VehicleModel,ProductCode,Price,InStock")] Suspension suspension)
        {
            if (ModelState.IsValid)
            {
                _context.Add(suspension);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandName", suspension.BrandID);
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes, "ProductTypeID", "ProductTypeName", suspension.ProductTypeID);
            return View(suspension);
        }

        // GET: Suspensions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suspension = await _context.Suspensions.FindAsync(id);
            if (suspension == null)
            {
                return NotFound();
            }
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandName", suspension.BrandID);
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes, "ProductTypeID", "ProductTypeName", suspension.ProductTypeID);
            return View(suspension);
        }

        // POST: Suspensions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WeightRatingPounds,ProductID,BrandID,ProductTypeID,ProductName,ProductDescription,VehicleModel,ProductCode,Price,InStock")] Suspension suspension)
        {
            if (id != suspension.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suspension);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuspensionExists(suspension.ProductID))
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
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandName", suspension.BrandID);
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes, "ProductTypeID", "ProductTypeName", suspension.ProductTypeID);
            return View(suspension);
        }

        // GET: Suspensions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suspension = await _context.Suspensions
                .Include(s => s.Brand)
                .Include(s => s.ProductType)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (suspension == null)
            {
                return NotFound();
            }

            return View(suspension);
        }

        // POST: Suspensions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suspension = await _context.Suspensions.FindAsync(id);
            _context.Suspensions.Remove(suspension);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuspensionExists(int id)
        {
            return _context.Suspensions.Any(e => e.ProductID == id);
        }
    }
}
