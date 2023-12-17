using Kursach.Logic;
using Kursach.Repositories_CRUD;
using Kursach.Services;
using Kursach.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursach
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var userRepository = new UserRepository(connectionString);
            var clientRepository = new ClientRepository(connectionString);
            var userService = new UserService(userRepository);
            var clientService = new ClientService(clientRepository);
            var dealerRepository = new DealerRepository(connectionString);
            var dealerService = new DealerService(dealerRepository);

            // Создаем экземпляр LoginController и передаем ему сервисы
            var loginController = new ControllerLoginForm(userService, clientService, dealerService);

            Application.Run(new LoginForm(userService, clientService, dealerService));
        }
    }
}
