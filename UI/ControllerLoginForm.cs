using Kursach.Logic;
using Kursach.Models;
using Kursach.Services;
using System;
using System.Windows.Forms;

namespace Kursach.UI
{
    public class ControllerLoginForm
    {
        private readonly UserService _userService;
        private readonly ClientService _clientService;
        private readonly DealerService _dealerService;

        public ControllerLoginForm(UserService userService, ClientService clientService, DealerService dealerService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            _dealerService = dealerService ?? throw new ArgumentNullException(nameof(dealerService));
        }

        public bool Login(string login, string password, string role)
        {
            try
            {
                if (_userService.IsLoginExists(login, password, role))
                {
                    
                    if (role == "Client")
                    {
                        _clientService.AddClient(login);
                    }
                    else if (role == "Dealer")
                    {
                        _dealerService.AddDealer(login);
                    }
                    return true;
                    // NextFormClient(login, password, role);
                }
                else
                {
                    
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public bool Registration(string login, string password, string role)
        {
            try
            {
                var newUser = new User { Login = login, Role = role, Password = password };
                _userService.RegisterUser(newUser);
                MessageBox.Show("Registration successful!");
                return true;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return false;
            }
        }

        public void AddInfoUser(string name, string email, string address, string login, string role)
        {
            _userService.AddInfo(name, email, address, login, role);
        }
        public bool CheckUserData(string login, string role)
        {
            if (role == "Client")
            {
                return _clientService.CheckUserData(login);
            }
            else if (role == "Dealer")
            {
                return _dealerService.CheckUserData(login);
            }
            else { return false; }
        }
        public void AddPhones(string login, string role, string phone1, string phone2)
        {
            if (role == "Client")
            {
                 _clientService.AddPhone(login, phone1);
            }
            else if (role == "Dealer")
            {
                 _dealerService.AddPhone(login, phone1, phone2);
            }
            
        }

    }
}
