using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProManagementSystem.Models.Entities
{
    public class Permission
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Foreign key
        [ForeignKey("SubModule")]
        public int SubModuleId { get; set; }

        // Navigation properties
        public virtual SubModule SubModule { get; set; } = null!;
    }
}
