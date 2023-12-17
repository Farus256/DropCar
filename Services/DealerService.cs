using Kursach.Models;
using Kursach.Repositories_CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    
}
