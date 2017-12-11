using Company.Project.Object.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.Identity.IdentityModel
{
    public class ApplicationUserGroup
    {
        public string Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public string ModifiedBy { get; set; }
        public int RowStatus { get; set; }

        public string ApplicationUserId { get; set; }
        public string ApplicationGroupId { get; set; }

        public ApplicationUserGroup()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedTime = DateTime.Now;
            this.CreatedBy = GlobalEnum.SystemName.System;
            this.RowStatus = GlobalEnum.RowStatus.Active;
        }
    }
}
