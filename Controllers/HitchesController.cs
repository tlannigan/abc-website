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
    public class HitchesController : Controller
    {
        private readonly ProductContext _context;

        public HitchesController(ProductContext context)
        {
            _context = context;
        }

        // GET: Hitches
        public async Task<IActionResult> Index()
        {
            var productContext = _context.Hitches.Include(h => h.Brand).Include(h => h.ProductType);
            return View(await productContext.ToListAsync());
        }

        // GET: Hitches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hitch = await _context.Hitches
                .Include(h => h.Brand)
                .Include(h => h.ProductType)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (hitch == null)
            {
                return NotFound();
            }

            return View(hitch);
        }

        // GET: Hitches/Create
        public IActionResult Create()
        {
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandName");
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes, "ProductTypeID", "ProductTypeName");
            return View();
        }

        // POST: Hitches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HitchClass,WeightRatingPounds,ProductID,BrandID,ProductTypeID,ProductName,ProductDescription,VehicleModel,ProductCode,Price,InStock")] Hitch hitch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hitch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandName", hitch.BrandID);
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes, "ProductTypeID", "ProductTypeName", hitch.ProductTypeID);
            return View(hitch);
        }

        // GET: Hitches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hitch = await _context.Hitches.FindAsync(id);
            if (hitch == null)
            {
                return NotFound();
            }
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandName", hitch.BrandID);
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes, "ProductTypeID", "ProductTypeName", hitch.ProductTypeID);
            return View(hitch);
        }

        // POST: Hitches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HitchClass,WeightRatingPounds,ProductID,BrandID,ProductTypeID,ProductName,ProductDescription,VehicleModel,ProductCode,Price,InStock")] Hitch hitch)
        {
            if (id != hitch.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hitch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HitchExists(hitch.ProductID))
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
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandName", hitch.BrandID);
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes, "ProductTypeID", "ProductTypeName", hitch.ProductTypeID);
            return View(hitch);
        }

        // GET: Hitches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hitch = await _context.Hitches
                .Include(h => h.Brand)
                .Include(h => h.ProductType)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (hitch == null)
            {
                return NotFound();
            }

            return View(hitch);
        }

        // POST: Hitches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hitch = await _context.Hitches.FindAsync(id);
            _context.Hitches.Remove(hitch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HitchExists(int id)
        {
            return _context.Hitches.Any(e => e.ProductID == id);
        }
    }
}
