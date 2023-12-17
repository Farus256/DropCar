using Kursach.Models;
using Kursach.Repositories_CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Logic
{
    public class PublicationService
    {
        private readonly ClientRepository _clientRepository;
        public List<Publication> GetAllPublications()
        {
            return _clientRepository.GetAllPublications();
        }
    }
}
