using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskAssignment.Persistence;

namespace TaskAssignment.Areas.Admin.Controllers
{
    public class SubstationController : Controller
    {
        [ChildActionOnly]
        public ActionResult List() {
            var ctx = new TaskAssignmentModel();
            var list = ctx.Substations.DefaultIfEmpty();
            return View("_List", list);
        }

        public ActionResult All() {
            var ctx = new TaskAssignmentModel();
            var list = ctx.Substations.DefaultIfEmpty();
            return View(list);
        }
    }
}