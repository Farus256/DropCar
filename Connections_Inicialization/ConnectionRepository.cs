using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;

namespace Kursach.Repositories_CRUD
{
    public class ConnectionRepository : IDisposable
    {
        public readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        private IDbConnection _db;
        public ConnectionRepository(string connectionString)
        {
            _db = new SqlConnection(connectionString);
        }

        public IDbCommand CreateCommand()
        {
            OpenConnection();
            return _db.CreateCommand();
        }

        public IDbDataAdapter CreateDataAdapter()
        {
            return new SqlDataAdapter();
        }

        public void Dispose()
        {
            CloseConnection();
        }

        public IDbConnection Connection
        {
            get
            {
                OpenConnection();
                return _db;
            }
        }

        private void OpenConnection()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
        }

        private void CloseConnection()
        {
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }
    }
}

