using Kursach.Models;
using Kursach.Repositories_CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Logic
{
    public class ClientService
    {
        private readonly ClientRepository _clientRepository;
        public ClientService(ClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public List<Client> GetAllClients()
        {
            return _clientRepository.GetAllClients();
        }
        public bool AddClient(string login)
        {

            return _clientRepository.AddClient(login);
        }
    }
}
