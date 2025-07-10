using Microsoft.EntityFrameworkCore;
using ProManagementSystem.Models.Entities;

namespace ProManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<Module> Modules { get; set; }
        public DbSet<SubModule> SubModules { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Module>()
                .HasMany(m => m.SubModules)
                .WithOne(sm => sm.Module)
                .HasForeignKey(sm => sm.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubModule>()
                .HasMany(sm => sm.Permissions)
                .WithOne(p => p.SubModule)
                .HasForeignKey(p => p.SubModuleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.RolePermissions)
                .WithOne(rp => rp.Role)
                .HasForeignKey(rp => rp.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Permission>()
                .HasMany<RolePermission>()
                .WithOne(rp => rp.Permission)
                .HasForeignKey(rp => rp.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure unique constraints
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<Module>()
                .HasIndex(m => m.Name)
                .IsUnique();

            // Seed initial data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Administrator", Description = "Full system access", IsActive = true },
                new Role { Id = 2, Name = "Manager", Description = "Management level access", IsActive = true },
                new Role { Id = 3, Name = "User", Description = "Basic user access", IsActive = true }
            );

            // Seed Modules
            modelBuilder.Entity<Module>().HasData(
                new Module { Id = 1, Name = "User Management", Description = "Manage users and roles", Icon = "fa-users", IsActive = true, SortOrder = 1 },
                new Module { Id = 2, Name = "Module Management", Description = "Manage system modules", Icon = "fa-cube", IsActive = true, SortOrder = 2 },
                new Module { Id = 3, Name = "Reports", Description = "System reports and analytics", Icon = "fa-chart-bar", IsActive = true, SortOrder = 3 },
                new Module { Id = 4, Name = "Settings", Description = "System configuration", Icon = "fa-cog", IsActive = true, SortOrder = 4 }
            );

            // Seed SubModules
            modelBuilder.Entity<SubModule>().HasData(
                // User Management SubModules
                new SubModule { Id = 1, Name = "Users", Description = "Manage users", Icon = "fa-user", Url = "/Users", IsActive = true, SortOrder = 1, ModuleId = 1 },
                new SubModule { Id = 2, Name = "Roles", Description = "Manage roles", Icon = "fa-shield-alt", Url = "/Roles", IsActive = true, SortOrder = 2, ModuleId = 1 },
                new SubModule { Id = 3, Name = "Permissions", Description = "Manage permissions", Icon = "fa-key", Url = "/Permissions", IsActive = true, SortOrder = 3, ModuleId = 1 },

                // Module Management SubModules
                new SubModule { Id = 4, Name = "Modules", Description = "Manage modules", Icon = "fa-cubes", Url = "/Modules", IsActive = true, SortOrder = 1, ModuleId = 2 },
                new SubModule { Id = 5, Name = "Sub Modules", Description = "Manage sub modules", Icon = "fa-cube", Url = "/SubModules", IsActive = true, SortOrder = 2, ModuleId = 2 },

                // Reports SubModules
                new SubModule { Id = 6, Name = "User Reports", Description = "User activity reports", Icon = "fa-chart-line", Url = "/Reports/Users", IsActive = true, SortOrder = 1, ModuleId = 3 },
                new SubModule { Id = 7, Name = "System Reports", Description = "System usage reports", Icon = "fa-chart-pie", Url = "/Reports/System", IsActive = true, SortOrder = 2, ModuleId = 3 },

                // Settings SubModules
                new SubModule { Id = 8, Name = "General Settings", Description = "General system settings", Icon = "fa-cogs", Url = "/Settings/General", IsActive = true, SortOrder = 1, ModuleId = 4 },
                new SubModule { Id = 9, Name = "Security Settings", Description = "Security configuration", Icon = "fa-lock", Url = "/Settings/Security", IsActive = true, SortOrder = 2, ModuleId = 4 }
            );

            // Seed Permissions
            modelBuilder.Entity<Permission>().HasData(
                // User Management Permissions
                new Permission { Id = 1, Name = "View Users", Description = "Can view users", IsActive = true, SubModuleId = 1 },
                new Permission { Id = 2, Name = "Create Users", Description = "Can create users", IsActive = true, SubModuleId = 1 },
                new Permission { Id = 3, Name = "Edit Users", Description = "Can edit users", IsActive = true, SubModuleId = 1 },
                new Permission { Id = 4, Name = "Delete Users", Description = "Can delete users", IsActive = true, SubModuleId = 1 },

                new Permission { Id = 5, Name = "View Roles", Description = "Can view roles", IsActive = true, SubModuleId = 2 },
                new Permission { Id = 6, Name = "Create Roles", Description = "Can create roles", IsActive = true, SubModuleId = 2 },
                new Permission { Id = 7, Name = "Edit Roles", Description = "Can edit roles", IsActive = true, SubModuleId = 2 },
                new Permission { Id = 8, Name = "Delete Roles", Description = "Can delete roles", IsActive = true, SubModuleId = 2 },

                new Permission { Id = 9, Name = "View Permissions", Description = "Can view permissions", IsActive = true, SubModuleId = 3 },
                new Permission { Id = 10, Name = "Manage Permissions", Description = "Can manage permissions", IsActive = true, SubModuleId = 3 },

                // Module Management Permissions
                new Permission { Id = 11, Name = "View Modules", Description = "Can view modules", IsActive = true, SubModuleId = 4 },
                new Permission { Id = 12, Name = "Create Modules", Description = "Can create modules", IsActive = true, SubModuleId = 4 },
                new Permission { Id = 13, Name = "Edit Modules", Description = "Can edit modules", IsActive = true, SubModuleId = 4 },
                new Permission { Id = 14, Name = "Delete Modules", Description = "Can delete modules", IsActive = true, SubModuleId = 4 },

                new Permission { Id = 15, Name = "View SubModules", Description = "Can view sub modules", IsActive = true, SubModuleId = 5 },
                new Permission { Id = 16, Name = "Create SubModules", Description = "Can create sub modules", IsActive = true, SubModuleId = 5 },
                new Permission { Id = 17, Name = "Edit SubModules", Description = "Can edit sub modules", IsActive = true, SubModuleId = 5 },
                new Permission { Id = 18, Name = "Delete SubModules", Description = "Can delete sub modules", IsActive = true, SubModuleId = 5 },

                // Reports Permissions
                new Permission { Id = 19, Name = "View User Reports", Description = "Can view user reports", IsActive = true, SubModuleId = 6 },
                new Permission { Id = 20, Name = "View System Reports", Description = "Can view system reports", IsActive = true, SubModuleId = 7 },

                // Settings Permissions
                new Permission { Id = 21, Name = "View General Settings", Description = "Can view general settings", IsActive = true, SubModuleId = 8 },
                new Permission { Id = 22, Name = "Edit General Settings", Description = "Can edit general settings", IsActive = true, SubModuleId = 8 },
                new Permission { Id = 23, Name = "View Security Settings", Description = "Can view security settings", IsActive = true, SubModuleId = 9 },
                new Permission { Id = 24, Name = "Edit Security Settings", Description = "Can edit security settings", IsActive = true, SubModuleId = 9 }
            );

            // Seed Default Admin User
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "System",
                    LastName = "Administrator",
                    Email = "admin@promanagementsystem.com",
                    Phone = "1234567890",
                    PasswordHash = "$2a$11$rFqmAKVdFWYuOJGIuIvTEOaIqL3YLfM8BjQNjbBkTJvGhEqUhbgCS", // Password: Admin123!
                    IsActive = true,
                    IsEmailVerified = true
                }
            );

            // Assign Admin Role to Default User
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, UserId = 1, RoleId = 1 }
            );
        }
    }
}
