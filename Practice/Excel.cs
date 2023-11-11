using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.XSSF;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Practice
{
    internal class Excel
    {
        private IWorkbook wb;
        public IWorkbook Workbook { get { return wb; } }

        public Excel(string filename) => ReadWorkBook(filename);
        public Excel() => wb = new XSSFWorkbook();

        public IWorkbook ReadWorkBook(string filename)
        {
            if (!File.Exists(filename)) return null;
            wb = WorkbookFactory.Create(filename);
            return wb;
        }

        public void Save(string destination, bool OverrideFiles)
        {
            if (File.Exists(destination) && !OverrideFiles) throw new Exception("File Override Exception: file already exists. OverrideFiles is false");
            ValidatePath(destination);
            using(FileStream stream = new FileStream(destination, FileMode.Create, FileAccess.Write))
            {
                wb.Write(stream);
            }
        }
        public void Save(string destination) => Save(destination, true);

        private void ValidatePath(string destination)
        {
            string[] parts = destination.Split(new char[] { '/' });
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < parts.Length - 1; i++)
            {
                sb.Append(parts[i]);
                if(i < parts.Length - 2) sb.Append('/');
            }
            string filePath = sb.ToString();
            if(!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
        }
    }
}
