using Kursach.Models;
using Kursach.Repositories_CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   

namespace Kursach.Services
{
    public class UserService
    {
        private readonly ClientRepository _clientRepository;
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository, ClientRepository clientRepository)
        {
            _userRepository = userRepository;
            _clientRepository = clientRepository;
        }

        public List<Clients> GetAllClients()
        {
            return _clientRepository.GetAllClients();
        }
        public List<User> GetAllUsers()
        {
            throw new NotImplementedException("Метод еще не реализован.");
        }
        
        public void RegisterUser(User user)
        {
            _userRepository.RegisterUser(user);
        }
        public bool IsLoginExists(string login, string password, string role)
        {
            return _userRepository.IsUserExists(login, password, role);
        }
        public bool AddClient(string login)
        {
            
            return _clientRepository.AddClient(login);
        }
    }
}
