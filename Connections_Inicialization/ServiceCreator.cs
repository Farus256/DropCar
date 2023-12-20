using Kursach.Logic;
using Kursach.Repositories_CRUD.Class;
using Kursach.Repositories_CRUD;
using Kursach.Services;
using System.Configuration;

namespace Kursach.Connections_Inicialization
{
    public class ServiceCreator
    {
        private readonly string _connectionString;

        public ServiceCreator()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public UserService CreateUserService()
        {
            var userRepository = new UserRepository(_connectionString);
            return new UserService(userRepository);
        }

        public ClientService CreateClientService()
        {
            var clientRepository = new ClientRepository(_connectionString);
            return new ClientService(clientRepository);
        }

        public DealerService CreateDealerService()
        {
            var dealerRepository = new DealerRepository(_connectionString);
            return new DealerService(dealerRepository);
        }

        public PublicationService CreatePublicationService()
        {
            var publicationRepository = new PublicationRepository(_connectionString);
            return new PublicationService(publicationRepository);
        }
    }
}
