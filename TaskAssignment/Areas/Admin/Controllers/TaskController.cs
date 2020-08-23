using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskAssignment.Areas.Admin.Models;
using TaskAssignment.Persistence;
using TaskAssignment.Util;

namespace TaskAssignment.Areas.Admin.Controllers
{
    public class TaskController : Controller
    {
        const int Type4Outdoor = 2;

        // GET: /Admin/Task/Show
        [HttpGet]
        public ActionResult Show() {

            return View();
        }

        [HttpPost]
        public ActionResult Show(DateRange model) {
            var ctx = new TaskAssignmentModel();
            var list = ctx.Tasks.Where(t => t.Date >= model.Start && t.Date <= model.Finish).OrderBy(t => t.Date);
            ViewBag.Start = model.Start.ToString("yyyy年MM月dd日");
            ViewBag.Finish = model.Finish.ToString("yyyy年MM月dd日");
            return View(list);
        }

        [HttpGet]
        public ActionResult Add() {

            return View();
        }

        [HttpPost]
        public ActionResult Add(AddTask model) {
            // Only invoke in Add.cshtml
            Task t = model.Task;
            var ctx = new TaskAssignmentModel();
            t.Visible = true;
            ctx.Tasks.Add(t);

            if (model.LeaderId > 0) {
                Assign leader = new Assign();
                leader.MemberId = model.LeaderId;
                leader.IsLeader = true;
                leader.TaskId = t.Id;
                ctx.Assigns.Add(leader);

                Attendance att = new Attendance();
                att.TaskId = t.Id;
                att.MemberId = model.LeaderId;
                att.StartDate = t.Date;
                att.FinishDate = t.Date;
                ctx.Attendances.Add(att);
            }

            foreach (var item in model.MemberId) {
                Assign asg = new Assign();
                asg.MemberId = item;
                asg.IsLeader = false;
                asg.TaskId = t.Id;

                Attendance att = new Attendance();
                att.TaskId = t.Id;
                att.MemberId = item;
                att.TypeId = Type4Outdoor; // The Type of outside work in DB is 2 (reffer to AttendanceType)
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

        [HttpGet]
        public JsonResult Edit(int id) {
            var ctx = new TaskAssignmentModel();
            var task = ctx.Tasks.SingleOrDefault(t => t.Id == id);
            var result = new JsonResult();
            result.ContentEncoding = System.Text.Encoding.UTF8;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;


            var tmp_list = task.Assigns.Where(t => t.TaskId == id && !t.IsLeader).ToList();
            long[] m_id = null;
            if (tmp_list != null && tmp_list.Count > 0) {
                m_id = new long[tmp_list.Count];
                for (int i = 0; i < m_id.Length; i++) {
                    m_id[i] = tmp_list[i].MemberId;
                }
            }

            var asg = task.Assigns.SingleOrDefault(t => t.TaskId == id && t.IsLeader);
            long l_id = 0;
            if (asg != null) {
                l_id = asg.MemberId;
            }
            result.Data = new {
                Id = task.Id,
                SubstationId = task.SubstationId,
                LocationId = task.Substation.LocationId,
                Content = task.Content,
                ConditionId = task.ConditionId,
                Date = task.Date.ToString("yyyy-MM-dd"),
                Visible = task.Visible,
                TypeId = task.TypeId,
                LeaderId = l_id,
                MemberId = m_id
            };
            return result;
        }

        [HttpPost]
        public ActionResult Edit(AddTask model) {
            if (model != null && model.Task != null && model.Task.Id > 0) {
                var ctx = new TaskAssignmentModel();
                var task = ctx.Tasks.SingleOrDefault(t => t.Id == model.Task.Id);
                ctx.Assigns.RemoveRange(task.Assigns);

                task.ConditionId = model.Task.ConditionId;
                task.Content = model.Task.Content;
                task.Date = model.Task.Date;
                task.SubstationId = model.Task.SubstationId;
                task.TypeId = model.Task.TypeId;

                //task.Visible = model.Task.Visible;
                // Notice: Attendance cascaded delete has been implement in DB by trigger


                if (model.LeaderId > 0) {
                    Assign leader = new Assign();
                    leader.MemberId = model.LeaderId;
                    leader.IsLeader = true;
                    leader.TaskId = task.Id;
                    ctx.Assigns.Add(leader);

                    Attendance att = new Attendance();
                    att.TaskId = model.Task.Id;
                    att.MemberId = model.LeaderId;
                    att.TypeId = Type4Outdoor; // The Type of outside work in DB is 2 (reffer to AttendanceType)
                    att.StartDate = task.Date;
                    att.FinishDate = task.Date;

                    ctx.Attendances.Add(att);
                }

                foreach (var item in model.MemberId) {
                    Assign asg = new Assign();
                    asg.MemberId = item;
                    asg.IsLeader = false;
                    asg.TaskId = task.Id;

                    Attendance att = new Attendance();
                    att.TaskId = model.Task.Id;
                    att.MemberId = item;
                    att.TypeId = Type4Outdoor; // The Type of outside work in DB is 2 (reffer to AttendanceType)
                    att.StartDate = task.Date;
                    att.FinishDate = task.Date;
                    ctx.Assigns.Add(asg);
                    ctx.Attendances.Add(att);
                }
                ctx.SaveChanges();
            }
            if (string.IsNullOrEmpty(model.ReturnAction)) {
                model.ReturnAction = "Add";
            }
            return RedirectToAction(model.ReturnAction);
        }

        public ActionResult Delete(int id, string rtnact) {
            var ctx = new TaskAssignmentModel();
            var task = ctx.Tasks.SingleOrDefault(t => t.Id == id);

            var asg = ctx.Assigns.Where(a => a.TaskId == id);
            if (asg != null && asg.Count() > 0) {
                ctx.Assigns.RemoveRange(asg);
            }

            // Notice: Attendance cascaded delete has been implement in DB by trigger

            if (task != null && task.Id != 0) {
                ctx.Tasks.Remove(task);
                ctx.SaveChanges();
                // Won't work currently
                //ViewBag.Success = true;
                //ViewBag.Message = "该工作已删除。";
            }
            if (rtnact == null || rtnact.Length == 0) {
                return RedirectToAction("Add");
            }
            else {
                return RedirectToAction(rtnact);
            }
        }

        public ActionResult Export(DateRange model) {
            var ctx = new TaskAssignmentModel();
            var tasks = ctx.Tasks.Where(t => t.Date >= model.Start && t.Date <= model.Finish).OrderBy(t => t.Date);
            string template = Server.MapPath("~/Content/templates/template-task.xlsx");
            string filename = "";
            if (model.Start == model.Finish) {
                filename = model.Start.ToString("MM.dd") + " 工作安排.xlsx";
            }
            else {
                filename = model.Start.ToString("MM.dd") + "-" + model.Finish.ToString("MM.dd") + " 工作安排.xlsx";
            }
            string exported = Server.MapPath("~/Content/exported/" + filename);
            try {
                ExcelHelper.Export(tasks, template, exported);
            }
            catch (Exception ex) {
                Global.Logger.AppendLog(ex);
            }

            if (System.IO.File.Exists(exported)) {
                FilePathResult result = new FilePathResult(exported, ExcelHelper.XlsxContentType);
                result.FileDownloadName = filename;
                return result;
            }
            else {
                return RedirectToAction("Error");
            }
        }

        [ChildActionOnly]
        public ActionResult Recent() {
            // Fetch the tasks that are already assigned this and the next  week 
            var ctx = new TaskAssignmentModel();
            const int fortnight = 14;
            DateTime thisWeekbegin = DateTime.Today.WeekBegin(DayOfWeek.Monday);
            DateTime nextWeekend = thisWeekbegin.AddDays(fortnight - 1);   // to next Sunday
            var tasks = ctx.Tasks
                .Where(t => t.Date >= thisWeekbegin && t.Date <= nextWeekend)
                .OrderBy(t => t.Assigns.Count);
            return View("_Recent", tasks);
        }

        [ChildActionOnly]
        public ActionResult AllTaskTypeList() {
            var ctx = new TaskAssignmentModel();
            var typeList=ctx.TaskTypes;

            return View();
        }

    }
}