using Microsoft.AspNetCore.Mvc;
using ProManagementSystem.Models.Entities;
using ProManagementSystem.Services;

namespace ProManagementSystem.Controllers
{
    public class ModulesController : Controller
    {
        private readonly IModuleService _moduleService;

        public ModulesController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        // GET: Modules
        public async Task<IActionResult> Index()
        {
            var modules = await _moduleService.GetAllModulesAsync();
            return View(modules);
        }

        // GET: Modules/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var module = await _moduleService.GetModuleByIdAsync(id);
            if (module == null)
            {
                return NotFound();
            }

            return View(module);
        }

        // GET: Modules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Modules/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Icon,IsActive,SortOrder")] Module module)
        {
            if (ModelState.IsValid)
            {
                if (await _moduleService.ModuleNameExistsAsync(module.Name))
                {
                    ModelState.AddModelError("Name", "A module with this name already exists.");
                    return View(module);
                }

                await _moduleService.CreateModuleAsync(module);
                TempData["SuccessMessage"] = "Module created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(module);
        }

        // GET: Modules/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var module = await _moduleService.GetModuleByIdAsync(id);
            if (module == null)
            {
                return NotFound();
            }
            return View(module);
        }

        // POST: Modules/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Icon,IsActive,SortOrder,CreatedAt")] Module module)
        {
            if (id != module.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (await _moduleService.ModuleNameExistsAsync(module.Name, module.Id))
                {
                    ModelState.AddModelError("Name", "A module with this name already exists.");
                    return View(module);
                }

                try
                {
                    await _moduleService.UpdateModuleAsync(module);
                    TempData["SuccessMessage"] = "Module updated successfully.";
                }
                catch (Exception)
                {
                    if (!await _moduleService.ModuleExistsAsync(module.Id))
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
            return View(module);
        }

        // GET: Modules/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var module = await _moduleService.GetModuleByIdAsync(id);
            if (module == null)
            {
                return NotFound();
            }

            return View(module);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _moduleService.DeleteModuleAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Module deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete module.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
