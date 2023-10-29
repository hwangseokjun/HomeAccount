using HomeAccount.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace HomeAccount.Models
{
    public class FinanceCollection
    {
        public ObservableCollection<Finance> Finances { get; set; }

        public FinanceCollection()
        {
            Finances = new ObservableCollection<Finance>();
        }

        public void ExportToExcel()
        {
            Excel.Application excel = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                // Excel 첫번째 워크시트 가져오기
                excel = new Excel.Application();
                workbook = excel.Workbooks.Add();
                worksheet = (Excel.Worksheet)workbook.Worksheets[1];

                // 데이타 넣기
                int count = Finances.Count;

                worksheet.Cells[1, 1] = "날짜";
                worksheet.Cells[1, 2] = "카테고리";
                worksheet.Cells[1, 3] = "지출 혹은 수입처";
                worksheet.Cells[1, 4] = "금액";
                worksheet.Cells[1, 5] = "지출 혹은 수입방법";
                worksheet.Cells[1, 6] = "비고";

                for (int i = 0; i < count; i++)
                {
                    int row = i + 2;

                    worksheet.Cells[row, 1] = Finances[i].Date;
                    worksheet.Cells[row, 2] = Finances[i].Category;
                    worksheet.Cells[row, 3] = Finances[i].Method;
                    worksheet.Cells[row, 4] = Finances[i].Source;
                    worksheet.Cells[row, 5] = Finances[i].Amount;
                    worksheet.Cells[row, 6] = Finances[i].Note;

                    Excel.Range amountCell = worksheet.Cells[row, 5];
                    string amountCellValue = amountCell.Value2.ToString();

                    if (int.TryParse(amountCellValue, out int amount))
                    {
                        amountCell.Font.Color = amount < 0 ? ColorTranslator.ToOle(Color.Red) : ColorTranslator.ToOle(Color.Green);
                    }
                }

                int rowEnd = count + 2;

                // 특정 범위 설정
                dynamic startCell = worksheet.Cells[1, 1];
                dynamic endCell = worksheet.Cells[rowEnd, 6];
                Excel.Range totalRange = worksheet.Range[startCell, endCell];

                // 테두리 설정
                Excel.Borders borders = totalRange.Borders;
                borders.LineStyle = Excel.XlLineStyle.xlContinuous; // 연속된 실선 테두리
                borders.Weight = Excel.XlBorderWeight.xlMedium; // 테두리 굵기

                // 배경색 설정
                dynamic startHeaderCell = worksheet.Cells[1, 1];
                dynamic endHeaderCell = worksheet.Cells[1, 6];
                Excel.Range headerRange = worksheet.Range[startHeaderCell, endHeaderCell];
                headerRange.Interior.Color = ColorTranslator.ToOle(Color.Yellow); // 배경색을 노란색으로 설정

                // 엑셀파일 저장
                workbook.SaveAs(@"C:\Users\unbea\Desktop\test.xlsx");
                workbook.Close(true);
                excel.Quit();

                // 엑셀파일 실행
                Process.Start(@"C:\Users\unbea\Desktop\test.xlsx");
            }
            finally
            {
                ReleaseExcelObject(worksheet);
                ReleaseExcelObject(workbook);
                ReleaseExcelObject(excel);
            }
        }

        private void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
