using Kursach.Models;
using Kursach.Repositories_CRUD;
using System;
using System.Collections.Generic;

namespace Kursach.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;

        }

        public void RegisterUser(User user)
        {
            _userRepository.RegisterUser(user);
        }
        public bool IsLoginExists(string login, string password, string role)
        {
            return _userRepository.IsUserExists(login, password, role);
        }

        public void AddInfo(string name, string email, string address, string login, string role)
        {
            _userRepository.UpdateUser(name, email, address, login, role);
        }
        
    }
}
