using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskAssignment.Persistence;

namespace TaskAssignment.Models
{
    public class AddTask
    {
        public Task Task { get; set; }
        public int LeaderId { get; set; }
        public int[] MemberId { get; set; }
    }
}