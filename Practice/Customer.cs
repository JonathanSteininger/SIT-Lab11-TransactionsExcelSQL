using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    internal class Customer
    {
        private string _id;
        private string _firstName;
        private string _lastName;
        private double _balance;
        public string Id { get { return _id; } }
        public string FirstName { get { return _firstName; } set { _firstName = value; } }
        public string LastName { get { return _lastName; } set { _lastName = value; } }
        public double Balance { get { return _balance; } set { _balance = value; } }
        public Customer(string id, string firstname, string lastname, double balance) {
            this._id = id;
            this._firstName = firstname;
            this._lastName = lastname;
            this._balance = balance;
        }

    }
}
