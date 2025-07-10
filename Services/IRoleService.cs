using ProManagementSystem.Models.Entities;

namespace ProManagementSystem.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<IEnumerable<Role>> GetActiveRolesAsync();
        Task<Role?> GetRoleByIdAsync(int id);
        Task<Role> CreateRoleAsync(Role role);
        Task<Role> UpdateRoleAsync(Role role);
        Task<bool> DeleteRoleAsync(int id);
        Task<bool> RoleExistsAsync(int id);
        Task<bool> RoleNameExistsAsync(string name, int? excludeId = null);
        Task<IEnumerable<Permission>> GetRolePermissionsAsync(int roleId);
        Task<bool> AssignPermissionToRoleAsync(int roleId, int permissionId);
        Task<bool> RemovePermissionFromRoleAsync(int roleId, int permissionId);
    }
}
