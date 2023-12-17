using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;

namespace Kursach.Repositories_CRUD
{
    public class ConnectionRepository
    {
        public readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        private readonly IDbConnection _db;
        ~ConnectionRepository()
        {
            if (_db != null && _db.State == ConnectionState.Open)
                _db.Close();
        }

        public ConnectionRepository(string connectionString)
        {
            _db = new SqlConnection(connectionString);
        }

        protected IDbConnection Connection
        {
            get
            {
                if (_db.State == ConnectionState.Closed)
                    _db.Open();
                return _db;
            }
        }
    }
}

