using Kursach.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursach.UI
{
    public class ClientController
    {
        private readonly UserService _clientService;

        public ClientController(UserService clientService)
        {
            _clientService = clientService;
        }

        public void DisplayAllClients()
        {
            /*var clients = _clientService.GetAllClients();
            StringBuilder sb = new StringBuilder();

            foreach (var client in clients)
            {
                sb.AppendLine($"ID: {client.id_client}, Имя: {client.name_client}, Email: {client.email}");
            }

            MessageBox.Show(sb.ToString(), "Все клиенты");*/
            throw new NotImplementedException("fSDDAS");
        }



    }
}
