using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using Company.Project.Identity.IdentityModel;

namespace Company.Project.Identity.IdentityContext
{
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser, ApplicationRole,
        string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        static ApplicationDbContext()
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual IDbSet<ApplicationGroup> ApplicationGroups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("MasterUser", "dbo");
            modelBuilder.Entity<ApplicationUser>().Property(a => a.UserName).
                HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("IDX_MasterUser", 1) { IsUnique = true }));
            modelBuilder.Entity<ApplicationUser>().Property(a => a.Email).
                HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("IDX_MasterUser", 2) { IsUnique = true }));

            modelBuilder.Entity<ApplicationUserClaim>().ToTable("MasterUserClaim", "dbo");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("MasterUserLogin", "dbo");

            modelBuilder.Entity<ApplicationRole>().ToTable("MasterRole", "dbo");
            modelBuilder.Entity<ApplicationRole>().Property(a => a.Name).
                HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("IDX_MasterRole", 1) { IsUnique = true }));

            modelBuilder.Entity<ApplicationUserRole>().ToTable("MasterUserRole", "dbo");

            modelBuilder.Entity<ApplicationGroup>().ToTable("MasterGroup", "dbo");
            modelBuilder.Entity<ApplicationGroup>().Property(a => a.Name).
                HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("IDX_MasterGroup", 1) { IsUnique = true }));
            modelBuilder.Entity<ApplicationGroup>()
                .HasMany<ApplicationGroupRole>((ApplicationGroup g) => g.ApplicationRoles)
                .WithRequired().HasForeignKey<string>((ApplicationGroupRole ap) => ap.ApplicationGroupId);

            modelBuilder.Entity<ApplicationGroup>()
                .HasMany<ApplicationUserGroup>((ApplicationGroup g) => g.ApplicationUsers)
                .WithRequired().HasForeignKey<string>((ApplicationUserGroup ag) => ag.ApplicationGroupId);

            modelBuilder.Entity<ApplicationUserGroup>()
                .HasKey((ApplicationUserGroup r) =>
                    new
                    {
                        ApplicationUserId = r.ApplicationUserId,
                        ApplicationGroupId = r.ApplicationGroupId
                    }).ToTable("MasterUserGroup", "dbo");

            modelBuilder.Entity<ApplicationGroupRole>().HasKey((ApplicationGroupRole gr) =>
                new
                {
                    ApplicationRoleId = gr.ApplicationRoleId,
                    ApplicationGroupId = gr.ApplicationGroupId
                }).ToTable("MasterGroupRole", "dbo");

        }
    }
}
