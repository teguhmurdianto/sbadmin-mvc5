using Company.Project.Object.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.Identity.IdentityModel
{
    public class ApplicationGroup
    {
        public DateTime created_time { get; set; }

        [StringLength(128)]
        public string created_by { get; set; }
        
        public DateTime? modified_time { get; set; }

        [StringLength(128)]
        public string modified_by { get; set; }
        
        public int row_status { get; set; }

        public ApplicationGroup()
        {
            this.id = Guid.NewGuid().ToString();
            this.ApplicationRoles = new List<ApplicationGroupRole>();
            this.ApplicationUsers = new List<ApplicationUserGroup>();
            this.created_time = DateTime.Now;
            this.created_by = GlobalEnum.SystemName.System;
            this.row_status = GlobalEnum.RowStatus.Active;
        }

        public ApplicationGroup(string name)
            : this()
        {
            this.name = name;
        }

        public ApplicationGroup(string name, string description)
            : this(name)
        {
            this.description = description;
        }

        [Key]
        [StringLength(128)]
        public string id { get; set; }

        [Display(Name = "Name")]
        [StringLength(25, ErrorMessage = "Max length 25 characters")]
        [Required(ErrorMessage = "Please Input Application Group Name")]
        public string name { get; set; }

        [Display(Name = "Description")]
        [StringLength(50, ErrorMessage = "Max length 50 characters")]
        [Required(ErrorMessage = "Please Input Application Group Description ")]
        public string description { get; set; }
        public virtual ICollection<ApplicationGroupRole> ApplicationRoles { get; set; }
        public virtual ICollection<ApplicationUserGroup> ApplicationUsers { get; set; }
    }
}
