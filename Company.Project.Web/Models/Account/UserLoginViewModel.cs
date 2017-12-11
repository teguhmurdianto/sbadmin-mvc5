using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Company.Project.Web.Models.Account
{
    public class UserLoginViewModel
    {
        private int? _ApplicationType { get; set; }
        private string _RememberMe { get; set; }

        public UserLoginViewModel()
        {
            this._ApplicationType = 1;
        }

        [Display(Name = "Username")]
        [RequiredIfEmpty("Email")]
        public string Username { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        [RequiredIfEmpty("Username")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public string RememberMe
        {
            get
            {
                return string.IsNullOrEmpty(_RememberMe) ? string.Empty : _RememberMe;
            }
            set
            {
                if (value == null)
                {
                    _RememberMe = string.Empty;
                }
                else
                {
                    _RememberMe = value;
                }
            }
        }

        [Required]
        [Display(Name = "Application Type")]
        public int? ApplicationType
        {
            get
            {
                return _ApplicationType == null ? 1 : _ApplicationType;
            }
            set
            {
                if (value == null)
                {
                    _ApplicationType = 1;
                }
                else
                {
                    _ApplicationType = value;
                }
            }
        }
    }
}