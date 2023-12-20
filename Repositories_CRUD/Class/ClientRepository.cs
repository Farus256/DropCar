﻿using Kursach.Models;
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
            var clients = Connection.Query<Client>("SELECT * FROM Clients").ToList();
            return clients;
        }

        public List<Publication> GetAllPublications()
        {
            string query = @"SELECT * FROM Publication";
            var publications = Connection.Query<Publication>(query).ToList();
            return publications;
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

        public bool CheckUserData(string login)
        {
            string query = "SELECT COUNT(*) FROM Clients c LEFT JOIN Clients_Phone cp ON c.login = cp.login WHERE c.Login = @Login AND (c.Email IS NOT NULL OR c.name_client IS NOT NULL OR c.Address IS NOT NULL OR cp.Phone IS NOT NULL);";

            int count = Connection.QuerySingle<int>(query, new { Login = login });

            return count > 0;
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

        public void DeleteClient()
        {
            throw new NotImplementedException("Метод еще не реализован.");
        }
    }
}
