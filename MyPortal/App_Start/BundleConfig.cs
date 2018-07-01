﻿using System.Web.Optimization;

namespace MyPortal
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/bootstrap.js",
                "~/scripts/bootbox.js",
                "~/Scripts/respond.js",
                "~/scripts/toastr.js",
                "~/scripts/datatables/jquery.datatables.js",
                "~/scripts/datatables/datatables.bootstrap.js"
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
                "~/content/datatables/css/datatables.bootstrap.css",
                "~/content/toastr.css",
                "~/content/myportal.css",
                "~/Content/site.css"));
        }
    }
}