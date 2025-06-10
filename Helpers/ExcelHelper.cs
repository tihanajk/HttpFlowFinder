using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using Excel = Microsoft.Office.Interop.Excel;

namespace HttpFlowFinder.Helpers
{
    public class ExcelHelper : PluginControlBase
    {

        public void HandleExcel(string fileName, List<FlowInfo> flowsData)
        {
            var saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Excel files|*.xlsx";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = fileName;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                WorkAsync(new WorkAsyncInfo()
                {
                    Message = "Populating excel",
                    AsyncArgument = null,
                    Work = (worker, args) =>
                    {
                        PopulateExcel(saveFileDialog.FileName, flowsData);
                    },
                    PostWorkCallBack = (args) =>
                    {
                        if (args.Error != null)
                        {
                            MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        MessageBox.Show("Finished :)");

                        //DialogResult confirmResult = MessageBox.Show("Do you want to open the file?", "Excel file", MessageBoxButtons.YesNo);

                        //if (confirmResult == DialogResult.Yes)
                        //{
                        //    System.Diagnostics.Process.Start(saveFileDialog.FileName);
                        //}
                    }
                });

            }
        }

        private void PopulateExcel(string fileName, List<FlowInfo> flowsData)
        {
            var excelApp = new Excel.Application();
            excelApp.Visible = false;
            Workbook workbook = excelApp.Workbooks.Add();

            WriteInfo(workbook, flowsData);

            workbook.SaveAs(fileName);
            workbook.Close();
            excelApp.Quit();
        }

        private void AddHeaderRow(Worksheet sheet)
        {
            string[] headers = { "NAME", "TRIGGER", "AUTH TYPE", "USERS", "SCHEMA", "STATE", "LINK" };

            Excel.Range header = sheet.Range["A1", "G1"];

            for (int i = 0; i < headers.Length; i++)
            {
                sheet.Cells[1, i + 1] = headers[i];
            }

            header.Font.Bold = true;

            var darkBlue = ColorTranslator.ToOle(Color.FromArgb(17, 94, 163));
            header.Interior.Color = darkBlue;
            header.Font.Color = ColorTranslator.ToOle(Color.White);
        }
        private void WriteInfo(Workbook workbook, List<FlowInfo> flowsData)
        {
            Worksheet sheet = workbook.Sheets.Add();
            sheet.Name = "Flows";

            AddHeaderRow(sheet);

            var lightBlue = ColorTranslator.ToOle(Color.FromArgb(154, 191, 220));

            var row = 1;
            foreach (var flow in flowsData)
            {
                ++row;
                var column = 0;

                ++column;
                sheet.Cells[row, column] = flow.name;

                ++column;
                sheet.Cells[row, column] = flow.trigger;

                ++column;
                sheet.Cells[row, column] = flow.authType;

                ++column;
                sheet.Cells[row, column] = flow.users;

                ++column;
                sheet.Cells[row, column] = flow.schema;

                ++column;
                sheet.Cells[row, column] = flow.state_display;

                ++column;
                sheet.Cells[row, column] = flow.link;

                if (row % 2 == 0)
                {
                    Excel.Range rowRange = sheet.Range[$"A{row}", $"G{row}"];

                    rowRange.Interior.Color = lightBlue;
                }

            }

            AutosizeColumns(sheet, 8);
        }

        private void AutosizeColumns(Worksheet sheet, int columnsCount)
        {
            for (int i = 1; i <= columnsCount; i++)
            {
                sheet.Columns[i].AutoFit();
            }
        }
    }

}