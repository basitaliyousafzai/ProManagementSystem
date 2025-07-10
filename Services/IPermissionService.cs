using ProManagementSystem.Models.Entities;

namespace ProManagementSystem.Services
{
    public interface IPermissionService
    {
        Task<IEnumerable<Permission>> GetAllPermissionsAsync();
        Task<IEnumerable<Permission>> GetActivePermissionsAsync();
        Task<Permission?> GetPermissionByIdAsync(int id);
        Task<IEnumerable<Permission>> GetPermissionsBySubModuleAsync(int subModuleId);
        Task<Permission> CreatePermissionAsync(Permission permission);
        Task<Permission> UpdatePermissionAsync(Permission permission);
        Task<bool> DeletePermissionAsync(int id);
        Task<bool> PermissionExistsAsync(int id);
        Task<bool> PermissionNameExistsAsync(string name, int subModuleId, int? excludeId = null);
    }
}
