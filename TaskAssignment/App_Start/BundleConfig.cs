using System.Web;
using System.Web.Optimization;

namespace TaskAssignment
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {
            // Clear all default settings, make js follow the given order
            bundles.ResetAll();

            #region JS bundle: ~/AdminLTE/js
            bundles.Add(new ScriptBundle("~/AdminLTE/js").Include(
                      // jQuery 3 and JQuery UI 1.11.4
                      "~/bower_components/jquery/dist/jquery.min.js",
                      "~/bower_components/jquery-ui/jquery-ui.min.js",
                      // Bootstrap 3.3.7
                      "~/bower_components/bootstrap/dist/js/bootstrap.min.js",
                      // FastClick
                      "~/bower_components/fastclick/lib/fastclick.js",
                      // AdminLTE App
                      "~/dist/js/adminlte.min.js",
                      // Bootstrap WYSIHML5
                      "~/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
                      // Sparkline
                      "~/bower_components/jquery-sparkline/dist/jquery.sparkline.min.js",
                      // jvectormap
                      "~/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                      "~/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
                      // SlimScroll
                      "~/bower_components/jquery-slimscroll/jquery.slimscroll.min.js",
                      // JQuery Knob Chart
                      "~/bower_components/jquery-knob/dist/jquery.knob.min.js",
                      // Morris Chart
                      "~/bower_components/raphael/raphael.min.js",
                      "~/bower_components/morris.js/morris.min.js",
                      // Date range picker
                      "~/bower_components/moment/min/moment.min.js",
                      "~/bower_components/bootstrap-daterangepicker/daterangepicker.js",
                      // Date picker
                      "~/bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"
                ));
            #endregion

            // JS boundle for demo
            bundles.Add(new ScriptBundle("~/AdminLTE/demo").Include(
                    "~/dist/js/pages/dashboard.js",
                    "~/dist/js/demo.js"
                ));

            #region Style Bundle: ~/AdminLTE/css
            bundles.Add(new StyleBundle("~/AdminLTE/css").Include(
                      // Bootstrap 3.3.7
                      "~/bower_components/bootstrap/dist/css/bootstrap.min.css",
                      // Font-Awesome
                      "~/bower_components/font-awesome/css/font-awesome.min.css",
                      // Ion icons
                      "~/bower_components/Ionicons/css/ionicons.min.css",
                      // Jvctormap
                      "~/bower_components/jvectormap/jquery-jvectormap.css",
                      // AdminLTE theme and skin
                      "~/dist/css/AdminLTE.min.css",
                      "~/dist/css/skins/_all-skins.min.css",
                      // Morris chart
                      "~/bower_components/morris.js/morris.css",
                      // Date picker
                      "~/bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css",
                      // Date range picker
                      "~/bower_components/bootstrap-daterangepicker/daterangepicker.css",
                      // bootstrap wysihtml5 - text editor
                      "~/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css"
                  ));
            #endregion
        }
    }
}