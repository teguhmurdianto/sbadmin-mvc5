using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Company.Project.Object.General;

namespace Company.Project.Identity.IdentityModel
{
    public class ApplicationUserLogin : IdentityUserLogin<string>
    {
        public string Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public string ModifiedBy { get; set; }
        public int RowStatus { get; set; }

        public ApplicationUserLogin()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedTime = DateTime.Now;
            this.CreatedBy = GlobalEnum.SystemName.System;
            this.RowStatus = GlobalEnum.RowStatus.Active;
        }
    }
}
