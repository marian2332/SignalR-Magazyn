using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Signalr.Data;
using Signalr.Hubs;
using Signalr.Models;

namespace Signalr.Controllers
{
    public class ZamowieniaController : Controller
    {
        private readonly MagazynContext _context;
        private readonly IHubContext<RefreshHub> _refreshhub;

        public ZamowieniaController(MagazynContext context, IHubContext<RefreshHub> refreshhub)
        {
            _context = context;
            _refreshhub = refreshhub;
        }

        // GET: Zamowienia
        public async Task<IActionResult> Index()
        {
            return View(await _context.Zamowienia.ToListAsync());
        }

        [HttpGet]
        public IActionResult GetProducts()
        {

            var res = _context.Zamowienia.ToList();
            return Ok(res);
        }

        // GET: Zamowienia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zamowienia = await _context.Zamowienia
                .FirstOrDefaultAsync(m => m.Za_Id == id);
            if (zamowienia == null)
            {
                return NotFound();
            }

            return View(zamowienia);
        }

        // GET: Zamowienia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zamowienia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Za_Id,Za_Nr_Zamowienia,Za_Nazwa,Za_Odbiorca")] Zamowienia zamowienia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zamowienia);
                await _context.SaveChangesAsync();
                await _refreshhub.Clients.All.SendAsync("LoadProducts");
                return RedirectToAction(nameof(Index));
                
            }
            return View(zamowienia);
        }

        // GET: Zamowienia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zamowienia = await _context.Zamowienia.FindAsync(id);
            if (zamowienia == null)
            {
                return NotFound();
            }
            return View(zamowienia);
        }

        // POST: Zamowienia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Za_Id,Za_Nr_Zamowienia,Za_Nazwa,Za_Odbiorca")] Zamowienia zamowienia)
        {
            if (id != zamowienia.Za_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zamowienia);
                    await _context.SaveChangesAsync();
                    await _refreshhub.Clients.All.SendAsync("LoadProducts");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZamowieniaExists(zamowienia.Za_Id))
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
            return View(zamowienia);
        }

        // GET: Zamowienia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zamowienia = await _context.Zamowienia
                .FirstOrDefaultAsync(m => m.Za_Id == id);
            if (zamowienia == null)
            {
                return NotFound();
            }

            return View(zamowienia);
        }

        // POST: Zamowienia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zamowienia = await _context.Zamowienia.FindAsync(id);
            _context.Zamowienia.Remove(zamowienia);
            await _context.SaveChangesAsync();
            await _refreshhub.Clients.All.SendAsync("LoadProducts");
            return RedirectToAction(nameof(Index));
        }

        private bool ZamowieniaExists(int id)
        {
            return _context.Zamowienia.Any(e => e.Za_Id == id);
        }
    }
}
