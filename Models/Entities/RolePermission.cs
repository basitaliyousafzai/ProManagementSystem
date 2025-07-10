using System.ComponentModel.DataAnnotations.Schema;

namespace ProManagementSystem.Models.Entities
{
    public class RolePermission
    {
        public int Id { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }

        [ForeignKey("Permission")]
        public int PermissionId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual Role Role { get; set; } = null!;
        public virtual Permission Permission { get; set; } = null!;
    }
}
