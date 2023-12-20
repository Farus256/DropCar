using Kursach.Models;
using Kursach.Repositories_CRUD;
using System.Collections.Generic;

namespace Kursach.Logic
{
    public class DealerService
    {
        private readonly DealerRepository _dealerRepository;
        public DealerService(DealerRepository dealerRepository)
        {
            _dealerRepository = dealerRepository;
        }

        public List<Dealer> GetAllDealers()
        {
            return _dealerRepository.GetAllDealers();
        }

        public bool AddDealer(string id)
        {
            return _dealerRepository.AddDealer(id);
        }

        public bool CheckUserData(string login)
        {
            return _dealerRepository.CheckUserData(login);
        }
        public void AddPhone(string login, string phone1, string phone2)
        {
            _dealerRepository.AddPhone(login, phone1, phone2);
        }
    }

}
