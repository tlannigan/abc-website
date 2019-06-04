using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ca.abcmufflerandhitch.Data;
using ca.abcmufflerandhitch.Models.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using PhotoSauce.MagicScaler;
using ca.abcmufflerandhitch.Controllers;

namespace ABCMufflerAndHitch.Controllers
{
    public class ExhaustsController : Controller
    {
        private readonly ProductContext _context;
        private IHostingEnvironment _hostingEnv; // tlannigan

        public ExhaustsController(ProductContext context, IHostingEnvironment env)
        {
            _context = context;
            _hostingEnv = env; // tlannigan
        }

        // GET: Exhausts
        public async Task<IActionResult> Index()
        {
            var productContext = _context.Exhausts.Include(e => e.Brand).Include(e => e.ProductType);
            return View(await productContext.ToListAsync());
        }

        // GET: Exhausts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exhaust = await _context.Exhausts
                .Include(e => e.Brand)
                .Include(e => e.ProductType)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (exhaust == null)
            {
                return NotFound();
            }

            return View(exhaust);
        }

        // GET: Exhausts/Create
        public IActionResult Create()
        {
            ViewData["BrandID"] = new SelectList(_context.Brands.OrderBy(b => b.BrandName), "BrandID", "BrandName");
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes.OrderBy(p => p.ProductTypeName), "ProductTypeID", "ProductTypeName");
            return View();
        }

        // POST: Exhausts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiameterInches,DegreeBend,Material,BrandID,ProductTypeID,ProductName,ProductDescription,VehicleModel,ProductCode,Price,InStock")] Exhaust exhaust, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exhaust);
                await _context.SaveChangesAsync();

                // tlannigan
                var image = new ImagesController(_context, _hostingEnv);
                image.SaveImage(exhaust.ProductID, file);

                return RedirectToAction(nameof(Index));
            }

            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandName", exhaust.BrandID);
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes, "ProductTypeID", "ProductTypeName", exhaust.ProductTypeID);

            return View(exhaust);
        }

        // GET: Exhausts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exhaust = await _context.Exhausts.FindAsync(id);
            if (exhaust == null)
            {
                return NotFound();
            }
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandName", exhaust.BrandID);
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes, "ProductTypeID", "ProductTypeName", exhaust.ProductTypeID);
            return View(exhaust);
        }

        // POST: Exhausts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiameterInches,DegreeBend,Material,BrandID,ProductTypeID,ProductName,ProductDescription,VehicleModel,ProductCode,Price,InStock")] Exhaust exhaust, IFormFile file)
        {
            if (id != exhaust.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exhaust);
                    await _context.SaveChangesAsync();

                    // tlannigan
                    if (file != null)
                    {
                        var image = new ImagesController(_context, _hostingEnv);
                        image.DeleteImage(id);
                        image.SaveImage(id, file);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExhaustExists(exhaust.ProductID))
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
            ViewData["BrandID"] = new SelectList(_context.Brands, "BrandID", "BrandName", exhaust.BrandID);
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes, "ProductTypeID", "ProductTypeName", exhaust.ProductTypeID);
            return View(exhaust);
        }

        // GET: Exhausts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exhaust = await _context.Exhausts
                .Include(e => e.Brand)
                .Include(e => e.ProductType)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (exhaust == null)
            {
                return NotFound();
            }

            return View(exhaust);
        }

        // POST: Exhausts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exhaust = await _context.Exhausts.FindAsync(id);
            _context.Exhausts.Remove(exhaust);

            var image = new ImagesController(_context, _hostingEnv);
            image.DeleteImage(id);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExhaustExists(int id)
        {
            return _context.Exhausts.Any(e => e.ProductID == id);
        }
    }
}
