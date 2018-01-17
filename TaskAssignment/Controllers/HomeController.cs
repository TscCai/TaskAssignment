using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskAssignment.Persistence;

namespace TaskAssignment.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index() {
            return View();
        }

        public ActionResult Index2() {
            return View();
        }

        public ActionResult Temp() {
            var ctx = new TaskAssignmentModel();

            // All works in Jan.
            IQueryable<Task> model = ctx.Tasks.Where(t => t.Date.Month == 1);
            
            // Works that haven't been assigned
            //IQueryable<Task> model = ctx.Tasks.Where(t => t.Assigns.Count == 0);
            return View(model);
        }
    }
}