using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskAssignment.Persistence;
using TaskAssignment.Models;
using TaskAssignment.Util;

namespace TaskAssignment.Controllers
{
    public class AdminController : Controller
    {


        // GET: Admin
        public ActionResult Index() {
            return View();
        }

        #region Task

        [HttpGet]
        public ActionResult AddTask() {

            return View();
        }

        [HttpPost]
        public ActionResult AddTask(AddTask model) {
            Task t = model.Task;
            var ctx = new TaskAssignmentModel();
            ctx.Tasks.Add(t);

            if (model.LeaderId > 0) {
                Assign leader = new Assign();
                leader.MemberId = model.LeaderId;
                leader.IsLeader = true;
                leader.TaskId = t.Id;
                ctx.Assigns.Add(leader);
            }

            foreach (var item in model.MemberId) {
                Assign asg = new Assign();
                asg.MemberId = item;
                asg.IsLeader = false;
                asg.TaskId = t.Id;

                Attendance att = new Attendance();
                att.MemberId = item;
                att.TypeId = 2; // The Type of outside work in DB is 2 (reffer to AttendanceType)
                att.StartDate = t.Date;
                att.FinishDate = t.Date;
                ctx.Assigns.Add(asg);
                ctx.Attendances.Add(att);
            }
            ctx.SaveChanges();

            ViewBag.Success = true;
            ViewBag.Message = "该工作已添加。";
            return View();
        }

        public ActionResult DeleteTask(int id) {
            var ctx = new TaskAssignmentModel();
            var task = ctx.Tasks.SingleOrDefault(t => t.Id == id);

            var asg = ctx.Assigns.Where(a=>a.TaskId == id);
            if(asg !=null && asg.Count() > 0) {
                ctx.Assigns.RemoveRange(asg);
            }
            if (task != null && task.Id != 0) {
                ctx.Tasks.Remove(task);
                ctx.SaveChanges();
                // Won't work currently
                //ViewBag.Success = true;
                //ViewBag.Message = "该工作已删除。";
            }

            return RedirectToAction("AddTask");
        }

        [ChildActionOnly]
        public ActionResult RecentTasks() {
            // Fetch the tasks that are already assigned this and the next  week 
            var ctx = new TaskAssignmentModel();
            const int fortnight = 14;
            DateTime thisWeekbegin = DateTime.Today.WeekBegin(DayOfWeek.Monday);
            DateTime nextWeekend = thisWeekbegin.AddDays(fortnight - 1);   // to next Sunday
            var tasks = ctx.Tasks
                .Where(t => t.Date >= thisWeekbegin && t.Date <= nextWeekend)
                .OrderBy(t => t.Assigns.Count);
            return View("_RecentTasks", tasks);
        }

        #endregion


        [ChildActionOnly]
        public ActionResult SubstationList() {
            var ctx = new TaskAssignmentModel();
            var list = ctx.Substations.DefaultIfEmpty();
            return View("_SubstationList", list);
        }

        public ActionResult AllSubstations() {
            var ctx = new TaskAssignmentModel();
            var list = ctx.Substations.DefaultIfEmpty();
            return View(list);
        }
    }
}