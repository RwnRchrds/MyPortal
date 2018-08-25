using System.Web.Optimization;

namespace MyPortal
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/bootstrap.min.js",
                "~/scripts/bootbox.min.js",
                "~/Scripts/respond.min.js",
                "~/scripts/toastr.min.js",
                "~/scripts/datatables/jquery.datatables.min.js",
                "~/scripts/datatables/datatables.bootstrap4.min.js",
                "~/scripts/datatables/datatables.responsive.min.js",
                "~/scripts/datatables/responsive.bootstrap4.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/staff").Include(
                "~/Scripts/MyPortal/StaffNavBar.js"));

            bundles.Add(new ScriptBundle("~/bundles/students").Include(
                "~/Scripts/MyPortal/StudentNavBar.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap-flatly.css",
                "~/content/datatables/css/datatables.bootstrap4.min.css",
                "~/content/datatables/css/responsive.bootstrap4.min.css",
                "~/content/toastr.min.css",
                "~/content/myportal.css",
                "~/content/fontawesome.min.css",
                "~/content/regular.min.css",
                "~/content/solid.min.css",
                "~/Content/site.css"));
        }
    }
}