using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskAssignment.Persistence;

namespace TaskAssignment.Areas.Admin.Models
{
	public class AddAbsence
	{
		public string ReturnAction { get; set; }

		public Attendance Attendance { get; set; }
	}
}