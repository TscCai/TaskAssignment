using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskAssignment.Persistence;
using TaskAssignment.Areas.Admin.Models;
using TaskAssignment.Util;

namespace TaskAssignment.Areas.Admin.Controllers
{
	public class AttendanceController : Controller
	{
		// GET: Admin/Attendance
		public ActionResult Index()
		{
			return View();
		}

		public JsonResult AbsentList(DateTime id)
		{
			return null;
		}


		public ActionResult Absent(DateTime? id)
		{
			// id refers to the objective month
			if (!id.HasValue)
			{
				return View();
			}
			else
			{
				int month = id.Value.Month;
				var ctx = new TaskAssignmentModel();
				var model = ctx.Attendances.Where(att => att.AttendanceType.IsAbsent && att.StartDate.Month == month);
				ViewBag.Date = id.Value.ToString("yyyy-MM");
				return View(model);
			}
		}

        [HttpPost]
        public ActionResult Export(DateTime id) {
            string template = Server.MapPath("~/Content/templates/template-attendance.xlsx");
            string exported = Server.MapPath("~/Content/exported/"+ "考勤记录-" + id.ToString("yyyy-MM-dd") + ".xlsx");
            var ctx = new TaskAssignmentModel();
            var members = ctx.Members.Where(m => m.IsInternal && m.Enable);
            var record = ctx.Attendances.Where(att => att.StartDate.Month == id.Month && att.StartDate.Year == id.Year);
            var absType = ctx.AttendanceTypes.Where(t=>t.IsAbsent);
            var t_holidays = ctx.Holidays.SingleOrDefault(h => h.Year == id.Year);
            var thd = t_holidays.Holidays.Split(';').Where(h=>h.StartsWith(id.ToString("MM")+"-")).ToArray();
            var tex = t_holidays.ExtraWorkdays.Split(';').Where(e => e.StartsWith(id.ToString("MM") + "-")).ToArray();

            int[] holidays =new int[thd.Count()];
            int[] extraWorkdays = new int[tex.Count()];
            for(int i = 0; i < holidays.Length; i++) {
                holidays[i] = Convert.ToInt32(thd[i].Substring(3));
            }
            for(int i=0; i < extraWorkdays.Length; i++) {
                extraWorkdays[i] = Convert.ToInt32(tex[i].Substring(3));
            }

            ExcelHelper.Export(id,members,record,absType,holidays,extraWorkdays, template,exported);
            //GC.Collect();

            FilePathResult r;
            if (System.IO.File.Exists(exported)) {
                r = new FilePathResult(exported, ExcelHelper.XlsxContentType);
                r.FileDownloadName = "考勤记录-" + id.ToString("yyyy-MM-dd") + ".xlsx";
                return r;
            }
            else {
                return RedirectToAction("Error");
            }
        }

        [ChildActionOnly]
		public ActionResult TypeList(string id)
		{
			var ctx = new TaskAssignmentModel();
			IQueryable<AttendanceType> model = null;
			if (id != "absent")
			{
				model = ctx.AttendanceTypes.DefaultIfEmpty();
			}
			else
			{
				model = ctx.AttendanceTypes.Where(type => type.IsAbsent);
			}

			return View("_TypeList", model);
		}

		[HttpGet]
		public JsonResult Edit(long id)
		{
			var ctx = new TaskAssignmentModel();
			var absence = ctx.Attendances.SingleOrDefault(att => att.Id == id);
			JsonResult result = new JsonResult();
			result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
			result.ContentEncoding = System.Text.Encoding.UTF8;
			// start, finish date, name, typename, comment
			result.Data = new
			{
				Id = absence.Id,
				StartDate = absence.StartDate.ToString("yyyy-MM-dd"),
				FinishDate = absence.FinishDate.ToString("yyyy-MM-dd"),
				MemberId = absence.MemberId,
				TypeId = absence.TypeId,
				Comments = absence.Comments
			};
			return result;
		}

		[HttpPost]
		public ActionResult Edit(AddAbsence model)
		{
			var ctx = new TaskAssignmentModel();
			var item = ctx.Attendances.SingleOrDefault(abs => abs.Id == model.Attendance.Id);
			item.MemberId = model.Attendance.MemberId;
			item.StartDate = model.Attendance.StartDate;
			item.FinishDate = model.Attendance.FinishDate;
			item.TypeId = model.Attendance.TypeId;
			item.Comments = model.Attendance.Comments;

			ctx.SaveChanges();

			string[] url = model.ReturnAction.Split('?');
			return RedirectToAction(url[0], new { id = url[1].Split('=')[1] });

		}

		public ActionResult Add(AddAbsence model)
		{
			var ctx = new TaskAssignmentModel();
			ctx.Attendances.Add(model.Attendance);
			ctx.SaveChanges();
			string[] url;
			if (!String.IsNullOrEmpty(model.ReturnAction))
			{
				url = model.ReturnAction.Split('?');
				string act = url[0];
				string query = "";
				if (url.Length > 1)
				{
					query = url[1].Split('=')[1];
				}
				return RedirectToAction(act, new { id = query });
			}
			else
			{
				return RedirectToAction("Absent");
			}
			
		}

		public ActionResult Delete(long id)
		{
			var ctx = new TaskAssignmentModel();
			var item = ctx.Attendances.SingleOrDefault(att => att.Id == id);
			ctx.Attendances.Remove(item);
			ctx.SaveChanges();
			return RedirectToAction("Absent");
		}

		public JsonResult Details(string id)
		{

			int month = DateTime.Parse(id).Month;
			var ctx = new TaskAssignmentModel();
			var model = ctx.Attendances.Where(att => att.StartDate.Month == month);
			JsonResult result = new JsonResult();
			result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
			result.ContentEncoding = System.Text.Encoding.UTF8;

			if (model != null && model.Count() > 0)
			{
				List<object> data = new List<object>();
				foreach (var item in model)
				{
					data.Add(new
					{
						MemberId = item.MemberId,
						TypeId = item.TypeId,
						Alias = item.AttendanceType.Alias,
						Symbol = item.AttendanceType.Symbol,
						Start = item.StartDate.ToString("yyyy-MM-dd"),
						Finish = item.FinishDate.ToString("yyyy-MM-dd")
					});
				}
				result.Data = data;
				return result;
			}
			else
			{
				return null;
			}
		}
	}
}