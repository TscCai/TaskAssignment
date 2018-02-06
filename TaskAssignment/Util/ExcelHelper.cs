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
        /// Export attendance records to xlsx file using Micorsoft.Office.Interop.Excel
        /// </summary>
        /// <param name="attendanceTime"></param>
        /// <param name="members"></param>
        /// <param name="record"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        public static string Export(DateTime attendanceTime, IEnumerable<Member> members, IEnumerable<Attendance> record, IEnumerable<AttendanceType> attType, int[] holidays, int[] extraWorkdays, string template) {
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
                // Cell Position Definition, int[] {row, col}
                int[] pos_department = new int[] { 2, 2 };
                int[] pos_month = new int[] { 2, 22 };
                int[] pos_timestamp = new int[] { 2, 36 };
                int[] pos_names = new int[] { 5, 1 };
                int namesReserveRow = 2;
                int[] pos_date = new int[] { 3, 2 };
                const int maxDayOfMonth = 31;
                const int date_offset = 1;
                int[] pos_total_att = new int[] { 3, 33 };
                int[] pos_absence = new int[] { 4, 34 };

                int ci_late = 34;
                int ci_personal = 35;
                int ci_sick = 36;
                int ci_comp = 37;
                int ci_annual = 38;
                int ci_wed = 39;
                int ci_absent = 40;
                int ci_maternity = 41;
                int ci_nurse = 42;
                int ci_injur = 43;
                int ci_train = 44;
                int ci_bussiness = 45;
                int ci_other = 46;
                // ci: col index, ri: row index
                int ci, ri;

                activeSheet.Cells[pos_month[0], pos_month[1]] = attendanceTime.Month + "月";
                activeSheet.Cells[pos_timestamp[0], pos_timestamp[1]] = "报表上报时间：" + DateTime.Now.ToString("yyyy年MM月dd日");

                // Delete the redundant date cols acorrding to attandanceTime
                int lastDay = DateTime.DaysInMonth(attendanceTime.Year, attendanceTime.Month);
                for (int i = maxDayOfMonth + date_offset; i > lastDay; i--) {
                    activeSheet.Columns[i].Delete(XlDeleteShiftDirection.xlShiftToLeft);
                }
                // Fill in the absent type header, create a dictionary<typeId, col_index>
                Dictionary<long, int> attTypeDict = new Dictionary<long, int>();
                ci = pos_absence[1];
                foreach (var i in attType) {
                    activeSheet.Cells[pos_absence[0], ci] = i.TypeName;
                    attTypeDict.Add(i.Id, ci);
                    ci++;
                }

                // Fill in names, create a dictionary<memberId, row_index>
                Dictionary<long, int> memberDict = new Dictionary<long, int>();
                ci = pos_names[1];
                ri = pos_names[0];
                foreach (var i in members) {
                    activeSheet.Cells[ri, ci] = i.Name;
                    memberDict.Add(i.Id, ri);
                    ri++;
                    activeSheet.Rows[ri].Insert(XlInsertShiftDirection.xlShiftDown);
                }
                activeSheet.Rows[ri].Delete(XlDeleteShiftDirection.xlShiftUp);
                int lastNameRow = ri--;

                // Mark all workdays as "on work" for all members, including the extra workDays but excluding the holidays
                string defaultSymbol = "√";
                for (int day = 1; day <= lastDay; day++) {
                    DateTime flag = new DateTime(attendanceTime.Year, attendanceTime.Month, day);
                    ci = pos_date[1] + day - 1;
                    if (IsWorkday(flag, holidays, extraWorkdays)) {
                        for (ri = pos_names[0]; ri <= lastNameRow; ri++) {
                            activeSheet.Cells[ri, ci] = defaultSymbol;
                        }
                    }
                }

                // <MemberId-TypeId, count> like "3-1",5
                Dictionary<string, int> attCnt = new Dictionary<string, int>();
                // Read record, fill the table, update the work and absence details in memory
                foreach (var item in record) {
                    long memberId = item.MemberId;
                    long typeId = item.TypeId;
                    string symbol = item.AttendanceType.Symbol;
                    string alias = item.AttendanceType.Alias;
                    DateTime start = item.StartDate;
                    DateTime finish = item.FinishDate;
                    int cnt = 0;
                    for (DateTime d = start; d <= finish; d = d.AddDays(1)) {
                        ri = memberDict[memberId];
                        ci = pos_date[1] + d.Day - 1;
                        activeSheet.Cells[ri, ci] = symbol;
                        cnt++;
                    }
                    string key = memberId + "-" + typeId;
                    if (!attCnt.ContainsKey(key)) {
                        attCnt.Add(key, cnt);
                    }
                    else {
                        attCnt[key] += cnt;
                    }


                }

                // Fill in the work & absence days count
                foreach(var i in members){
                    int workCnt = attCnt[i.Id+"-"+"1"] + attCnt[i.Id+"-"+"2"];
                    ri = memberDict[i.Id];
                    ci = pos_total_att[1];
                    activeSheet.Cells[ri,ci]=workCnt;
                    
                }
                
                foreach(var i in attCnt){
                    long memberId = Convert.ToInt64(i.Key.Split('-')[0]);
                    long typeId = Convert.ToInt64(i.Key.Split('-')[1]);
                    int cnt = i.Value;
                    
                    ri = memberDict[memberId];
                    ci = attTypeDict[typeId];
                    activeSheet.Cells[ri,ci] = cnt;
                }
                
                // Save changes
                activeSheet.SaveAs(filename);
            }
            catch (Exception ex) {

            }
            finally {
                app.Workbooks.Close();
                app.Quit();
            }
            return "";
        }

        /// <summary>
        /// Export task assignment records to xlsx file using Microsoft.Office.Interop.Excel
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        public static string Export(IEnumerable<Task> tasks, string template) {
            string filename = AppDomain.CurrentDomain.BaseDirectory + "\\工作安排-" + tasks.First().Date.ToString("yyyy-MM-dd") + ".xlsx";
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
                // col index definition
                const int ci_comment = 1;
                const int ci_year = 2;
                const int ci_month = 3;
                const int ci_day = 4;
                const int ci_location = 5;
                const int ci_substation = 6;
                const int ci_voltage = 7;
                const int ci_type = 8;
                const int ci_content = 9;
                const int ci_leader = 10;
                const int ci_member_s = 11;
                const int ci_member_e = 14;

                const int first_row = 2;
                int ri = first_row, ci;
                foreach (var i in tasks) {
                    int year = i.Date.Year;
                    int month = i.Date.Month;
                    int day = i.Date.Day;
                    string location = i.Substation.Location.LocationName;
                    string substation = i.Substation.SubstationName;
                    string voltage = i.Substation.Voltage + "kV";
                    string type = i.TaskType.TypeName;
                    string content = i.Content;
                    string leader = i.Assigns.SingleOrDefault(l => l.IsLeader).Member.Name;

                    int memberCnt = i.Assigns.Where(m => !m.IsLeader).Count();
                    string[] members = new string[memberCnt];
                    int ptr = 0;
                    foreach (var j in i.Assigns.Where(m => !m.IsLeader)) {
                        members[ptr] = j.Member.Name;
                        ptr++;
                    }

                    activeSheet.Cells[ri, ci_year] = year;
                    activeSheet.Cells[ri, ci_month] = month;
                    activeSheet.Cells[ri, ci_day] = day;
                    activeSheet.Cells[ri, ci_location] = location;
                    activeSheet.Cells[ri, ci_substation] = substation;
                    activeSheet.Cells[ri, ci_voltage] = voltage;
                    activeSheet.Cells[ri, ci_type] = type;
                    activeSheet.Cells[ri, ci_content] = content;
                    activeSheet.Cells[ri, ci_leader] = leader;
                    ci = ci_member_s;
                    for (ptr = 0; ptr < members.Length; ptr++) {
                        if (ci > ci_member_e) {
                            ci = ci_member_s;
                            ri++;
                            activeSheet.Rows[ri].Insert(XlInsertShiftDirection.xlShiftDown);
                        }
                        activeSheet.Cells[ri, ci] = members[ptr];
                    }
                    ri++;
                    activeSheet.Rows[ri].Insert(XlInsertShiftDirection.xlShiftDown);
                }
                // Save changes
                activeSheet.SaveAs(filename);
            }
            catch (Exception ex) {

            }
            finally {
                app.Workbooks.Close();
                app.Quit();
            }
            return "";
        }

        public static object Import(string filename) {
            // Not implement yet
            return null;
        }

        static bool IsWorkday(DateTime date, int[] holidays, int[] extraWorkdays) {

            bool result = (date.DayOfWeek != DayOfWeek.Sunday && date.DayOfWeek != DayOfWeek.Saturday && !holidays.Contains(date.Day)) ||
                     (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday && extraWorkdays.Contains(date.Day));
            return result;
        }
    }
}