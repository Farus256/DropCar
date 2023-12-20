using Kursach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Windows.Forms;

namespace Kursach.Repositories_CRUD
{
    public class UserRepository : ConnectionRepository
    {
        public UserRepository(string connectionString) : base(connectionString) { }

        public void RegisterUser(User user)
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                if (IsUserExists(user.Login, user.Password, user.Role))
                {
                    throw new InvalidOperationException("User with this login already exists.");
                }

                string query = "INSERT INTO [User] (Login, Role, password) VALUES (@Login, @Role, @Password)";
                repo.Connection.Execute(query, new { Login = user.Login, Role = user.Role, Password = user.Password });
            }
        }

        public bool IsUserExists(string login, string password, string role)
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                string query = "SELECT COUNT(*) FROM [User] WHERE Login = @Login AND Password = @Password AND Role = @Role";
                int count = repo.Connection.ExecuteScalar<int>(query, new { Login = login, Password = password, Role = role });
                return count > 0;
            }
        }

        public List<User> GetAllUsers()
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                var users = repo.Connection.Query<User>("SELECT * FROM [User]").ToList();
                return users;
            }
        }

        public bool UpdateUser(string name, string email, string address, string login, string role)
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                try
                {
                    if (role == "Client")
                    {
                        string query = "UPDATE [Clients] SET email = @Email, address = @Address, name_client = @Name WHERE Login = @Login";
                        repo.Connection.Execute(query, new { Login = login, Email = email, Address = address, Name = name });
                    }
                    else if (role == "Dealer")
                    {
                        string query = "UPDATE [Dealers] SET email = @Email, address = @Address, name_dealer = @Name WHERE Login = @Login";
                        repo.Connection.Execute(query, new { Login = login, Email = email, Address = address, Name = name });
                    }

                    return true;
                }

                catch (Exception ex) { MessageBox.Show(ex.Message); return false; }
            }
        }

    }
}
