using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Company.Project.Identity.IdentityAccess;
using Company.Project.Identity.IdentityContext;
using Company.Project.Identity.IdentityModel;
using Company.Project.Object.General;


namespace Company.Project.Identity
{
    // This is useful if you do not want to tear down the database each time you run the application.
    // public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    // This example shows you how to create a new database if the Model changes
    public class ApplicationDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        public ApplicationDbInitializer() { }

        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }

        public static void InitializeIdentityForEF(ApplicationDbContext db)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            const string userName = "adminweb";
            const string emailName = "admin@company.com";
            const string password = "Admin@123";
            const string roleName = "SuperAdmin";
            const string roleDesc = "Super Admin Full Access to All";

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new ApplicationRole(roleName, roleDesc);
                var roleresult = roleManager.Create(role);
                if (!roleresult.Succeeded)
                {
                    throw new Exception(roleresult.Errors.ElementAt(0));
                }

                var groupManager = new ApplicationGroupManager();
                string groupName = "SuperAdmin";
                string groupDesc = "Super Admin Full Access to All";
                var group = groupManager.FindByName(groupName);
                if (group == null)
                {
                    var newGroup = new ApplicationGroup(groupName, groupDesc);
                    var groupresult = groupManager.CreateGroup(newGroup);
                    if (!groupresult.Succeeded)
                    {
                        roleresult = roleManager.Delete(role);
                        if (!roleresult.Succeeded)
                        {
                            throw new Exception(roleresult.Errors.ElementAt(0));
                        }
                    }

                    var user = userManager.FindByName(userName);
                    var emailUser = userManager.FindByEmail(emailName);
                    if (user == null && emailUser == null)
                    {
                        user = new ApplicationUser { UserName = userName, Email = emailName, CreatedTime = DateTime.Now, CreatedBy = GlobalEnum.SystemName.System, RowStatus = GlobalEnum.RowStatus.Active };
                        var userresult = userManager.Create(user, password);
                        userresult = userManager.SetLockoutEnabled(user.Id, false);
                        if (!userresult.Succeeded)
                        {
                            roleresult = roleManager.Delete(role);
                            if (!roleresult.Succeeded)
                            {
                                throw new Exception(roleresult.Errors.ElementAt(0));
                            }

                            groupresult = groupManager.DeleteGroup(group.Id);
                            if (!groupresult.Succeeded)
                            {
                                throw new Exception(groupresult.Errors.ElementAt(0));
                            }
                        }

                        groupManager.SetUserGroups(user.Id, new string[] { newGroup.Id });
                        groupManager.SetGroupRoles(newGroup.Id, new string[] { role.Name });
                    }
                }
            }
        }
    }
}
