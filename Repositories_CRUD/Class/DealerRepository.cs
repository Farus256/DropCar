using Dapper;
using Kursach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Repositories_CRUD
{
    public class DealerRepository : ConnectionRepository
    {
        public DealerRepository(string connectionString) : base(connectionString) { }

        public List<Dealer> GetAllDealers()
        {
            var dealers = Connection.Query<Dealer>("SELECT * FROM Dealers").ToList();
            return dealers;
        }

        public bool IsDealerExists(string Login)
        {
            string query = "SELECT COUNT(*) FROM Dealers WHERE Login = @Login";
            int count = Connection.ExecuteScalar<int>(query, new { login = Login }) ;
            return count > 0;
        }

        public Dealer GetDealer()
        {
            throw new NotImplementedException("Метод еще не реализован.");
        }

        public bool AddDealer(string Login)
        {
            if (IsDealerExists(Login))
            {
                return false;
            }
            else
            {
                string query = "INSERT INTO [Dealers] (Login) VALUES (@login)";
                Connection.Execute(query, new { login = Login });
                return true;
            }
        }

        public void UpdateDealer()
        {
            throw new NotImplementedException("Метод еще не реализован.");
        }

        public void DeleteDealer()
        {
            throw new NotImplementedException("Метод еще не реализован.");
        }
    }

}
