using Microsoft.EntityFrameworkCore;
using ProManagementSystem.Data;
using ProManagementSystem.Models.Entities;

namespace ProManagementSystem.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly ApplicationDbContext _context;

        public PermissionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
        {
            return await _context.Permissions
                .Include(p => p.SubModule)
                .ThenInclude(sm => sm.Module)
                .OrderBy(p => p.SubModule.Module.Name)
                .ThenBy(p => p.SubModule.Name)
                .ThenBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Permission>> GetActivePermissionsAsync()
        {
            return await _context.Permissions
                .Include(p => p.SubModule)
                .ThenInclude(sm => sm.Module)
                .Where(p => p.IsActive && p.SubModule.IsActive && p.SubModule.Module.IsActive)
                .OrderBy(p => p.SubModule.Module.Name)
                .ThenBy(p => p.SubModule.Name)
                .ThenBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<Permission?> GetPermissionByIdAsync(int id)
        {
            return await _context.Permissions
                .Include(p => p.SubModule)
                .ThenInclude(sm => sm.Module)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Permission>> GetPermissionsBySubModuleAsync(int subModuleId)
        {
            return await _context.Permissions
                .Where(p => p.SubModuleId == subModuleId)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<Permission> CreatePermissionAsync(Permission permission)
        {
            permission.CreatedAt = DateTime.Now;
            permission.UpdatedAt = DateTime.Now;
            
            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();
            return permission;
        }

        public async Task<Permission> UpdatePermissionAsync(Permission permission)
        {
            permission.UpdatedAt = DateTime.Now;
            
            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();
            return permission;
        }

        public async Task<bool> DeletePermissionAsync(int id)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission == null)
                return false;

            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PermissionExistsAsync(int id)
        {
            return await _context.Permissions.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> PermissionNameExistsAsync(string name, int subModuleId, int? excludeId = null)
        {
            var query = _context.Permissions.Where(p => p.Name.ToLower() == name.ToLower() && p.SubModuleId == subModuleId);
            
            if (excludeId.HasValue)
                query = query.Where(p => p.Id != excludeId.Value);

            return await query.AnyAsync();
        }
    }
}
