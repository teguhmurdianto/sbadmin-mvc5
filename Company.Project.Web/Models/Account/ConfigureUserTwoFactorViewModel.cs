using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Project.Web.Models.Account
{
    public class ConfigureUserTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}