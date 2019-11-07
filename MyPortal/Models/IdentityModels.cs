using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Migrations.Model;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyPortal.Models
{
    public enum UserType
    {
        Staff,
        Student,
        Parent
    }
    
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, string> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public int? SelectedAcademicYearId { get; set; }
        public UserType UserType { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base()
        {
        }

        public ApplicationRole(string name) : base(name)
        {
        }

        public bool System { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }

    [Table("AspNetPermissions")]
    public class Permission
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Area { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(255)]
        [Required]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }

    [Table("AspNetRolePermissions")]
    public class RolePermission
    {
        public int Id { get; set; }

        [Required]
        public string RoleId { get; set; }

        public int PermissionId { get; set; }
        
        public virtual Permission Permission { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }

    public class IdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public IdentityContext()
            : base("name=MyPortalDbContext")
        {
        }

        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Permission>()
                .HasMany(e => e.RolePermissions)
                .WithRequired(e => e.Permission)
                .HasForeignKey(e => e.PermissionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationRole>()
                .HasMany(e => e.RolePermissions)
                .WithRequired(e => e.Role)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(true);
        }

        public static IdentityContext Create()
        {
            return new IdentityContext();
        }
    }
}