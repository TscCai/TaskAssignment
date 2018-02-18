using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskAssignment.Persistence;

namespace TaskAssignment.Areas.Admin.Controllers
{
    public class MemberController : Controller
    {
        public ActionResult Index() {
            return View();
        }

        // GET: Member
        [ChildActionOnly]
        public ActionResult AllList() {
            var ctx = new TaskAssignmentModel();
            var model = ctx.Members.DefaultIfEmpty();
            return View("_AllList", model);
        }

        [ChildActionOnly]
        public ActionResult AttendanceMemberList() {
            
            
            var ctx = new TaskAssignmentModel();
            var model = ctx.Members.Where(m => m.Enable && m.IsInternal);
            //List<object> data = null;
            //if (model != null && model.Count() > 0) {
            //    data = new List<object>();
            //    foreach(var i in model) {
            //        data.Add(new {Id=i.Id,Enable=i.Enable,Name=i.Name });
            //    }
            //}

            //result.Data = data;
            //return result;
            return View(model);
        }
		
		public JsonResult UnavailableMemberList(DateTime id)
		{
			JsonResult result = new JsonResult();
			result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
			var ctx = new TaskAssignmentModel();

			var tmp = ctx.Attendances.Where(att => att.StartDate <= id && att.FinishDate >= id && att.AttendanceType.IsAbcense).ToArray();
			if (tmp.Count() > 0)
			{
				long[] mIds = new long[tmp.Count()];
				for (int i = 0; i < mIds.Length; i++)
				{
					mIds[i] = tmp[i].MemberId;
				}
				result.Data = mIds;
			}
			return result;
			
		}
    }
}