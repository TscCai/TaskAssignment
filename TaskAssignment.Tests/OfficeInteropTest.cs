using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Excel = Microsoft.Office.Interop.Excel;

namespace TaskAssignment.Tests
{
    [TestClass]
    public class OfficeInteropTest
    {
        [TestMethod]
        public void TestMethod1() {
            var app = new Excel.Application();
            app.Visible = false;
            try {
                app.Workbooks.Open("D:\\test.xlsx");
                var worksheets = app.Worksheets;
                if (worksheets.Count >= 1) {
                    // Sheet 1 start as index 1
                    Excel.Worksheet activeSheet = worksheets[1];
                    activeSheet.Activate();
                    
                    // The inserted row will now be Row 2
                    activeSheet.Rows[2].Insert(Excel.XlInsertShiftDirection.xlShiftDown);

                    // Delete cols from the tail to aviod the col index change after delete.
                    activeSheet.Columns[5].Delete(Excel.XlDeleteShiftDirection.xlShiftToLeft);
                    activeSheet.Columns[4].Delete(Excel.XlDeleteShiftDirection.xlShiftToLeft);
                   
                    // Write something in the inserted row
                    activeSheet.Cells[2, 1] = 5;
                    activeSheet.Cells[2, 3] = (int)activeSheet.Cells[2, 1].Value + 1;
                    activeSheet.Cells[2, 2] = "test insert";

                    string filename = "D:\\changed_" + DateTime.Now.ToString("MMddHHmmss") + ".xlsx";
                    app.ActiveWorkbook.SaveAs(filename);
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            finally {
                //app.Workbooks.Close();
                app.Quit();
                
            }
            Console.WriteLine("Done");



        }
    }
}
