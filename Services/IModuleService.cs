using ProManagementSystem.Models.Entities;

namespace ProManagementSystem.Services
{
    public interface IModuleService
    {
        Task<IEnumerable<Module>> GetAllModulesAsync();
        Task<IEnumerable<Module>> GetActiveModulesAsync();
        Task<Module?> GetModuleByIdAsync(int id);
        Task<Module> CreateModuleAsync(Module module);
        Task<Module> UpdateModuleAsync(Module module);
        Task<bool> DeleteModuleAsync(int id);
        Task<bool> ModuleExistsAsync(int id);
        Task<bool> ModuleNameExistsAsync(string name, int? excludeId = null);
    }
}
