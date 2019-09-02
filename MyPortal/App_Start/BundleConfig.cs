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
                "~/Scripts/jquery.unobtrusive-ajax.js",
                "~/Scripts/bootstrap.bundle.min.js",
                "~/Scripts/bootbox.min.js",
                "~/Scripts/respond.min.js",
                "~/Scripts/toastr.min.js",
                "~/Scripts/datatables/jquery.datatables.min.js",
                "~/Scripts/datatables/datatables.bootstrap4.min.js",
                "~/Scripts/datatables/datatables.responsive.min.js",
                "~/Scripts/datatables/responsive.bootstrap4.min.js",
                "~/Scripts/moment.min.js",
                "~/Scripts/sb-admin-2.min.js",
                "~/Scripts/myportal.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/syncfusion").Include(
                "~/Scripts/ej2/ej2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/navbar").Include(
                "~/Scripts/MyPortal/NavBar.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/sb-admin-2.css",
                "~/Content/datatables/css/datatables.bootstrap4.min.css",
                "~/Content/datatables/css/responsive.bootstrap4.min.css",
                "~/content/toastr.min.css",
                "~/content/myportal.css",
                "~/content/fontawesome.min.css",
                "~/content/regular.min.css",
                "~/content/solid.min.css",
                "~/Content/brands.min.css",
                "~/Content/site.css",
                "~/Content/ej2/material.css"));
        }
    }
}