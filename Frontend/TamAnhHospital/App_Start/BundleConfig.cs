using System.Web.Optimization;

namespace TamAnhHospital
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        //"~/Scripts/jquery-{version}.js",
                        "~/scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Global/css").Include(
                "~/Content/btr-menu.min.css",
                "~/Content/datepicker.css",
                "~/Content/lightbox.css",
                "~/content/chosen.min.css"));

            bundles.Add(new ScriptBundle("~/Global/js").Include(
                "~/scripts/jquery.validate.min.js",
                "~/scripts/jquery.validate.unobtrusive.min.js",
                "~/scripts/btr-menu.min.js",
                "~/scripts/init.js",
                "~/scripts/calendar/moment.min.js",
                "~/scripts/datepicker.js",
                "~/scripts/detectbrowser.js",
                "~/scripts/chosen.min.js",
                "~/scripts/lightbox.js",
                "~/scripts/jQuery-ui.js",
                "~/scripts/signature_pad.min.js"));

            bundles.Add(new StyleBundle("~/dataTable/css").Include(
                "~/Content/datatables/datatables.min.css",
                "~/Content/datatables/datatables.bootstrap4.min.css",
                "~/Content/datatables/fixedHeader.dataTables.min.css",
                "~/Content/datatables/datatables.plugin.min.css"
            ));
            bundles.Add(new ScriptBundle("~/dataTable/js").Include(
                    "~/scripts/datatables/datatables.min.js",
                    "~/scripts/datatables/datatables.bootstrap4.min.js",
                    "~/scripts/datatables/datatables.plugin.min.js",
                    "~/scripts/datatables/dataTables.fixedHeader.min.js",
                    "~/scripts/datatables/datetime-moment.js",
                    "~/scripts/datatables/datetime.js",
                    "~/scripts/datatables/moment.min.js"
            ));

            bundles.Add(new StyleBundle("~/bundles/calendarCss").Include(
                      "~/scripts/calendar/fullcalendar.min.css"
                      //"~/scripts/calendar/fullcalendar.print.min.css"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/calendarJs").Include(
                      "~/Scripts/calendar/moment.min.js",
                      "~/Scripts/calendar/fullcalendar.min.js",
                      "~/Scripts/calendar/locale-all.js"));

            #region IVF LIB

            string[] ivf_scripts = {
                "~/scripts/jquery-1.10.2.min.js",
                "~/scripts/bootstrap.min.js",
                "~/scripts/jquery.validate.min.js",
                "~/scripts/jquery.validate.unobtrusive.min.js",
                "~/scripts/modernizr-2.6.2.js",
                "~/scripts/respond.min.js",
                "~/scripts/chosen.min.js",
                "~/scripts/jquery.toast.min.js",
                "~/scripts/printPreview.js",
                "~/scripts/mask.js",
                "~/areas/ivf/content/init.js",
                "~/areas/ivf/content/site.js"
            };

            string[] ivf_css = {
                "~/content/chosen.min.css",
                "~/content/bootstrap.min.css",
                "~/content/jquery.toast.min.css",
                "~/areas/ivf/content/site.css"
            };

            bundles.Add(new ScriptBundle("~/ivf/scripts").Include(ivf_scripts));
            bundles.Add(new StyleBundle("~/ivf/css").Include(ivf_css));

            #endregion IVF LIB

            //BundleTable.EnableOptimizations = true;
        }
    }
}