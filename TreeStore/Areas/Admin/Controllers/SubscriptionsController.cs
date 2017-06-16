using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TreeStore.Data;
using TreeStore.Models.Entities;

namespace TreeStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubscriptionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Admin/Subscriptions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Subscriptions.ToListAsync());
        }

        // GET: Admin/Subscriptions/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = await _context.Subscriptions
                .SingleOrDefaultAsync(m => m.Id == id);
            if (subscription == null)
            {
                return NotFound();
            }

            return View(subscription);
        }

        // GET: Admin/Subscriptions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Subscriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,FullName,IsSubscribed,SubscriptionDate,UnsubscriptionDate,IsConfirmed,ConfirmationDate,ConfirmationCode,Id,Name,CreateDate,UpdateDate,CreatedBy,UpdateBy")] Subscription subscription)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(subscription);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Create");
                throw ex;
            }
            
            return View(subscription);
        }

        // GET: Admin/Subscriptions/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = await _context.Subscriptions.SingleOrDefaultAsync(m => m.Id == id);
            if (subscription == null)
            {
                return NotFound();
            }
            return View(subscription);
        }

        // POST: Admin/Subscriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Email,FullName,IsSubscribed,SubscriptionDate,UnsubscriptionDate,IsConfirmed,ConfirmationDate,ConfirmationCode,Id,Name,CreateDate,UpdateDate,CreatedBy,UpdateBy")] Subscription subscription)
        {
            if (id != subscription.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subscription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubscriptionExists(subscription.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(subscription);
        }

        // GET: Admin/Subscriptions/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = await _context.Subscriptions
                .SingleOrDefaultAsync(m => m.Id == id);
            if (subscription == null)
            {
                return NotFound();
            }

            return View(subscription);
        }

        // POST: Admin/Subscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var subscription = await _context.Subscriptions.SingleOrDefaultAsync(m => m.Id == id);
            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SubscriptionExists(long id)
        {
            return _context.Subscriptions.Any(e => e.Id == id);
        }
    }
}
