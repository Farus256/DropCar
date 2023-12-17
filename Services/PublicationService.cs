using Kursach.Models;
using Kursach.Repositories_CRUD;
using Kursach.Repositories_CRUD.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Logic
{
    public class PublicationService
    {
        private readonly PublicationRepository PublicationRepository;
        public List<Publication> GetAllPublications()
        {
            return PublicationRepository.GetAllPublications();
        }
    }
}
