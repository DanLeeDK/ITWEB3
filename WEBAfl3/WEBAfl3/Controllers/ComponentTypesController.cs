using System;
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

namespace WEBAfl3.Controllers
{
    public class ComponentTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoryRepository _categoryRepository;
        

        public ComponentTypesController(ICategoryRepository categoryRepository, IServiceProvider serviceProvider)
        {
            _categoryRepository = categoryRepository;
            _context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        }

        // GET: ComponentTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ComponentTypes.ToListAsync());
        }

        public async Task<IActionResult> TypeList(int id, long typeId = 0)
        {

            ViewBag.CategoryId = id;
            ViewBag.SelectedTypeId = (int)typeId;
            return View(await _context.CategoryComponentTypes
                .Where(cc => cc.CategoryId == id)
                .Select(cc => cc.ComponentType)
                .ToListAsync());
        }



        public IActionResult ComponentList(long id)
        {
            return ViewComponent("Result", new { typeId = (int)id });
        }

        // GET: ComponentTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await _context.ComponentTypes
                .FirstOrDefaultAsync(m => m.ComponentTypeId == id);
            if (componentType == null)
            {
                return NotFound();
            }

            return View(componentType);
        }

        // GET: ComponentTypes/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var allCategories = _categoryRepository.GetAll().ToList();
            ViewBag.AllCategories = allCategories;
            return View();
        }

        // POST: ComponentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComponentName,ComponentInfo,Location,Datasheet,ImageUrl,Manufacturer,WikiLink,AdminComment,Categories")] ComponentType componentType, IFormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                var selectedValues = formCollection["categories"].ToString();
                var splitSelected = selectedValues.Split(",");
                var allCategories = _categoryRepository.GetAll().ToList();
                foreach (var typeId in splitSelected)
                {
                    foreach (var cat in allCategories)
                    {
                        if (cat.CategoryId.ToString() == typeId)
                        {
                            _context.CategoryComponentTypes.Add(new ComponentTypeCategory { Category = cat, ComponentType = componentType });
                        }
                    }
                }

                _context.Add(componentType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(componentType);
        }

        // GET: ComponentTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await _context.ComponentTypes.SingleOrDefaultAsync(m => m.ComponentTypeId == id);
            var componentsOfType = await _context.Components.Where(c => c.ComponentTypeId == id).ToListAsync();
            ViewBag.ComponentsOfType = componentsOfType;

            var categoriesOfType = await _context.CategoryComponentTypes
                .Where(x => x.ComponentTypeId == id)
                .Select(x => x.Category)
                .ToListAsync();

            var categoriesAvailable = await _context.Categories.Where(c => !categoriesOfType.Contains(c))
                .ToListAsync();

            ViewBag.CategoriesOfType = categoriesOfType;
            ViewBag.CategoriesAvailable = categoriesAvailable;
            if (componentType == null)
            {
                return NotFound();
            }
            return View(componentType);
        }

        // POST: ComponentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ComponentTypeId,ComponentName,ComponentInfo,Location,Status,Datasheet,ImageUrl,Manufacturer,WikiLink,AdminComment")] ComponentType componentType, IFormCollection formCollection)
        {
            if (id != componentType.ComponentTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var selectedValues = formCollection["categories"].ToString();
                    string[] splitSelected = new string[0];
                    if (!string.IsNullOrEmpty(selectedValues))
                    {
                        splitSelected = selectedValues.Split(",");
                    }

                    var categoriesOfType = await _context.CategoryComponentTypes
                        .Where(cc => cc.ComponentTypeId == id)
                        .Select(cc => cc.Category)
                        .ToListAsync();

                    foreach (var categoryId in splitSelected)
                    {
                        var category = _categoryRepository.GetById(Int32.Parse(categoryId));



                        if (!categoriesOfType.Contains(category))
                        {
                            _context.CategoryComponentTypes.Add(new ComponentTypeCategory { Category = category, ComponentType = componentType });
                        }
                    }

                    foreach (var type in categoriesOfType)
                    {
                        if (!splitSelected.Contains(type.CategoryId.ToString()))
                        {
                            var catCompType = await _context.CategoryComponentTypes
                                .Where(cc => cc.CategoryId == type.CategoryId
                                             && cc.ComponentTypeId == id).SingleOrDefaultAsync();
                            if (catCompType != null)
                            {
                                _context.CategoryComponentTypes.Remove(catCompType);
                                _context.SaveChanges();
                            }
                        }
                    }
                    _context.Update(componentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComponentTypeExists(componentType.ComponentTypeId))
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
            return View(componentType);
        }

        // GET: ComponentTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await _context.ComponentTypes
                .FirstOrDefaultAsync(m => m.ComponentTypeId == id);
            if (componentType == null)
            {
                return NotFound();
            }

            return View(componentType);
        }

        // POST: ComponentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var componentType = await _context.ComponentTypes.FindAsync(id);
            _context.ComponentTypes.Remove(componentType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComponentTypeExists(int id)
        {
            return _context.ComponentTypes.Any(e => e.ComponentTypeId == id);
        }
    }
}
