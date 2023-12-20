using Kursach.Models;
using Kursach.Repositories_CRUD;
using System.Collections.Generic;

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
        public bool CheckUserData(string login)
        {
            return _clientRepository.CheckUserData(login);
        }
        public string GetRealname(string login)
        {
            return _clientRepository.GetRealName(login);
        }
        public List<string> GetDealerPhones(string login)
        {
            return _clientRepository.GetDealersPhones(login);
        }
        public void AddPhone(string login, string phone) 
        {
            _clientRepository.AddPhone(login, phone);
        }
    }
}
