using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskAssignment.Persistence;

namespace TaskAssignment.Controllers
{
    public class MemberController : Controller
    {
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