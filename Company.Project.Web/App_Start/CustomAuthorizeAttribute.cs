using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Routing;

namespace Company.Project.Web
{
    //authorize access and checking session
    public class SessionAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }

            var user_id = httpContext.User.Identity.GetUserId();
            var session = httpContext.Session["_user_id"];

            if (user_id != null && session != null)
            {
                if (string.Compare(user_id.ToString(), session.ToString(), StringComparison.Ordinal) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var session = filterContext.HttpContext.Session["_user_id"];

            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult( 
                                            new RouteValueDictionary( 
                                                new {
                                                      controller = "Account",
                                                      action = "LoginExpired",
                                                      returnUrl = filterContext.HttpContext.Request.Url.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped)
                                                }));
            }
        }
    }

    //below for another custom authorization
    //public class SessionAuthorizeAttribute : AuthorizeAttribute
    //{
    //    public bool AuthorizeCore(HttpContextBase httpContext)
    //    {
    //        var isAuthorized = base.AuthorizeCore(httpContext);
    //        if (!isAuthorized)
    //        {
    //            return false;
    //        }

    //        string username = httpContext.User.Identity.GetUserId();

    //        var session = httpContext.Session["_app_access_id"];

    //        return base.AuthorizeCore(httpContext);
    //    }
    //}
}