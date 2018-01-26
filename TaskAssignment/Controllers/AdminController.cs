﻿using System;
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
        const int Type4Outdoor = 2;

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

        public ActionResult DeleteTask(int id) {
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



        [HttpGet]
        public JsonResult EditTask(int id) {
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
            if(asg != null) {
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
        public ActionResult EditTask(AddTask model) {
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
            return RedirectToAction("AddTask");
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