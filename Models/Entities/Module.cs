using System.ComponentModel.DataAnnotations;

namespace ProManagementSystem.Models.Entities
{
    public class Module
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Icon { get; set; } = string.Empty;
        
        public bool IsActive { get; set; } = true;
        
        public int SortOrder { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        // Navigation properties
        public virtual ICollection<SubModule> SubModules { get; set; } = new List<SubModule>();
    }
}
