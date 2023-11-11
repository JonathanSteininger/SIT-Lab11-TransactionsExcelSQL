using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Lab11TransactionsExcelSQL
{
    internal class DataBase
    {
        private string _connectionString;
        private MySqlConnection _conn;

        public DataBase(string ConnectionString)
        {
            Connect(ConnectionString);
        }

        public void Connect(string ConnectionString)
        {
            Close();
            _connectionString = ConnectionString;
            _conn = new MySqlConnection(_connectionString);
        }
        public void Connect() => Connect(_connectionString);
        public void Close() { if(_conn != null) _conn.Close(); }
        public void Open() { if(_conn != null) _conn.Open(); }


        public List<object[]> Query(string sql, params MySqlParameter[] args)
        {
            Open();
            List<object[]> result = new List<object[]>();
            using(MySqlCommand command = new MySqlCommand(sql, _conn))
            {
                command.Parameters.AddRange(args);
                command.Prepare();
                using(MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        object[] values = new object[reader.FieldCount];
                        for(int i = 0; i < values.Length; i++) values[i] = reader.GetValue(i);
                        result.Add(values);
                    }
                }
            }
            Close();
            return result;
        }

    }
}
