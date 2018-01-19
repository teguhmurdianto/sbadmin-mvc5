using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Company.Project.Object.General;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Project.Identity.IdentityModel
{
    public class ApplicationUserLogin : IdentityUserLogin<string>
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

        public ApplicationUserLogin()
        {
            id = Guid.NewGuid().ToString();
            created_time = DateTime.Now;
            created_by = GlobalEnum.SystemName.System;
            row_status = GlobalEnum.RowStatus.Active;
        }
    }
}
