using Company.Project.Object.General;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.Identity.IdentityModel
{
    public class ApplicationRole : IdentityRole<string, ApplicationUserRole>
    {
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public string ModifiedBy { get; set; }
        public int RowStatus { get; set; }

        public ApplicationRole()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedTime = DateTime.Now;
            this.CreatedBy = GlobalEnum.SystemName.System;
            this.RowStatus = GlobalEnum.RowStatus.Active;
        }

        public ApplicationRole(string name, string desc)
            : this()
        {
            this.Name = name;
            this.Description = desc;
        }
    }
}
