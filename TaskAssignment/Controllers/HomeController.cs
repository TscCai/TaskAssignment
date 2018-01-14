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
        public ActionResult Index()
        {
            var ctx = new TaskAssignmentModel();
            IQueryable<Task> model = ctx.Tasks.Where(t=>t.Date.Month == 1);

            return View(model);
        }
    }
}