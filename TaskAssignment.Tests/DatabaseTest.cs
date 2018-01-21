using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskAssignment.Persistence;

namespace TaskAssignment.Tests
{
    [TestClass]
    public class DatabaseTest
    {
        [TestMethod]
        public void TaskAdd_Test() {
            var ctx = new TaskAssignmentModel();
            Task t = new Task();
            t.Content = "Test task2";
            t.Date = DateTime.Today;
            t.ConditionId = 2;
            t.TypeId = 1;
            ctx.Tasks.Add(t);
            ctx.SaveChanges();
        }
    }
}
