using System.Web;
using System.Web.Optimization;

namespace M19G1
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.5.1.js",
                        "~/Scripts/jquery-3.5.1.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/Site.css",
                      "~/Content/font-awesome.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/bundles/search").Include(
                        "~/Content/Search.css"));
            bundles.Add(new StyleBundle("~/bundles/sidebar").Include(
            "~/Content/Sidebar.css"));
            bundles.Add(new StyleBundle("~/bundles/form").Include(
"~/Content/form.css"));
        }
    }
}
