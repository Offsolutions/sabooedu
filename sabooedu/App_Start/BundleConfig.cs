using System.Web;
using System.Web.Optimization;

namespace sabooedu
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/jquery-1.12.4.min.js",
                      "~/Scripts/bootstrap.min.js", "~/Scripts/owl.carousel.min.js", "~/Scripts/jquery.counterup.min.js",
                      "~/Scripts/waypoints.js", "~/Scripts/isotope.pkgd.min.js", "~/Scripts/jquery.stellar.min.js",
                      "~/Scripts/magnific.min.js", "~/Scripts/venobox.min.js", "~/Scripts/jquery.meanmenu.js",
                      "~/Scripts/form-validator.min.js", "~/Scripts/plugins.js", "~/Scripts/main.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/owl.carousel.css", "~/Content/owl.transitions.css", "~/Content/meanmenu.min.css",
                      "~/Content/font-awesome.min.css", "~/Content/icon.css", "~/Content/flaticon.css",
                      "~/Content/magnific.min.css", "~/Content/venobox.css", "~/Content/style.css",
                      "~/Content/responsive.css"));
        }
    }
}
