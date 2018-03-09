using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Web.Optimization;
using TaskAssignment.Util;

namespace TaskAssignment
{
    public class Global : HttpApplication
    {
        public static SimpleLogger Logger;
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            string logFile = Server.MapPath("~/Content/logs/log"+DateTime.Now.ToString("yyyy-MM-dd")+".txt");
            Logger = new SimpleLogger(logFile);
            Logger.Level = SimpleLogger.LogLevel.Debug;
        }

        void Application_End(object sender, EventArgs e) {
            if(Logger != null) {
                Logger.Close();
            }
        }
    }
}