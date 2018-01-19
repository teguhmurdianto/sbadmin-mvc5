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

            modelBuilder.Entity<ApplicationUser>().ToTable("m_user", "dbo");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.Id).HasColumnName("id");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.Email).HasColumnName("email");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.EmailConfirmed).HasColumnName("email_confirmed");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.PasswordHash).HasColumnName("password_hash");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.SecurityStamp).HasColumnName("security_stamp");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.PhoneNumber).HasColumnName("phone_number");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.PhoneNumberConfirmed).HasColumnName("phone_number_confirmed");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.TwoFactorEnabled).HasColumnName("two_factor_enabled");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.LockoutEndDateUtc).HasColumnName("lockout_end_date_utc");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.LockoutEnabled).HasColumnName("lockout_enabled");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.AccessFailedCount).HasColumnName("access_failed_count");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.UserName).HasColumnName("username");
            modelBuilder.Entity<ApplicationUser>().Property(a => a.UserName).
                HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("idx_m_user", 1) { IsUnique = true }));
            modelBuilder.Entity<ApplicationUser>().Property(a => a.Email).
                HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("idx_m_user", 2) { IsUnique = true }));

            modelBuilder.Entity<ApplicationUserClaim>().ToTable("m_user_claim", "dbo");
            modelBuilder.Entity<ApplicationUserClaim>().Property(p => p.Id).HasColumnName("id");
            modelBuilder.Entity<ApplicationUserClaim>().Property(p => p.UserId).HasColumnName("user_id");
            modelBuilder.Entity<ApplicationUserClaim>().Property(p => p.ClaimType).HasColumnName("claim_type");
            modelBuilder.Entity<ApplicationUserClaim>().Property(p => p.ClaimValue).HasColumnName("claim_value");

            modelBuilder.Entity<ApplicationUserLogin>().ToTable("m_user_login", "dbo");
            modelBuilder.Entity<ApplicationUserLogin>().Property(p => p.id).HasColumnName("id");
            modelBuilder.Entity<ApplicationUserLogin>().Property(p => p.UserId).HasColumnName("user_id");
            modelBuilder.Entity<ApplicationUserLogin>().Property(p => p.LoginProvider).HasColumnName("login_provider");
            modelBuilder.Entity<ApplicationUserLogin>().Property(p => p.ProviderKey).HasColumnName("provider_key");

            modelBuilder.Entity<ApplicationRole>().ToTable("m_role", "dbo");
            modelBuilder.Entity<ApplicationRole>().Property(p => p.Id).HasColumnName("id");
            modelBuilder.Entity<ApplicationRole>().Property(p => p.Name).HasColumnName("name");
            modelBuilder.Entity<ApplicationRole>().Property(a => a.Name).
                HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("idx_m_role", 1) { IsUnique = true }));

            modelBuilder.Entity<ApplicationUserRole>().ToTable("m_user_role", "dbo");
            modelBuilder.Entity<ApplicationUserRole>().Property(p => p.RoleId).HasColumnName("role_id");
            modelBuilder.Entity<ApplicationUserRole>().Property(p => p.UserId).HasColumnName("user_id");

            modelBuilder.Entity<ApplicationGroup>().ToTable("m_group", "dbo");
            modelBuilder.Entity<ApplicationGroup>().Property(a => a.id).HasColumnName("id");
            modelBuilder.Entity<ApplicationGroup>().Property(a => a.name).HasColumnName("name");
            modelBuilder.Entity<ApplicationGroup>().Property(a => a.name).
                HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("idx_m_group", 1) { IsUnique = true }));
            modelBuilder.Entity<ApplicationGroup>()
                .HasMany<ApplicationGroupRole>((ApplicationGroup g) => g.ApplicationRoles)
                .WithRequired().HasForeignKey<string>((ApplicationGroupRole ap) => ap.app_group_id);

            modelBuilder.Entity<ApplicationGroup>()
                .HasMany<ApplicationUserGroup>((ApplicationGroup g) => g.ApplicationUsers)
                .WithRequired().HasForeignKey<string>((ApplicationUserGroup ag) => ag.app_group_id);

            modelBuilder.Entity<ApplicationUserGroup>()
                .HasKey((ApplicationUserGroup r) =>
                    new
                    {
                        app_user_id = r.app_user_id,
                        app_group_id = r.app_group_id
                    }).ToTable("m_user_group", "dbo");

            modelBuilder.Entity<ApplicationGroupRole>().HasKey((ApplicationGroupRole gr) =>
                new
                {
                    app_role_id = gr.app_role_id,
                    app_group_id = gr.app_group_id
                }).ToTable("m_group_role", "dbo");
        }
    }
}