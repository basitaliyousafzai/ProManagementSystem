using Microsoft.EntityFrameworkCore;
using ProManagementSystem.Data;
using ProManagementSystem.Models.Entities;

namespace ProManagementSystem.Services
{
    public class SubModuleService : ISubModuleService
    {
        private readonly ApplicationDbContext _context;

        public SubModuleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SubModule>> GetAllSubModulesAsync()
        {
            return await _context.SubModules
                .Include(sm => sm.Module)
                .Include(sm => sm.Permissions)
                .OrderBy(sm => sm.Module.Name)
                .ThenBy(sm => sm.SortOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<SubModule>> GetActiveSubModulesAsync()
        {
            return await _context.SubModules
                .Include(sm => sm.Module)
                .Where(sm => sm.IsActive && sm.Module.IsActive)
                .OrderBy(sm => sm.Module.Name)
                .ThenBy(sm => sm.SortOrder)
                .ToListAsync();
        }

        public async Task<SubModule?> GetSubModuleByIdAsync(int id)
        {
            return await _context.SubModules
                .Include(sm => sm.Module)
                .Include(sm => sm.Permissions)
                .FirstOrDefaultAsync(sm => sm.Id == id);
        }

        public async Task<IEnumerable<SubModule>> GetSubModulesByModuleAsync(int moduleId)
        {
            return await _context.SubModules
                .Include(sm => sm.Module)
                .Where(sm => sm.ModuleId == moduleId)
                .OrderBy(sm => sm.SortOrder)
                .ToListAsync();
        }

        public async Task<SubModule> CreateSubModuleAsync(SubModule subModule)
        {
            subModule.CreatedAt = DateTime.Now;
            subModule.UpdatedAt = DateTime.Now;
            
            _context.SubModules.Add(subModule);
            await _context.SaveChangesAsync();
            return subModule;
        }

        public async Task<SubModule> UpdateSubModuleAsync(SubModule subModule)
        {
            subModule.UpdatedAt = DateTime.Now;
            
            _context.SubModules.Update(subModule);
            await _context.SaveChangesAsync();
            return subModule;
        }

        public async Task<bool> DeleteSubModuleAsync(int id)
        {
            var subModule = await _context.SubModules.FindAsync(id);
            if (subModule == null)
                return false;

            _context.SubModules.Remove(subModule);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SubModuleExistsAsync(int id)
        {
            return await _context.SubModules.AnyAsync(sm => sm.Id == id);
        }

        public async Task<bool> SubModuleNameExistsAsync(string name, int moduleId, int? excludeId = null)
        {
            var query = _context.SubModules.Where(sm => sm.Name.ToLower() == name.ToLower() && sm.ModuleId == moduleId);
            
            if (excludeId.HasValue)
                query = query.Where(sm => sm.Id != excludeId.Value);

            return await query.AnyAsync();
        }
    }
}
