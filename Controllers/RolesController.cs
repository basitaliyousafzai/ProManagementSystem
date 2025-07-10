using Microsoft.AspNetCore.Mvc;
using ProManagementSystem.Models.Entities;
using ProManagementSystem.Services;

namespace ProManagementSystem.Controllers
{
    public class RolesController : Controller
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return View(roles);
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,IsActive")] Role role)
        {
            if (ModelState.IsValid)
            {
                if (await _roleService.RoleNameExistsAsync(role.Name))
                {
                    ModelState.AddModelError("Name", "A role with this name already exists.");
                    return View(role);
                }

                await _roleService.CreateRoleAsync(role);
                TempData["SuccessMessage"] = "Role created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,IsActive,CreatedAt")] Role role)
        {
            if (id != role.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (await _roleService.RoleNameExistsAsync(role.Name, role.Id))
                {
                    ModelState.AddModelError("Name", "A role with this name already exists.");
                    return View(role);
                }

                try
                {
                    await _roleService.UpdateRoleAsync(role);
                    TempData["SuccessMessage"] = "Role updated successfully.";
                }
                catch (Exception)
                {
                    if (!await _roleService.RoleExistsAsync(role.Id))
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
            return View(role);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _roleService.DeleteRoleAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Role deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete role.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
