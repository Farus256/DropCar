using Dapper;
using Kursach.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kursach.Repositories_CRUD
{
    public class DealerRepository : ConnectionRepository
    {
        public DealerRepository(string connectionString) : base(connectionString) { }

        public List<Dealer> GetAllDealers()
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                var dealers = repo.Connection.Query<Dealer>("SELECT * FROM Dealers").ToList();
                return dealers;
            }
        }

        public bool IsDealerExists(string Login)
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Dealers WHERE Login = @Login";
                int count = repo.Connection.ExecuteScalar<int>(query, new { login = Login });
                return count > 0;
            }
        }

        public bool CheckUserData(string login)
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Dealers d LEFT JOIN Dealers_phone dp ON d.login = dp.login WHERE d.Login = @Login AND (d.Email IS NOT NULL OR d.Name_Dealer IS NOT NULL OR d.Address IS NOT NULL OR dp.Phone IS NOT NULL);";
                int count = repo.Connection.QuerySingle<int>(query, new { Login = login });
                return count > 0;
            }
        }

        public Dealer GetDealer()
        {
            throw new NotImplementedException("Метод еще не реализован.");
        }

        public bool AddDealer(string Login)
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                if (IsDealerExists(Login))
                {
                    return false;
                }
                else
                {
                    string query = "INSERT INTO [Dealers] (Login) VALUES (@login)";
                    repo.Connection.Execute(query, new { login = Login });
                    return true;
                }
            }
        }

        public void AddPhone(string login, string phone1, string phone2)
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                string query = "INSERT INTO Dealers_Phone (Phone, Login) VALUES (@Phone, @Login)";
                repo.Connection.Execute(query, new { Phone = phone1, Login = login });
                if (!string.IsNullOrEmpty(phone2))
                {
                    repo.Connection.Execute(query, new { Phone = phone2, Login = login });
                }
            }
        }


    }

}
