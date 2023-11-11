using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.XSSF;

namespace Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Excel CustomerSheet = new Excel("./customers.xlsx");
            List<Customer> customers = GetCustomers(CustomerSheet);
            List<Transaction> transactions = GetTransactions();
            foreach (Transaction transaction in transactions)
            {
                if(customers.Exists((item) => item.Id == transaction.CustomerID))
                {
                    customers.Find((item) => item.Id == transaction.CustomerID).Balance += transaction.Amount;
                }
                else
                {
                    customers.Add(new Customer(transaction.CustomerID, "", "", transaction.Amount));
                }
            }
            Excel Output = new Excel();
            FillSheet(ref Output, customers);
            Output.Save("./output.xlsx");
        }

        private static void FillSheet(ref Excel output, List<Customer> customers)
        {
            ISheet sheet = output.Workbook.CreateSheet("customers");
            FillRowString(ref sheet, "ID", "surname", "firstname", "balance");
            for (int i = 1; i <= customers.Count; i++) FillRowCustomer(i, ref sheet, customers[i-1]);
        }

        private static void FillRowCustomer(int i, ref ISheet sheet, Customer customer)
        {
            IRow row = sheet.CreateRow(i);
            row.CreateCell(0).SetCellValue(customer.Id);
            row.CreateCell(1).SetCellValue(customer.LastName);
            row.CreateCell(2).SetCellValue(customer.FirstName);
            row.CreateCell(3).SetCellValue(customer.Balance);
        }

        private static void FillRowString(ref ISheet sheet, params string[] titles)
        {
            IRow row = sheet.CreateRow(0);
            for(int i = 0; i < titles.Length; i++) row.CreateCell(i).SetCellValue(titles[i]);
        }

        private static List<Transaction> GetTransactions()
        {
            using (DataBase db = new DataBase("server=localhost;database=it607labs;Uid=student;pwd=secret"))//connection to database
            {
                string q = "select * from transactions;";//query string
                return new List<Transaction>(Array.ConvertAll(db.Query(q).ToArray(), (row) => new Transaction(row)));
            }
        }

        private static List<Customer> GetCustomers(Excel customerSheet)
        {
            List<Customer> result = new List<Customer>();
            ISheet sheet = customerSheet.Workbook.GetSheetAt(0);
            for(int i = 1; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                result.Add(new Customer(row.GetCell(0).StringCellValue, row.GetCell(2).StringCellValue, row.GetCell(1).StringCellValue, row.GetCell(3).NumericCellValue));
            }
            return result;
        }
    }
}
