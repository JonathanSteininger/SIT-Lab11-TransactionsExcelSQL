using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    internal class Transaction
    {
        private string _transactionID;
        private string _customerID;
        private string _productID;
        private double _amount;
        public string TransactionID { get { return _transactionID; } }
        public string CustomerID { get { return _customerID; } }
        public string ProductID { get { return _productID; } }
        public double Amount { get { return _amount; } set { _amount = value; } }

        public Transaction(string transactionID, string CustomerID,  string ProductID, double Amount)
        {
            this._transactionID = transactionID;
            this._customerID = CustomerID;
            this._productID = ProductID;
            this._amount = Amount;
        }
        public Transaction(params object[] args)
        {
            this._transactionID = (string)args[0];
            this._customerID = (string)args[1];
            this._productID = (string)args[2];
            this._amount = (double)args[3];
        }
    }
}
