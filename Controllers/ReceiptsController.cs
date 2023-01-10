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
    public class ReceiptsController : Controller
    {
        private readonly StoreContext _context;

        public ReceiptsController(StoreContext context)
        {
            _context = context;
        }

        // GET: Receipts
        [Authorize(Roles="Administrator, Seller")]
        public async Task<IActionResult> Index()
        {
            var storeContext = _context.Receipts.Include(r => r.Branch).Include(r => r.Customer).Include(r => r.Vehicle);
            return View(await storeContext.ToListAsync());
        }
            
        // GET: Receipts/Details/5
        [Authorize(Roles="Administrator, Seller")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Receipts == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipts
                .Include(r => r.Branch)
                .Include(r => r.Customer)
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (receipt == null)
            {
                return NotFound();
            }

            return View(receipt);
        }

        // GET: Receipts/Create
        [Authorize(Roles="Administrator, Seller")]
        public IActionResult Create()
        {
            ViewData["BranchID"] = new SelectList(_context.Branches, "ID", "ID");
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID");
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "ID", "ID");
            return View();
        }

        // POST: Receipts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,BranchID,VehicleID,CustomerID,Price,Date")] Receipt receipt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receipt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchID"] = new SelectList(_context.Branches, "ID", "ID", receipt.BranchID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID", receipt.CustomerID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "ID", "ID", receipt.VehicleID);
            return View(receipt);
        }

        // GET: Receipts/Edit/5
        [Authorize(Roles="Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Receipts == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipts.FindAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }
            ViewData["BranchID"] = new SelectList(_context.Branches, "ID", "ID", receipt.BranchID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID", receipt.CustomerID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "ID", "ID", receipt.VehicleID);
            return View(receipt);
        }

        // POST: Receipts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,BranchID,VehicleID,CustomerID,Price,Date")] Receipt receipt)
        {
            if (id != receipt.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receipt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceiptExists(receipt.ID))
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
            ViewData["BranchID"] = new SelectList(_context.Branches, "ID", "ID", receipt.BranchID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "ID", receipt.CustomerID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "ID", "ID", receipt.VehicleID);
            return View(receipt);
        }

        // GET: Receipts/Delete/5
        [Authorize(Roles="Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Receipts == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipts
                .Include(r => r.Branch)
                .Include(r => r.Customer)
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (receipt == null)
            {
                return NotFound();
            }

            return View(receipt);
        }

        // POST: Receipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Receipts == null)
            {
                return Problem("Entity set 'StoreContext.Receipts'  is null.");
            }
            var receipt = await _context.Receipts.FindAsync(id);
            if (receipt != null)
            {
                _context.Receipts.Remove(receipt);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceiptExists(int id)
        {
          return (_context.Receipts?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
