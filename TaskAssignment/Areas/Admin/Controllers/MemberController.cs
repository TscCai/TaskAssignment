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
        public ActionResult AllList()
        {
            var ctx = new TaskAssignmentModel();
            var model = ctx.Members.DefaultIfEmpty();
            return View("_AllList",model);
        }
    }
}