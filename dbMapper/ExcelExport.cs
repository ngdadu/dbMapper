using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBMapper
{
    public class ExcelExport: IDisposable
    {
        public static dynamic CreateCOMObject(string progId)
        {
            return Activator.CreateInstance(Type.GetTypeFromProgID(progId));
        }

        public void Dispose()
        {
            if (ExcelApp != null)
            {
                ExcelApp = null;
            }
        }

        public dynamic ExcelApp { get; private set; }
        public dynamic Book { get; private set; }

        public ExcelExport()
        {
            ExcelApp = CreateCOMObject("Excel.Application");
            ExcelApp.Visible = true;
            Book = ExcelApp.Workbooks.Add();
        }

        public void SetRangeValue(dynamic sheet, object CellAddress, object Value, string cellFormat = "")
        {
            if (sheet == null || CellAddress == null || String.IsNullOrEmpty(CellAddress.ToString())) return;
            try
            {
                dynamic range = sheet.Range(CellAddress);
                if (range != null)
                {
                    if (!string.IsNullOrEmpty(cellFormat)) range.NumberFormat = cellFormat;
                    range.Value = Value;
                }
            }
            catch { }
        }
        public void SetCellValue(dynamic sheet, int row, int column, object Value, string cellFormat = "")
        {
            if (sheet == null || row < 1 || column < 1) return;
            try
            {
                if (!string.IsNullOrEmpty(cellFormat)) sheet.Cells[row, column].NumberFormat = cellFormat;
                sheet.Cells[row, column].Value = Value;
            }
            catch { }
        }
    }
}
