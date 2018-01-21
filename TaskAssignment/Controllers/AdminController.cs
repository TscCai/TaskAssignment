using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskAssignment.Persistence;
using TaskAssignment.Util;

namespace TaskAssignment.Controllers
{
    public class AdminController : Controller
    {


        // GET: Admin
        public ActionResult Index() {
            return View();
        }

        [HttpGet]
        public ActionResult AddTask() {
            // Fetch the tasks that are already assigned this and the next  week 
            var ctx = new TaskAssignmentModel();
            const int fortnight = 14;
            DateTime thisWeekbegin = DateTime.Today.WeekBegin(DayOfWeek.Monday);
            DateTime nextWeekend = thisWeekbegin.AddDays(fortnight-1);   // to next Sunday
            var tasks = ctx.Tasks
                .Where(t => t.Date >= thisWeekbegin && t.Date <= nextWeekend)
                .OrderBy(t => t.Date);
            return View(tasks);
        }
    }
}