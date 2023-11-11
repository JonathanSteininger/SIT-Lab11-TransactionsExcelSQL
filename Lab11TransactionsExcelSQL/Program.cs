using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI;
using NPOI.SS.UserModel;

namespace Lab11TransactionsExcelSQL
{
    internal class Program
    {
        private const string CONNECTION_STRING = "Server=localhost;Database=it607labs;Uid=student;Pwd=secret;";
        private const string EXCEL_FILEPATH = "./customers.xlsx";

        static private DataBase _db;
        static private Excel _ex;
        static void Main(string[] args)
        {
            _db = new DataBase(CONNECTION_STRING);
            _ex = new Excel(EXCEL_FILEPATH);
            List<Customer> customers = GetList(_ex);
            List<Transaction> transactions = GetTransactions();
            foreach (Transaction transaction in transactions)
            {
                if (customers.Exists((cust) => cust.CustomerId == transaction.CustomerID))
                {
                    customers.Find((cust) => cust.CustomerId == transaction.CustomerID).Balance += transaction.Amount;
                }
                else customers.Add(new Customer(transaction.CustomerID, "", "", transaction.Amount));
                
            }

            Excel resultExcel = new Excel();
            ISheet sheet = resultExcel.Workbook.CreateSheet();
            FillRowString(0, ref sheet, "CustomerID", "Surname", "Firstname", "Balance");
            foreach(Customer customer in customers)
            {
                AddCustomerRow(customer, ref sheet);
            }
            ExcelFactory.SaveFile("./resultfile.xlsx", resultExcel.Workbook,true);
        }

        private static void AddCustomerRow(Customer customer, ref ISheet sheet)
        {
            IRow row = sheet.CreateRow(sheet.LastRowNum + 1);
            row.CreateCell(0).SetCellValue(customer.CustomerId);
            row.CreateCell(1).SetCellValue(customer.LastName);
            row.CreateCell(2).SetCellValue(customer.FirstName);
            row.CreateCell(3).SetCellValue(customer.Balance);
        }

        static private void FillRowString(int number, ref ISheet sheet, params string[] values)
        {
            //creates row, and fills it with the values given
            IRow row = sheet.CreateRow(number);
            for( int i = 0; i < values.Length; i++) row.CreateCell(i).SetCellValue(values[i]);
        }

        private static List<Transaction> GetTransactions()
        {
            List<object[]> table = _db.Query("select * from transactions");
            return new List<Transaction>(Array.ConvertAll(table.ToArray(), (row) => new Transaction(row)));
        }

        private static List<Customer> GetList(Excel ex)
        {
            List<Customer> list = new List<Customer>();
            ISheet sheet = ex.Workbook.GetSheetAt(0);
            for(int i = 1; i <= sheet.LastRowNum; i++)
            {
                List<ICell> cells = sheet.GetRow(i).Cells;
                Customer temp = new Customer(cells[0].StringCellValue, cells[2].StringCellValue, cells[1].StringCellValue, cells[3].NumericCellValue);
                list.Add(temp);
            }
            return list;
        }
    }
}
