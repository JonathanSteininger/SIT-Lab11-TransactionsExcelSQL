using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;

namespace Lab11TransactionsExcelSQL
{
    internal class Excel
    {
        private IWorkbook _workbook;
        public IWorkbook Workbook { get { return _workbook; } }

        public Excel()
        {
            _workbook = new XSSFWorkbook();
        }
        public Excel(string FilePath)
        {
            _workbook = ExcelFactory.GetFile(FilePath);
        }
        public Excel(IWorkbook workbook)
        {
            _workbook = workbook;
        }

        
    }
}
