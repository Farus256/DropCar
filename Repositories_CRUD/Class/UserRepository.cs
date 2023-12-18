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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Net;
using System.Xml.Linq;
using System.Windows.Forms;

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

        public bool UpdateUser(string name, string email, string address, string login, string role)
        {
            try
            {
                if (role == "Client")
                {
                    string query = "UPDATE [Clients] SET email = @Email, address = @Address, name_client = @Name WHERE Login = @Login";
                    Connection.Execute(query, new { Login = login, Email = email, Address = address, Name = name });
                }
                else if (role == "Dealer")
                {
                    string query = "UPDATE [Dealers] SET email = @Email, address = @Address, name_dealer = @Name WHERE Login = @Login";
                    Connection.Execute(query, new { Login = login, Email = email, Address = address, Name = name });
                }

                return true;
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); return false; }
        }

        public void DeleteUser()
        {
            throw new NotImplementedException("Метод еще не реализован.");
        }
    }
}
