using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskAssignment.Persistence;

namespace TaskAssignment.Areas.Admin.Models
{
    public class AddTask
    {
        public Task Task { get; set; }
        public string ReturnAction { get; set; }
        public int LeaderId { get; set; }
        public int[] MemberId { get; set; }
    }
}