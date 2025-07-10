using Microsoft.EntityFrameworkCore;
using ProManagementSystem.Data;
using ProManagementSystem.Models.Entities;

namespace ProManagementSystem.Services
{
    public class ModuleService : IModuleService
    {
        private readonly ApplicationDbContext _context;

        public ModuleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Module>> GetAllModulesAsync()
        {
            return await _context.Modules
                .Include(m => m.SubModules)
                .OrderBy(m => m.SortOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<Module>> GetActiveModulesAsync()
        {
            return await _context.Modules
                .Include(m => m.SubModules.Where(sm => sm.IsActive))
                .Where(m => m.IsActive)
                .OrderBy(m => m.SortOrder)
                .ToListAsync();
        }

        public async Task<Module?> GetModuleByIdAsync(int id)
        {
            return await _context.Modules
                .Include(m => m.SubModules)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Module> CreateModuleAsync(Module module)
        {
            module.CreatedAt = DateTime.Now;
            module.UpdatedAt = DateTime.Now;
            
            _context.Modules.Add(module);
            await _context.SaveChangesAsync();
            return module;
        }

        public async Task<Module> UpdateModuleAsync(Module module)
        {
            module.UpdatedAt = DateTime.Now;
            
            _context.Modules.Update(module);
            await _context.SaveChangesAsync();
            return module;
        }

        public async Task<bool> DeleteModuleAsync(int id)
        {
            var module = await _context.Modules.FindAsync(id);
            if (module == null)
                return false;

            _context.Modules.Remove(module);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ModuleExistsAsync(int id)
        {
            return await _context.Modules.AnyAsync(m => m.Id == id);
        }

        public async Task<bool> ModuleNameExistsAsync(string name, int? excludeId = null)
        {
            var query = _context.Modules.Where(m => m.Name.ToLower() == name.ToLower());
            
            if (excludeId.HasValue)
                query = query.Where(m => m.Id != excludeId.Value);

            return await query.AnyAsync();
        }
    }
}
