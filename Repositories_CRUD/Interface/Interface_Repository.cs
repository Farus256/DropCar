using Kursach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Repositories_CRUD.Interface
{
    public interface Interface_Repository
    {
        void RegisterUser(User user);
        List<User> GetAllUsers();
        User GetUser();
        void AddUser();
        void UpdateUser();
        void DeleteUser();
        bool IsUserExists(string login, string password, string role);
    }
}
