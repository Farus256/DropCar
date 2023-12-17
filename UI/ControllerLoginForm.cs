using Kursach.Logic;
using Kursach.Models;
using Kursach.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        public void Login(string login, string password, string role)
        {
            try
            {
                if (_userService.IsLoginExists(login, password, role))
                {
                    MessageBox.Show("Вход успешно выполнен!");
                    if (role == "Client")
                    {
                        _clientService.AddClient(login);
                    }
                    else if (role == "Dealer")
                    {
                        _dealerService.AddDealer(login);
                    }
                    else if (role == "Dealer/Client")
                    {
                        // Дополнительная логика для Dealer/Client
                    }
                    // NextFormClient(login, password, role);
                }
                else
                {
                    MessageBox.Show("Пользователь с таким логином и паролем не найден.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Registration(string login, string password, string role)
        {
            try
            {
                var newUser = new User { Login = login, Role = role, Password = password };
                _userService.RegisterUser(newUser);
                MessageBox.Show("Registration successful!");
                // Дополнительная логика, если необходимо
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }




    }
}
