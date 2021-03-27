using System.Web;
using System.Web.Optimization;

namespace Notes_MarketPlace
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Admin/css").Include(
                      "~/Content/Admin/bootstrap/bootstrap.css",
                      "~/Content/Admin/responsive.css",
                      "~/Content/Admin/style.css"));

            bundles.Add(new StyleBundle("~/User/css").Include(
                      "~/Content/Custom/bootstrap/bootstrap.css",
                      "~/Content/Custom/responsive2.css",
                      "~/Content/Custom/style2.css"));

            bundles.Add(new StyleBundle("~/fonts/css").Include(
                      "~/fonts/Custom/css/font-awesome.css",
                      "~/fonts/Custom/fontawesome/css/fontawesome.css",
                      "~/fonts/Custom/fontawesome/css/brands.css",
                      "~/fonts/Custom/fontawesome/css/solid.css"));

            bundles.Add(new ScriptBundle("~/Scripts").Include(
                        "~/Scripts/Admin/jquery.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Admin").Include(
                        "~/Scripts/Admin/script.js",
                        "~/Scripts/Admin/popper.min.js",
                        "~/Scripts/Admin/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/User").Include(
                        "~/Scripts/Custom/popper.min.js",
                        "~/Scripts/Custom/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/HomeScript").Include(
                        "~/Scripts/Custom/script.js"));

            bundles.Add(new ScriptBundle("~/MemberScript").Include(
                        "~/Scripts/Custom/script3.js"));
        }
    }
}
