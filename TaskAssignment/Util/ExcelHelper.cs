using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskAssignment.Persistence;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace TaskAssignment.Util
{
    public class ExcelHelper
    {
        /// <summary>
        /// Export to xlsx file using Micorsoft.Office.Interop.Excel
        /// </summary>
        /// <param name="attendanceTime"></param>
        /// <param name="members"></param>
        /// <param name="record"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        public static string Export(DateTime attendanceTime, IEnumerable<Member> members, IEnumerable<Attendance> record, int[] holidays, int[] extraWorkdays,string template) {
            string filename = AppDomain.CurrentDomain.BaseDirectory + "\\考勤记录-" + attendanceTime.ToString("yyyy-MM-dd") + ".xlsx";
            if (File.Exists(filename)) {
                File.Delete(filename);
            }
            var app = new Application();
            app.Visible = false;
            try {
                app.Workbooks.Open(template);
                // Sheet 1 start as index 1
                Worksheet activeSheet = app.Worksheets[1];
                activeSheet.Activate();
                // TODO:
                // Delete the redundant date cols acorrding to attandanceTime
                // Fill in the absent type header, create a dictionary<typeId, col_index>
                // Fill in names, create a dictionary<memberId, row_index>
                // Mark all workdays as "on work" for all members, including the extra workDays but excluding the holidays
                // Read record, fill the table, update the work and absence details in memory
                // Fill in the work & absence days count
            }
            catch (Exception ex) {

            }
            finally {
                app.Workbooks.Close();
                app.Quit();
            }
            return "";
        }
    }
}