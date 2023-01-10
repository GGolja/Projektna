using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projektna.Data;
using Projektna.Models;
using Microsoft.AspNetCore.Authorization;

namespace Projektna.Controllers
{
    public class TestController : Controller
    {
        private readonly StoreContext _context;

        public TestController(StoreContext context)
        {
            _context = context;
        }

        // GET: Test
        [Authorize(Roles="Administrator, Seller")]
        public async Task<IActionResult> Index()
        {
            var storeContext = _context.TestDrives.Include(t => t.Branch).Include(t => t.Customer).Include(t => t.Vehicle);
            return View(await storeContext.ToListAsync());
        }

        // GET: Test/Details/5
        [Authorize(Roles="Administrator, Seller")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TestDrives == null)
            {
                return NotFound();
            }

            var testDrive = await _context.TestDrives
                .Include(t => t.Branch)
                .Include(t => t.Customer)
                .Include(t => t.Vehicle)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (testDrive == null)
            {
                return NotFound();
            }

            return View(testDrive);
        }

        // GET: Test/Create
        [Authorize(Roles="Administrator, Seller")]
        public IActionResult Create()
        {
            ViewData["BranchID"] = new SelectList(_context.Branches, "ID", "ID");
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID");
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "ID", "ID");
            return View();
        }

        // POST: Test/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,BranchID,VehicleID,CustomerID,Date")] TestDrive testDrive)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testDrive);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchID"] = new SelectList(_context.Branches, "ID", "ID", testDrive.BranchID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID", testDrive.CustomerID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "ID", "ID", testDrive.VehicleID);
            return View(testDrive);
        }

        // GET: Test/Edit/5
        [Authorize(Roles="Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TestDrives == null)
            {
                return NotFound();
            }

            var testDrive = await _context.TestDrives.FindAsync(id);
            if (testDrive == null)
            {
                return NotFound();
            }
            ViewData["BranchID"] = new SelectList(_context.Branches, "ID", "ID", testDrive.BranchID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID", testDrive.CustomerID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "ID", "ID", testDrive.VehicleID);
            return View(testDrive);
        }

        // POST: Test/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,BranchID,VehicleID,CustomerID,Date")] TestDrive testDrive)
        {
            if (id != testDrive.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testDrive);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestDriveExists(testDrive.ID))
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
            ViewData["BranchID"] = new SelectList(_context.Branches, "ID", "ID", testDrive.BranchID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID", testDrive.CustomerID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "ID", "ID", testDrive.VehicleID);
            return View(testDrive);
        }

        // GET: Test/Delete/5
        [Authorize(Roles="Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TestDrives == null)
            {
                return NotFound();
            }

            var testDrive = await _context.TestDrives
                .Include(t => t.Branch)
                .Include(t => t.Customer)
                .Include(t => t.Vehicle)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (testDrive == null)
            {
                return NotFound();
            }

            return View(testDrive);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TestDrives == null)
            {
                return Problem("Entity set 'StoreContext.TestDrives'  is null.");
            }
            var testDrive = await _context.TestDrives.FindAsync(id);
            if (testDrive != null)
            {
                _context.TestDrives.Remove(testDrive);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestDriveExists(int id)
        {
          return (_context.TestDrives?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
