using Microsoft.EntityFrameworkCore;
using ProManagementSystem.Data;
using ProManagementSystem.Models.Entities;

namespace ProManagementSystem.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;

        public RoleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Roles
                .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .OrderBy(r => r.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetActiveRolesAsync()
        {
            return await _context.Roles
                .Where(r => r.IsActive)
                .OrderBy(r => r.Name)
                .ToListAsync();
        }

        public async Task<Role?> GetRoleByIdAsync(int id)
        {
            return await _context.Roles
                .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Role> CreateRoleAsync(Role role)
        {
            role.CreatedAt = DateTime.Now;
            role.UpdatedAt = DateTime.Now;
            
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<Role> UpdateRoleAsync(Role role)
        {
            role.UpdatedAt = DateTime.Now;
            
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
                return false;

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RoleExistsAsync(int id)
        {
            return await _context.Roles.AnyAsync(r => r.Id == id);
        }

        public async Task<bool> RoleNameExistsAsync(string name, int? excludeId = null)
        {
            var query = _context.Roles.Where(r => r.Name.ToLower() == name.ToLower());
            
            if (excludeId.HasValue)
                query = query.Where(r => r.Id != excludeId.Value);

            return await query.AnyAsync();
        }

        public async Task<IEnumerable<Permission>> GetRolePermissionsAsync(int roleId)
        {
            return await _context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .Include(rp => rp.Permission)
                .Select(rp => rp.Permission)
                .ToListAsync();
        }

        public async Task<bool> AssignPermissionToRoleAsync(int roleId, int permissionId)
        {
            var existingRolePermission = await _context.RolePermissions
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

            if (existingRolePermission != null)
                return false;

            var rolePermission = new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId,
                CreatedAt = DateTime.Now
            };

            _context.RolePermissions.Add(rolePermission);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemovePermissionFromRoleAsync(int roleId, int permissionId)
        {
            var rolePermission = await _context.RolePermissions
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

            if (rolePermission == null)
                return false;

            _context.RolePermissions.Remove(rolePermission);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
