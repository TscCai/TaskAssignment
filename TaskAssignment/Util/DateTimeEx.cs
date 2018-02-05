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

        public static DateTime LastDayOfMonth(this DateTime date) {
            // 4,6,9,11
            DateTime result;
            if (date.Month == 2) {
                if (DateTime.IsLeapYear(date.Year)) {
                    result = new DateTime(date.Year, 2, 29);
                }
                else {
                    result = new DateTime(date.Year, 2, 28);
                }
            }
            else if(date.Month == 4 || date.Month == 6 || date.Month == 9 || date.Month == 11) {
                result = new DateTime(date.Year, date.Month, 30);
            }
            else {
                result = new DateTime(date.Year, date.Month, 31);
            }
            return result;
        }
    }
}