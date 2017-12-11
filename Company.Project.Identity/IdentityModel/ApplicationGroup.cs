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
        public DateTime CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public string ModifiedBy { get; set; }
        public int RowStatus { get; set; }

        public ApplicationGroup()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ApplicationRoles = new List<ApplicationGroupRole>();
            this.ApplicationUsers = new List<ApplicationUserGroup>();
            this.CreatedTime = DateTime.Now;
            this.CreatedBy = GlobalEnum.SystemName.System;
            this.RowStatus = GlobalEnum.RowStatus.Active;
        }

        public ApplicationGroup(string name)
            : this()
        {
            this.Name = name;
        }

        public ApplicationGroup(string name, string description)
            : this(name)
        {
            this.Description = description;
        }

        [Key]
        public string Id { get; set; }

        [Display(Name = "Name")]
        [StringLength(25, ErrorMessage = "Max length 25 characters")]
        [Required(ErrorMessage = "Please Input Application Group Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(50, ErrorMessage = "Max length 50 characters")]
        [Required(ErrorMessage = "Please Input Application Group Description ")]
        public string Description { get; set; }
        public virtual ICollection<ApplicationGroupRole> ApplicationRoles { get; set; }
        public virtual ICollection<ApplicationUserGroup> ApplicationUsers { get; set; }
    }
}
