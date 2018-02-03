using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskAssignment.Persistence;

namespace TaskAssignment.Areas.Admin.Controllers
{
    public class AttendanceController : Controller
    {
        // GET: Admin/Attendance
        public ActionResult Index() {
            return View();
        }

        public JsonResult Details(string id) {
            
            int month = DateTime.Parse(id).Month;
            var ctx = new TaskAssignmentModel();
            var model = ctx.Attendances.Where(att => att.StartDate.Month == month);
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.ContentEncoding = System.Text.Encoding.UTF8;

            if (model != null && model.Count() > 0) {
                List<object> data = new List<object>();
                foreach (var item in model) {
                    data.Add(new {
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
            else {
                return null;
            }
        }
    }
}