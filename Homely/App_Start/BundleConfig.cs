using System.Web;
using System.Web.Optimization;

namespace Homely
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
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            #region Index page scripts

            bundles.Add(new ScriptBundle("~/bundles/index").Include(
                      "~/Scripts/js/bootstrap.min.js",
                       "~/Scripts/ui-range/jquery.easing.1.3.js",
                       "~/Scripts/ui-range/jquery.BA.ToolTip.js",
                        "~/Scripts/HomePage/index.js",
                         "~/Scripts/js/jquery-ui.js",
                          "~/Scripts/Global/globalFile.js",
                          "~/Scripts/ui-range/jquery.easing.min.js",
                          "~/Scripts/ui-range/jquery.easy-ticker.js",
                          "~/Content/date-picker/jquery.datetimepicker.full.js"));

            bundles.Add(new StyleBundle("~/Content/indexCss").Include(
                     "~/Content/bootstrap.min.css",
                     "~/Content/style.css",
                     "~/Content/custom.css",
                     "~/Content/font-awesome.min.css",
                     "~/Content/date-picker/jquery.datetimepicker.css",
                     "~/Content/default.css",
                     "~/Content/component.css"
                     ));

            #endregion

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
