using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab11TransactionsExcelSQL
{
    internal class Customer
    {
        private string _id;
        private string _firstName;
        private string _lastName;
        private double _balance;
        public string CustomerId { get { return _id; } }
        public string FirstName { get { return _firstName; } }
        public string LastName { get { return _lastName; } }
        public double Balance { get { return _balance; } set { _balance = value; } }

        public Customer(string id, string firstName, string lastName, double balance)
        {
            _id = id;
            _firstName = firstName;
            _lastName = lastName;
            _balance = balance;
        }

        

        public override string ToString()
        {
            return $"{_id} : {_firstName} : {_lastName} : {_balance}";
        }
    }
}
