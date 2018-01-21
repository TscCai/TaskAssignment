using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskAssignment.Util
{
    public static class DateTimeEx
    {
        /// <summary>
        /// 返回本周开始的那一天，默认为星期天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime WeekBegin(this DateTime date) {
            // DayOfWeek begin from Sunday
            return date.WeekBegin(DayOfWeek.Sunday);
        }

        /// <summary>
        /// 返回本周开始的那一天
        /// </summary>
        /// <param name="date"></param>
        /// <param name="beginDay">以给定的日期为一周的起始</param>
        /// <returns></returns>
        public static DateTime WeekBegin(this DateTime date, DayOfWeek beginDay) {
            var dow = date.DayOfWeek;
            var ds = beginDay - dow;
            if (ds > 0) {
                ds -= 7;
            }
            return date.AddDays(ds);
        }
    }
}