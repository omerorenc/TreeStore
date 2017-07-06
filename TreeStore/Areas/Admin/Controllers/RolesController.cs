using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TreeStore.Data;
using TreeStore.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace TreeStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<Role> _roleManager;

        public RolesController(ApplicationDbContext context,RoleManager<Role> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        // GET: Admin/ApplicationRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicationRole.ToListAsync());
        }

        // GET: Admin/ApplicationRoles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationRole = await _context.ApplicationRole
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationRole == null)
            {
                return NotFound();
            }

            return View(applicationRole);
        }

        // GET: Admin/ApplicationRoles/Create
        public IActionResult Create()
        {
            var role = new Role();
            return View(role);
        }

        // POST: Admin/ApplicationRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NormalizedName,ConcurrencyStamp")] Role applicationRole)
        {
            try { if (ModelState.IsValid)
                {
                    if (_roleManager == null)
                    {
                        return NotFound();
                    }
                    var roles = applicationRole;
                    await _roleManager.CreateAsync(roles);
                    return RedirectToAction("Index");
                } }
            catch (Exception ex)
            {
                return RedirectToAction("Create");
                throw ex;

            }

            return View(applicationRole);
        }

        // GET: Admin/ApplicationRoles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationRole = await _context.ApplicationRole.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationRole == null)
            {
                return NotFound();
            }
            return View(applicationRole);
        }

        // POST: Admin/ApplicationRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,NormalizedName,ConcurrencyStamp")] Role applicationRole)
        {
            if (id != applicationRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _roleManager.UpdateAsync(applicationRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationRoleExists(applicationRole.Id))
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
            return View(applicationRole);
        }

        // GET: Admin/ApplicationRoles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationRole = await _context.ApplicationRole
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationRole == null)
            {
                return NotFound();
            }

            return View(applicationRole);
        }

        // POST: Admin/ApplicationRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationRole = await _context.ApplicationRole.SingleOrDefaultAsync(m => m.Id == id);
            await _roleManager.DeleteAsync(applicationRole);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ApplicationRoleExists(string id)
        {
            return _context.ApplicationRole.Any(e => e.Id == id);
        }
    }
}
