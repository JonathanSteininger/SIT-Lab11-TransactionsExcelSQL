using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab11TransactionsExcelSQL
{
    internal class Transaction
    {
        private string _id;
        private string _customerID;
        private string _productID;
        private double _amount;
        public string TransactionID { get { return _id; } }
        public string CustomerID { get { return _customerID; } }
        public string ProductID { get { return _productID; } }
        public double Amount { get { return _amount; } }

        public Transaction(string id, string customerID, string productID, double amount)
        {
            _id = id;
            _customerID = customerID;
            _productID = productID;
            _amount = amount;
        }
        public Transaction(params object[] v) : this((string)v[0], (string)v[1], (string)v[2], (double)v[3])
        {

        }
        public override string ToString()
        {
            return $"Transaction ID: {_id}; Customer ID: {_customerID}; Product ID: {_productID}; Amount: ${_amount:0.00}";
        }
    }
}
