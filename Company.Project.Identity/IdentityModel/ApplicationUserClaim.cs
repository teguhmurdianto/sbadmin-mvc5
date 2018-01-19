using Company.Project.Object.General;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.Identity.IdentityModel
{
    public class ApplicationUserClaim : IdentityUserClaim<string>
    {
        public DateTime created_time { get; set; }
        
        [StringLength(128)]
        public string created_by { get; set; }
        
        public DateTime? modified_time { get; set; }

        [StringLength(128)]
        public string modified_by { get; set; }
        
        public int row_status { get; set; }

        public ApplicationUserClaim()
        {
            created_time = DateTime.Now;
            created_by = GlobalEnum.SystemName.System;
            row_status = GlobalEnum.RowStatus.Active;
        }
    }
}
