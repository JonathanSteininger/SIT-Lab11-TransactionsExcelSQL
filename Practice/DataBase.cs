using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using NPOI.OpenXmlFormats;

namespace Practice
{
    internal class DataBase : IDisposable
    {
        private string _connectionString;
        private MySqlConnection _conn;

        public DataBase(string ConnectionString)
        {
            Connect(ConnectionString);
        }

        public void Connect(string ConnectionString)
        {
            if (ConnectionString == null) throw new Exception("ConnectionString is Null");
            Close();
            _conn = new MySqlConnection(ConnectionString);
            _connectionString = ConnectionString;
        }
        public void Connect() => Connect(_connectionString);

        public void Close() { _conn?.Close(); }
        public void Open() { _conn?.Open(); }

        public List<object[]> Query(string QueryString)
        {
            List<object[]> list = new List<object[]>();
            try
            {
                if (_conn == null) throw new Exception("Mysql connection is null"); 
                Open();
                using(MySqlCommand cmd = new MySqlCommand(QueryString, _conn))
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        object[] row = new object[reader.FieldCount];
                        for(int i =  0; i < reader.FieldCount; i++)
                        {
                            row[i] = reader.GetValue(i);
                        }
                        list.Add(row);
                    }
                    reader.Close();
                }

            }catch (Exception ex)
            {
                Close();
                throw new Exception("MySQL Query Exception: ", ex);
            }
            Close();
            return list;
        }

        public void Dispose()
        {
            Close();
            _conn?.Dispose();
            _connectionString = null;
        }
    }
}
