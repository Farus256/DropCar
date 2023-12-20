using Kursach.Logic;
using Kursach.Models;
using System;
using System.Collections.Generic;

namespace Kursach.UI
{
    public class ControllerMainFormClient
    {
        private readonly PublicationService _publicationService;
        private readonly ClientService _clientService;
        public ControllerMainFormClient(PublicationService publicationService, ClientService clientservice)
        {
            _publicationService = publicationService;
            _clientService = clientservice;
        }

        public List<Publication> GetPublications()
        {
            return _publicationService.GetAllPublications();
        }

        public string GetRealName(string login)
        {
            return _clientService.GetRealname(login);
        }
        public List<string> GetDealerPhones(string login)
        {
            return _clientService.GetDealerPhones(login);
        }
        public void AddDeal(int selectedPublicationId, DateTime DateBuy, decimal finalPrice, string loginClient)
        {
            var deal = new Deal
            {
                IdPublication = selectedPublicationId,
                DateBuy = DateBuy,
                FinalPrice = finalPrice,
                Login = loginClient
            };
            _publicationService.AddDeal(deal);
        }

        public List<Publication> GetPublicationsFilter(decimal targetPrice, string condition)
        {
            return _publicationService.GetPublicationsFilter(targetPrice, condition);



        }
    }
}
