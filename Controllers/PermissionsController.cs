using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProManagementSystem.Models.Entities;
using ProManagementSystem.Services;

namespace ProManagementSystem.Controllers
{
    public class PermissionsController : Controller
    {
        private readonly IPermissionService _permissionService;
        private readonly ISubModuleService _subModuleService;

        public PermissionsController(IPermissionService permissionService, ISubModuleService subModuleService)
        {
            _permissionService = permissionService;
            _subModuleService = subModuleService;
        }

        // GET: Permissions
        public async Task<IActionResult> Index()
        {
            var permissions = await _permissionService.GetAllPermissionsAsync();
            return View(permissions);
        }

        // GET: Permissions/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var permission = await _permissionService.GetPermissionByIdAsync(id);
            if (permission == null)
            {
                return NotFound();
            }

            return View(permission);
        }

        // GET: Permissions/Create
        public async Task<IActionResult> Create()
        {
            await PopulateSubModuleDropDown();
            return View();
        }

        // POST: Permissions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,IsActive,SubModuleId")] Permission permission)
        {
            if (ModelState.IsValid)
            {
                if (await _permissionService.PermissionNameExistsAsync(permission.Name, permission.SubModuleId))
                {
                    ModelState.AddModelError("Name", "A permission with this name already exists in this sub-module.");
                    return View(permission);
                }

                await _permissionService.CreatePermissionAsync(permission);
                TempData["SuccessMessage"] = "Permission created successfully.";
                return RedirectToAction(nameof(Index));
            }
            await PopulateSubModuleDropDown(permission.SubModuleId);
            return View(permission);
        }

        // GET: Permissions/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var permission = await _permissionService.GetPermissionByIdAsync(id);
            if (permission == null)
            {
                return NotFound();
            }
            return View(permission);
        }

        // POST: Permissions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,IsActive,SubModuleId,CreatedAt")] Permission permission)
        {
            if (id != permission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (await _permissionService.PermissionNameExistsAsync(permission.Name, permission.SubModuleId, permission.Id))
                {
                    ModelState.AddModelError("Name", "A permission with this name already exists in this sub-module.");
                    return View(permission);
                }

                try
                {
                    await _permissionService.UpdatePermissionAsync(permission);
                    TempData["SuccessMessage"] = "Permission updated successfully.";
                }
                catch (Exception)
                {
                    if (!await _permissionService.PermissionExistsAsync(permission.Id))
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
            return View(permission);
        }

        // GET: Permissions/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var permission = await _permissionService.GetPermissionByIdAsync(id);
            if (permission == null)
            {
                return NotFound();
            }

            return View(permission);
        }

        // POST: Permissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _permissionService.DeletePermissionAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Permission deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete permission.";
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateSubModuleDropDown(object selectedSubModule = null)
        {
            var subModules = await _subModuleService.GetActiveSubModulesAsync();
            var subModuleList = subModules.Select(sm => new
            {
                Value = sm.Id,
                Text = $"{sm.Module.Name} - {sm.Name}"
            });
            ViewBag.SubModuleId = new SelectList(subModuleList, "Value", "Text", selectedSubModule);
        }
    }
}
