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
    public class UserRepository : ConnectionRepository
    {
        public UserRepository(string connectionString) : base(connectionString) {}

        public void RegisterUser(User user)
        {
            if (IsUserExists(user.Login, user.Password, user.Role))
            {
                throw new InvalidOperationException("User with this login already exists.");
            }

            string query = "INSERT INTO [User] (Login, Role, password) VALUES (@Login, @Role, @Password)";
            Connection.Execute(query, new { Login = user.Login, Role = user.Role, Password = user.Password });
        }

        public bool IsUserExists(string login, string password, string role)
        {
            string query = "SELECT COUNT(*) FROM [User] WHERE Login = @Login AND Password = @Password AND Role = @Role";
            int count = Connection.ExecuteScalar<int>(query, new { Login = login, Password = password, Role = role});
            return count > 0;
        }

        public List<User> GetAllUsers()
        {
            var users = Connection.Query<User>("SELECT * FROM [User]").ToList();
            return users;
        }

        public User GetUser()
        {
            throw new NotImplementedException("Метод еще не реализован.");
        }

        public void AddUser()
        {
            throw new NotImplementedException("Метод еще не реализован.");
        }

        public void UpdateUser()
        {
            throw new NotImplementedException("Метод еще не реализован.");
        }

        public void DeleteUser()
        {
            throw new NotImplementedException("Метод еще не реализован.");
        }
    }
}
