using Company.Project.Object.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.Identity.IdentityModel
{
    public class ApplicationGroupRole
    {
        [Key]
        [StringLength(128)]
        public string id { get; set; }

        public DateTime created_time { get; set; }

        [StringLength(128)]
        public string created_by { get; set; }
        
        public DateTime? modified_time { get; set; }
        
        [StringLength(128)]
        public string modified_by { get; set; }
        
        public int row_status { get; set; }

        public string app_group_id { get; set; }
        public string app_role_id { get; set; }

        public ApplicationGroupRole()
        {
            this.id = Guid.NewGuid().ToString();
            this.created_time = DateTime.Now;
            this.created_by = GlobalEnum.SystemName.System;
            this.row_status = GlobalEnum.RowStatus.Active;
        }
    }
}
