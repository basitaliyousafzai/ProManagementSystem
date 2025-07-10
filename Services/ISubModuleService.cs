using ProManagementSystem.Models.Entities;

namespace ProManagementSystem.Services
{
    public interface ISubModuleService
    {
        Task<IEnumerable<SubModule>> GetAllSubModulesAsync();
        Task<IEnumerable<SubModule>> GetActiveSubModulesAsync();
        Task<SubModule?> GetSubModuleByIdAsync(int id);
        Task<IEnumerable<SubModule>> GetSubModulesByModuleAsync(int moduleId);
        Task<SubModule> CreateSubModuleAsync(SubModule subModule);
        Task<SubModule> UpdateSubModuleAsync(SubModule subModule);
        Task<bool> DeleteSubModuleAsync(int id);
        Task<bool> SubModuleExistsAsync(int id);
        Task<bool> SubModuleNameExistsAsync(string name, int moduleId, int? excludeId = null);
    }
}
