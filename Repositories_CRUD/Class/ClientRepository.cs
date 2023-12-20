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
using System.Net;
using System.Windows.Forms;

namespace Kursach.Repositories_CRUD
{
    public class ClientRepository : ConnectionRepository
    {
        public ClientRepository(string connectionString) : base(connectionString) { }

        public List<Client> GetAllClients()
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                var clients = repo.Connection.Query<Client>("SELECT * FROM Clients").ToList();
                return clients;
            }
        }

        public List<Publication> GetAllPublications()
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                string query = @"SELECT * FROM Publication";
                var publications = repo.Connection.Query<Publication>(query).ToList();
                return publications;
            }
        }

        public bool IsClientExists(string login)
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Clients WHERE Login = @Login";
                int count = repo.Connection.ExecuteScalar<int>(query, new { Login = login });
                return count > 0;
            }
        }

        public Client GetClient()
        {
            throw new NotImplementedException("Метод еще не реализован.");
        }

        public bool CheckUserData(string login)
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Clients WHERE Login = @Login AND (Email IS NOT NULL OR name_client IS NOT NULL OR Address IS NOT NULL OR Phone IS NOT NULL);";
                int count = repo.Connection.QuerySingle<int>(query, new { Login = login });
                return count > 0;
            }
        }

        public List<string> GetDealersPhones(string login)
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                string query = "SELECT Phone FROM Dealers_Phone WHERE Login = @Login";
                List<string> dealerPhones = repo.Connection.Query<string>(query, new { Login = login }).ToList();
                return dealerPhones;
            }
        }

        public string GetRealName(string login)
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                string query = "SELECT Name_Dealer FROM Dealers WHERE Login = @Login";
                string nameDealer = repo.Connection.QuerySingle<string>(query, new { Login = login });
                return nameDealer;
            }
        }

        public bool AddClient(string login)
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                if (IsClientExists(login))
                {
                    return false;
                }
                else
                {
                    string query = "INSERT INTO [Clients] (Login) VALUES ( @Login)";
                    repo.Connection.Execute(query, new { Login = login });
                    return true;
                }
            }
        }

        public void AddPhone(string login, string phone)
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                string query = "UPDATE Clients SET Phone = @Phone WHERE Login = @Login";
                repo.Connection.Execute(query, new { Phone = phone, Login = login });
            }
        }

    }
}
