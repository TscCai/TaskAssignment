using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskAssignment.Util;

namespace TaskAssignment.Tests
{
	[TestClass]
	public class UtilTest
	{
		[TestMethod]
		public void DateWeekTest()
		{
            DateTime someday = new DateTime(2018, 1, 20);

            DateTime thisWeekstart = someday.WeekBegin(DayOfWeek.Sunday);
            var nextWeekend = thisWeekstart.AddDays(13);
            
            
		}
	}
}
