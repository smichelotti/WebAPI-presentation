using System.Web;
using System.Web.Optimization;

namespace WebApiDemo
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // Custom Script Bundle
            bundles.Add(new ScriptBundle("~/bundles/playbook").Include(
                "~/Scripts/jquery-1.*",
                "~/Scripts/jquery.notifyBar.js",
                "~/Scripts/jquery-ui*",
                "~/Scripts/knockout*",
                "~/Scripts/ko-protected-observable.js",
                "~/Scripts/ajax-util.js",
                "~/Scripts/ko-execute-on-enter.js",
                "~/Scripts/bp-index-final.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            // Custom Style Bundle
            bundles.Add(new StyleBundle("~/Content/playbookstyles").Include(
                "~/Content/jquery.notifyBar.css",
                "~/Content/PlaybookSite.css",
                "~/Content/themes/base/jquery.ui.core.css",
                "~/Content/themes/base/jquery.ui.dialog.css",
                "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}