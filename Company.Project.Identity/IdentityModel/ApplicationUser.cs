using Company.Project.Object.General;
using Company.Project.Identity.IdentityAccess;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Company.Project.Identity.IdentityModel
{
    public class ApplicationUser : IdentityUser<string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public DateTime created_time { get; set; }

        [StringLength(128)]
        public string created_by { get; set; }

        public DateTime? modified_time { get; set; }

        [StringLength(128)]
        public string modified_by { get; set; }

        public int row_status { get; set; }

        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString();
            created_time = DateTime.Now;
            created_by = GlobalEnum.SystemName.System;
            row_status = GlobalEnum.RowStatus.Active;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}
