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
         
            #region JS bundle: ~/dependency/js
            bundles.Add(new ScriptBundle("~/dependency/js").Include(
                    // jQuery 3
                    "~/Content/dependency/js/jquery.js",
                    // Bootstrap 3.3.7
                    "~/Content/dependency/js/bootstrap.min.js"
            ));
            #endregion

            #region JS bundle: ~/plugins/js
            bundles.Add(new ScriptBundle("~/plugins/js").Include(
                // JQuery UI
                "~/Content/plugins/jquery-ui/jquery-ui.min.js",
                // JQuery Knob Chart
                "~/Content/plugins/jquery-knob/jquery.knob.min.js",
                // FastClick
                "~/Content/plugins/fastclick/fastclick.js",
                // Sparkline
                "~/Content/plugins/jquery-sparkline/jquery.sparkline.min.js",
                // SlimScroll
                "~/Content/plugins/jquery-slimscroll/jquery.slimscroll.min.js",

                // Morris Chart
                "~/Content/plugins/raphael/raphael.min.js",
                "~/Content/plugins/morris.js/morris.min.js",
                // Date range picker
                "~/Content/plugins/moment/moment.min.js",
                "~/Content/plugins/bootstrap-daterangepicker/daterangepicker.js",
                
                // Date picker
                "~/Content/plugins/bootstrap-datepicker/bootstrap-datepicker.min.js",
                "~/Content/plugins/bootstrap-datepicker/locales/bootstrap-datepicker.zh-CN.min.js",

                // Bootstrap WYSIHML5
                "~/Content/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
                // jvectormap
                "~/Content/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                "~/Content/plugins/jvectormap/jquery-jvectormap-world-mill-en.js"

            ));
            #endregion

            #region JS bundle: ~/AdminLTE/js
            bundles.Add(new ScriptBundle("~/AdminLTE/js").Include(
                // AdminLTE App
                "~/Content/AdminLTE/js/adminlte.min.js"
            ));
            #endregion

            #region JS boundle: ~/AdminLTE/demo
            bundles.Add(new ScriptBundle("~/AdminLTE/demo").Include(
                "~/Content/AdminLTE/js/pages/dashboard.js",
                "~/Content/AdminLTE/js/demo.js"
            ));
            #endregion

            #region JS bundle: ~/plugins/js/table
            bundles.Add(new ScriptBundle("~/plugins/js/table").Include(
                // JQuery UI
                "~/Content/plugins/datatables.net/jquery.dataTables.min.js",
                "~/Content/plugins/datatables.net-bs/dataTables.bootstrap.min.js"
            ));
            #endregion

            #region Style bundle: ~/dependency/css
            bundles.Add(new StyleBundle("~/dependency/css").Include(
                // Bootstrap 3.3.7
                "~/Content/dependency/css/bootstrap.min.css"
            ));
            #endregion

            #region Style bundle: ~/Content/dependency/fonts
            bundles.Add(new StyleBundle("~/Content/dependency/fonts").Include(
                // Font-Awesome
                "~/Content/dependency/css/font-awesome.min.css",
                // Ion icons
                "~/Content/dependency/css/ionicons.min.css"
            ));
            #endregion

            #region Style bundle: ~/plugins/css/table
            bundles.Add(new StyleBundle("~/plugins/css/table").Include(
                "~/Content/plugins/datatables.net-bs/dataTables.bootstrap.min.css"
            ));
            #endregion

            #region Style bundle: ~/plugins/css
            bundles.Add(new StyleBundle("~/plugins/css").Include(

                // Jvctormap
                "~/Content/plugins/jvectormap/jquery-jvectormap.css",
                // Morris chart
                "~/Content/plugins/morris.js/morris.css",
                // Date picker
                "~/Content/plugins/bootstrap-datepicker/bootstrap-datepicker.min.css",
                // Date range picker
                "~/Content/plugins/bootstrap-daterangepicker/daterangepicker.css",

                // bootstrap wysihtml5 - text editor
                "~/Content/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css"
            ));
            #endregion

            #region Style bundle: ~/AdminLTE/css
            bundles.Add(new StyleBundle("~/AdminLTE/css").Include(
                // AdminLTE theme and skin
                "~/Content/AdminLTE/css/AdminLTE.min.css",
                "~/Content/AdminLTE/css/skins/_all-skins.min.css"
            ));
            #endregion
        }
    }
}