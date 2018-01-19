using System.Web;
using System.Web.Optimization;

namespace Company.Project.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region bundles js

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/metisMenu.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-dropdownhover").Include(
                      "~/Vendor/bootstrap-dropdown-hover/js/bootstrap-dropdownhover.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                      "~/Vendor/datatables/media/js/jquery.dataTables.min.js",
                      "~/Vendor/datatables-plugins/integration/bootstrap/3/dataTables.bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/raphael").Include(
                      "~/Vendor/raphael/raphael-min.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                      "~/Vendor/carhartl-jquery-cookie/jquery.cookie.js",
                      "~/Scripts/sb-admin-2.js"));

            #endregion

            #region bundles style

            bundles.Add(new StyleBundle("~/bundles/bootstrap_css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/metisMenu.min.css",
                      "~/Content/timeline.css"));

            bundles.Add(new StyleBundle("~/bundles/animate_css").Include(
                      "~/Vendor/animate.css/animate.min.css"));

            bundles.Add(new StyleBundle("~/bundles/bootstrap-dropdownhover_css").Include(
                      "~/Vendor/bootstrap-dropdown-hover/css/bootstrap-dropdownhover.min.css"));

            bundles.Add(new StyleBundle("~/bundles/morris_css").Include(
                      "~/Vendor/morrisjs/morris.css"));

            bundles.Add(new StyleBundle("~/bundles/datatables_css").Include(
                      "~/Vendor/datatables-plugins/integration/bootstrap/3/dataTables.bootstrap.css",
                      "~/Vendor/datatables-responsive/css/dataTables.responsive.css"));

            bundles.Add(new StyleBundle("~/bundles/app_css").Include(
                      "~/Content/sb-admin-2.css"));

            #endregion

            BundleTable.EnableOptimizations = true;
        }
    }
}