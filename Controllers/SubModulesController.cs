using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProManagementSystem.Models.Entities;
using ProManagementSystem.Services;

namespace ProManagementSystem.Controllers
{
    public class SubModulesController : Controller
    {
        private readonly ISubModuleService _subModuleService;
        private readonly IModuleService _moduleService;

        public SubModulesController(ISubModuleService subModuleService, IModuleService moduleService)
        {
            _subModuleService = subModuleService;
            _moduleService = moduleService;
        }

        // GET: SubModules
        public async Task<IActionResult> Index()
        {
            var subModules = await _subModuleService.GetAllSubModulesAsync();
            return View(subModules);
        }

        // GET: SubModules/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var subModule = await _subModuleService.GetSubModuleByIdAsync(id);
            if (subModule == null)
            {
                return NotFound();
            }

            return View(subModule);
        }

        // GET: SubModules/Create
        public async Task<IActionResult> Create()
        {
            await PopulateModuleDropDown();
            return View();
        }

        // POST: SubModules/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Icon,Url,IsActive,SortOrder,ModuleId")] SubModule subModule)
        {
            if (ModelState.IsValid)
            {
                if (await _subModuleService.SubModuleNameExistsAsync(subModule.Name, subModule.ModuleId))
                {
                    ModelState.AddModelError("Name", "A sub-module with this name already exists in this module.");
                    await PopulateModuleDropDown(subModule.ModuleId);
                    return View(subModule);
                }

                await _subModuleService.CreateSubModuleAsync(subModule);
                TempData["SuccessMessage"] = "Sub-module created successfully.";
                return RedirectToAction(nameof(Index));
            }
            await PopulateModuleDropDown(subModule.ModuleId);
            return View(subModule);
        }

        // GET: SubModules/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var subModule = await _subModuleService.GetSubModuleByIdAsync(id);
            if (subModule == null)
            {
                return NotFound();
            }
            await PopulateModuleDropDown(subModule.ModuleId);
            return View(subModule);
        }

        // POST: SubModules/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Icon,Url,IsActive,SortOrder,ModuleId,CreatedAt")] SubModule subModule)
        {
            if (id != subModule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (await _subModuleService.SubModuleNameExistsAsync(subModule.Name, subModule.ModuleId, subModule.Id))
                {
                    ModelState.AddModelError("Name", "A sub-module with this name already exists in this module.");
                    await PopulateModuleDropDown(subModule.ModuleId);
                    return View(subModule);
                }

                try
                {
                    await _subModuleService.UpdateSubModuleAsync(subModule);
                    TempData["SuccessMessage"] = "Sub-module updated successfully.";
                }
                catch (Exception)
                {
                    if (!await _subModuleService.SubModuleExistsAsync(subModule.Id))
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
            await PopulateModuleDropDown(subModule.ModuleId);
            return View(subModule);
        }

        // GET: SubModules/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var subModule = await _subModuleService.GetSubModuleByIdAsync(id);
            if (subModule == null)
            {
                return NotFound();
            }

            return View(subModule);
        }

        // POST: SubModules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _subModuleService.DeleteSubModuleAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Sub-module deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete sub-module.";
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateModuleDropDown(object selectedModule = null)
        {
            var modules = await _moduleService.GetActiveModulesAsync();
            ViewBag.ModuleId = new SelectList(modules, "Id", "Name", selectedModule);
        }
    }
}
