using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using Company.Project.Web.Models;
using System.Threading.Tasks;
using Company.Project.Web.Models.Account;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using Company.Project.Identity;
using Company.Project.Identity.IdentityContext;
using Company.Project.Identity.IdentityModel;

namespace Company.Project.Web.Controllers
{
    [SessionAuthorize]
    //[InitializeSimpleMembership]
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            // Seeding DB
            ApplicationDbInitializer DBInitializer = new ApplicationDbInitializer();
            Database.SetInitializer<ApplicationDbContext>(DBInitializer);
            DBInitializer.InitializeDatabase(HttpContext.GetOwinContext().Get<ApplicationDbContext>());

            return View();
        }

        [AllowAnonymous]
        public ActionResult LoginExpired(string returnUrl)
        {
            if (AuthenticationManager != null)
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            }

            if (Session != null)
            {
                Session.Abandon();
            }

            return View(new { returnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserLoginViewModel model, FormCollection form, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (string.IsNullOrEmpty(model.Username) && string.IsNullOrEmpty(model.Email))
            {
                AddErrors("Username or Email required");
                return View(model);
            }

            ApplicationUser user = null;

            if (!string.IsNullOrEmpty(model.Email))
            {
                user = UserManager.FindByEmail(model.Email);
            }

            if (!string.IsNullOrEmpty(model.Username))
            {
                if (user == null)
                {
                    user = UserManager.FindByName(model.Username);
                }
                else
                {
                    if (!user.UserName.Equals(model.Username))
                    {
                        AddErrors("Invalid Username");
                        return View(model);
                    }
                }
            }

            if (user == null)
            {
                AddErrors("User not found");
                return View(model);
            }
            else 
            {
                model.Username = user.UserName;
            }

            // This doesn't count login failures towards lockout only two factor authentication
            // To enable password failures to trigger lockout, change to shouldLockout: true
            SignInStatus result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe.Equals("1"), shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    {
                        Session.Add("_user_id", user.Id.ToString());

                        if (string.IsNullOrEmpty(returnUrl))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return Redirect(returnUrl);
                        }
                    }
                case SignInStatus.LockedOut:
                    {
                        return View("Login");
                    }
                case SignInStatus.RequiresVerification:
                    {
                        return RedirectToAction("Login", new { ReturnUrl = returnUrl });
                    }
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginExpired(UserLoginViewModel model, FormCollection form, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (string.IsNullOrEmpty(model.Username) && string.IsNullOrEmpty(model.Email))
            {
                AddErrors("Username or Email required");
                return View(model);
            }

            ApplicationUser user = null;

            if (!string.IsNullOrEmpty(model.Email))
            {
                user = UserManager.FindByEmail(model.Email);
            }

            if (!string.IsNullOrEmpty(model.Username))
            {
                if (user == null)
                {
                    user = UserManager.FindByName(model.Username);
                }
                else
                {
                    if (!user.UserName.Equals(model.Username))
                    {
                        AddErrors("Invalid Username");
                        return View(model);
                    }
                }
            }

            if (user == null)
            {
                AddErrors("User not found");
                return View(model);
            }
            else
            {
                model.Username = user.UserName;
            }

            // This doesn't count login failures towards lockout only two factor authentication
            // To enable password failures to trigger lockout, change to shouldLockout: true
            SignInStatus result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe.Equals("1"), shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    {
                        Session.Add("_user_id", user.Id.ToString());

                        if (string.IsNullOrEmpty(returnUrl))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return Redirect(returnUrl);
                        }
                    }
                case SignInStatus.LockedOut:
                    {
                        return View("Login");
                    }
                case SignInStatus.RequiresVerification:
                    {
                        return RedirectToAction("Login", new { ReturnUrl = returnUrl });
                    }
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl)
        {
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            var user = await UserManager.FindByIdAsync(await SignInManager.GetVerifiedUserIdAsync());
            if (user != null)
            {
                ViewBag.Status = "For DEMO purposes the current " + provider + " code is: " + await UserManager.GenerateTwoFactorTokenAsync(user.Id, provider);
            }
            return View(new VerifyUserCodeViewModel { Provider = provider, ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyUserCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: false, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            SetViewData(null);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SetViewData(model.GroupLevelId);

                    var user = new ApplicationUser
                    {
                        UserName = model.Email,
                        Email = model.Email
                    };

                    var result = await UserManager.CreateAsync(user, model.Password);
                    result = await UserManager.SetLockoutEnabledAsync(user.Id, false);

                    var groupresult = await GroupManager.FindByIdAsync(model.GroupLevelId);

                    if (groupresult == null)
                    {
                        AddErrors(
                            new IdentityResult("Invalid Group Level")
                            );
                        return Register();
                    }

                    result = GroupManager.SetUserGroups(user.Id, new string[] { groupresult.Id });

                    if (result.Succeeded)
                    {
                        return Login();
                    }
                    else
                    {
                        AddErrors(result);
                        return Register();
                    }
                }
                catch (Exception E)
                {
                    AddErrors(
                            new IdentityResult(E.Message)
                            );
                    return Register();
                }
            }
            else
            {
                return Register();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        public ActionResult LogOff(string returnUrl)
        {
            return View();
        }

        public ActionResult Index(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionAuthorize]
        public ActionResult LogOff()
        {
            if (AuthenticationManager != null)
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            }

            if (Session != null)
            {
                Session.Abandon();
            }

            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        #region Helpers

        public void SetViewData(string GroupLevevlId)
        {
            if (String.IsNullOrEmpty(GroupLevevlId))
            {
            }
            else
            {
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        #endregion
    }
}