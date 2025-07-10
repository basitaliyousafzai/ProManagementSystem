using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProManagementSystem.Models.Entities
{
    public class SubModule
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Icon { get; set; } = string.Empty;
        
        [StringLength(200)]
        public string Url { get; set; } = string.Empty;
        
        public bool IsActive { get; set; } = true;
        
        public int SortOrder { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        
        // Foreign key
        [ForeignKey("Module")]
        public int ModuleId { get; set; }
        
        // Navigation properties
        public virtual Module Module { get; set; } = null!;
        public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
