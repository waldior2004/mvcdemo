using System.Web;
using System.Web.Optimization;

namespace com.msc.frontend.mvc
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Content/vendors/bootstrap/dist/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/SiteGentelella").Include(
                        "~/Scripts/SiteGentelella.js"));

            bundles.Add(new StyleBundle("~/Content/Login").Include(
                      "~/Content/vendors/bootstrap/dist/css/bootstrap.min.css",
                      "~/Content/vendors/font-awesome/css/font-awesome.min.css",
                      "~/Content/vendors/nprogress/nprogress.css",
                      "~/Content/vendors/animate.css/animate.min.css",
                      "~/Content/build/css/custom.min.css",
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/DataTables").Include(
                      "~/Content/vendors/datatables.net-bs/css/dataTables.bootstrap.min.css",
                      "~/Content/vendors/datatables.net-buttons-bs/css/buttons.bootstrap.min.css",
                      "~/Content/vendors/datatables.net-fixedheader-bs/css/fixedHeader.bootstrap.min.css",
                      "~/Content/vendors/datatables.net-responsive-bs/css/responsive.bootstrap.min.css",
                      "~/Content/vendors/datatables.net-scroller-bs/css/scroller.bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/Home").Include(
                      "~/Content/vendors/bootstrap/dist/css/bootstrap.min.css",
                      "~/Content/vendors/font-awesome/css/font-awesome.min.css",
                      "~/Content/vendors/nprogress/nprogress.css",
                      "~/Content/vendors/iCheck/skins/flat/green.css",
                      "~/Content/vendors/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css",
                      "~/Content/vendors/jqvmap/dist/jqvmap.min.css",
                      "~/Content/build/css/custom.min.css",
                      "~/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/bundles/DataTables").Include(
                        "~/Content/vendors/datatables.net/js/jquery.dataTables.min.js",
                        "~/Content/vendors/datatables.net-bs/js/dataTables.bootstrap.min.js",
                        "~/Content/vendors/datatables.net-buttons/js/dataTables.buttons.min.js",
                        "~/Content/vendors/datatables.net-buttons-bs/js/buttons.bootstrap.min.js",
                        "~/Content/vendors/datatables.net-buttons/js/buttons.flash.min.js",
                        "~/Content/vendors/datatables.net-buttons/js/buttons.html5.min.js",
                        "~/Content/vendors/datatables.net-buttons/js/buttons.print.min.js",
                        "~/Content/vendors/datatables.net-fixedheader/js/dataTables.fixedHeader.min.js",
                        "~/Content/vendors/datatables.net-keytable/js/dataTables.keyTable.min.js",
                        "~/Content/vendors/datatables.net-responsive/js/dataTables.responsive.min.js",
                        "~/Content/vendors/datatables.net-responsive-bs/js/responsive.bootstrap.js",
                        "~/Content/vendors/datatables.net-scroller/js/dataTables.scroller.min.js",
                        "~/Content/vendors/jszip/dist/jszip.min.js",
                        "~/Content/vendors/pdfmake/build/pdfmake.min.js",
                        "~/Content/vendors/pdfmake/build/vfs_fonts.js"));

            bundles.Add(new ScriptBundle("~/bundles/Home").Include(
                        "~/Content/vendors/jquery/dist/jquery.min.js",
                        "~/Content/vendors/tether-master/dist/js/tether.min.js",
                        "~/Content/vendors/bootstrap/dist/js/bootstrap.min.js",
                        "~/Content/vendors/fastclick/lib/fastclick.js",
                        "~/Content/vendors/nprogress/nprogress.js",
                        "~/Content/vendors/devbridge-autocomplete/dist/jquery.autocomplete.min.js",
                        "~/Content/vendors/Chart.js/dist/Chart.bundle.min.js",
                        "~/Content/vendors/bootstrap-progressbar/bootstrap-progressbar.min.js",
                        "~/Content/vendors/iCheck/icheck.min.js",
                        "~/Content/vendors/DateJS/build/date.js",
                        "~/Content/vendors/moment/min/moment.min.js",
                        "~/Content/build/js/custom.min.js",
                        "~/Scripts/SiteGentelella.js"));
        }
    }
}
