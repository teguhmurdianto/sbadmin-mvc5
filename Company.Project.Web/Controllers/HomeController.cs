using Company.Project.Business;
using Company.Project.Object.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Company.Project.Web.Controllers
{
    [SessionAuthorize]
    public class HomeController : BaseController
    {
        [SessionAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [SessionAuthorize]
        public ActionResult FlotCharts()
        {
            return View("FlotCharts");
        }

        [SessionAuthorize]
        public ActionResult MorrisCharts()
        {
            return View("MorrisCharts");
        }

        [SessionAuthorize]
        public ActionResult Tables()
        {
            return View("Tables");
        }

        [SessionAuthorize]
        public ActionResult Forms()
        {
            return View("Forms");
        }

        [SessionAuthorize]
        public ActionResult Panels()
        {
            return View("Panels");
        }

        [SessionAuthorize]
        public ActionResult Buttons()
        {
            return View("Buttons");
        }

        [SessionAuthorize]
        public ActionResult Notifications()
        {
            return View("Notifications");
        }

        [SessionAuthorize]
        public ActionResult Typography()
        {
            return View("Typography");
        }

        [SessionAuthorize]
        public ActionResult Icons()
        {
            return View("Icons");
        }

        [SessionAuthorize]
        public ActionResult Grid()
        {
            return View("Grid");
        }

        [SessionAuthorize]
        public ActionResult Blank()
        {
            BooksBusinessLogic booksBL = new BooksBusinessLogic();
            ResultStatus resultStat = booksBL.UpdateBooks(string.Empty);

            return View("Blank");
        }
    }
}
