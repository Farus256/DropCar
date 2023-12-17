using Kursach.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using Kursach.Repositories_CRUD;

namespace Kursach.Repositories_CRUD
{
    public class ClientRepository : ConnectionRepository
    {
        public ClientRepository(string connectionString) : base(connectionString) { }
        public List<Client> GetAllClients()
        {
            var clients = Connection.Query<Client>("SELECT * FROM Clients").ToList();
            return clients;
        }

        public bool IsClientExists(string login)
        {
            string query = "SELECT COUNT(*) FROM Clients WHERE Login = @Login";
            int count = Connection.ExecuteScalar<int>(query, new { Login = login });
            return count > 0;
        }

        public Client GetClient()
        {
            throw new NotImplementedException("Метод еще не реализован.");
        }

        public bool AddClient(string login)
        {
            
            if (IsClientExists(login))
            {
                return false;
            }
            else
            {
                string query = "INSERT INTO [Clients] (Login) VALUES ( @Login)";
                Connection.Execute(query, new {Login = login });
                return true;
            }
            
        }

        public void UpdateClient()
        {
            throw new NotImplementedException("Метод еще не реализован.");
        }

        public void DeleteClient()
        {
            throw new NotImplementedException("Метод еще не реализован.");
        }
    }
}
