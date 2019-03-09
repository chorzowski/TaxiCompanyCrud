using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaxiCompany.Models;

namespace TaxiCompany.Controllers
{
    public class DriverController : Controller
    {
        private readonly TaxiCompanyContext _context;
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Driver> _repo;

        public DriverController(TaxiCompanyContext context, IUnitOfWork unit, IGenericRepository<Driver> repo)
        {
            _context = context;
            _uow = unit;
            _repo = repo;
        }

        // GET: Driver
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Drivers.ToListAsync());
            return View(await _uow.Context.Drivers.ToListAsync());
        }

        // GET: Driver/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _uow.Context.Drivers.FirstOrDefaultAsync(m => m.DriverID == id);
            //var driver = await _context.Drivers
            //    .FirstOrDefaultAsync(m => m.DriverID == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        // GET: Driver/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Driver/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DriverID,FirstName,LastName,TelephoneNumber,Email")] Driver driver)
        {
            if (ModelState.IsValid)
            {
                //   _context.Add(driver);
                _uow.Context.Drivers.Add(driver);
                // await _context.SaveChangesAsync();
                await _uow.Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(driver);
        }

        // GET: Driver/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //  var driver = await _context.Drivers.FindAsync(id);
            var driver = await _uow.Context.Drivers.FindAsync(id);
            if (driver == null)
            {
                return NotFound();
            }
            return View(driver);
        }

        // POST: Driver/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DriverID,FirstName,LastName,TelephoneNumber,Email")] Driver driver)
        {
            if (id != driver.DriverID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(driver);
                    //await _context.SaveChangesAsync();
                    _uow.Context.Update(driver);
                    await _uow.Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverExists(driver.DriverID))
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
            return View(driver);
        }

        // GET: Driver/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _uow.Context.Drivers.FirstOrDefaultAsync(m => m.DriverID == id);
            //var driver = await _context.Drivers
            //    .FirstOrDefaultAsync(m => m.DriverID == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        // POST: Driver/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var driver = await _context.Drivers.FindAsync(id);
            //_context.Drivers.Remove(driver);
            //await _context.SaveChangesAsync();
            var driver = await _uow.Context.Drivers.FindAsync(id);
            _uow.Context.Drivers.Remove(driver);
            await _uow.Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DriverExists(int id)
        {
            //  return _context.Drivers.Any(e => e.DriverID == id);
            return _uow.Context.Drivers.Any(m => m.DriverID == id);
        }
    }
}
