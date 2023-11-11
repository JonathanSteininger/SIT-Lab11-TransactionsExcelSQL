using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab11TransactionsExcelSQL
{
    static class ExcelFactory
    {
        static public IWorkbook GetFile(string filePath)
        {
            if(!File.Exists(filePath)) throw new FileNotFoundException();
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                IWorkbook book = WorkbookFactory.Create(stream);
                stream.Close();
                return book;
            }
        }
        static public void SaveFile(string filePath, IWorkbook book, bool overwriteFiles)
        {
            if (File.Exists(filePath) && !overwriteFiles) throw new Exception("File Already exists with the same name");
            CheckPath(filePath);
            using(FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                book.Write(stream);
                stream.Close();
            }
        }

        static private void CheckPath(string filePath)
        {
            string[] parts = filePath.Split('/');
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < parts.Length - 1; i++)
            {
                sb.Append(parts[i]);
                sb.Append('/');
            }
            string Path = sb.ToString();

            if (!Directory.Exists(Path)) Directory.CreateDirectory(Path);
        }
    }
}
