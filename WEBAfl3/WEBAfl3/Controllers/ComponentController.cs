﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WEBAfl3.Data;
using WEBAfl3.Data.Repository;
using WEBAfl3.Models;
using WEBAfl3.Models.ViewModel;

namespace WEBAfl3.Controllers
{
    public class ComponentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IComponentTypeRepository _componentTypeRepository;

        public ComponentController(IComponentTypeRepository componentTypeRepository, IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _componentTypeRepository = componentTypeRepository;
        }

        // GET: Component
        public async Task<IActionResult> Index()
        {
            return View(await _context.Components.ToListAsync());
        }

        // GET: Component/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.Components
                .FirstOrDefaultAsync(m => m.ComponentId == id);
            if (component == null)
            {
                return NotFound();
            }

            return View(component);
        }

        // GET: Component/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var allTypes = _componentTypeRepository.GetAll().ToList();
            ViewBag.AllTypes = allTypes;
            return View();
        }

        // POST: Component/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ComponentId,ComponentTypeId,ComponentNumber,SerialNo,Status,AdminComment,UserComment,CurrentLoanInformationId")] ComponentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var componentType = _componentTypeRepository.GetById(vm.ComponentTypeId);

                var component = new Component
                {
                    ComponentType = componentType,
                    AdminComment = vm.AdminComment,
                    ComponentNumber = vm.ComponentNumber,
                    CurrentLoanInformationId = vm.CurrentLoanInformationId,
                    SerialNo = vm.SerialNo,
                    Status = vm.Status,
                    UserComment = vm.UserComment
                };

                _context.Add(component);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.types = _componentTypeRepository.GetAll();
            return View(vm);
        }

        // GET: Component/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.Components.FindAsync(id);
            if (component == null)
            {
                return NotFound();
            }
            return View(component);
        }

        // POST: Component/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ComponentId,ComponentTypeId,ComponentNumber,SerialNo,Status,AdminComment,UserComment,CurrentLoanInformationId")] Component component)
        {
            if (id != component.ComponentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(component);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComponentExists(component.ComponentId))
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
            return View(component);
        }

        // GET: Component/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.Components
                .FirstOrDefaultAsync(m => m.ComponentId == id);
            if (component == null)
            {
                return NotFound();
            }

            return View(component);
        }

        // POST: Component/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var component = await _context.Components.FindAsync(id);
            _context.Components.Remove(component);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComponentExists(long id)
        {
            return _context.Components.Any(e => e.ComponentId == id);
        }
    }
}
