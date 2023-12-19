using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kursach.Models; // Добавьте эту директиву using для использования моделей
using Kursach.Repositories_CRUD.Class;

namespace Kursach.Logic
{
    public class PublicationService
    {
        private readonly PublicationRepository _publicationRepository;
        public PublicationService(PublicationRepository publicationRepository)
        {
            _publicationRepository = publicationRepository;
        }
        public List<Publication> GetAllPublications()
        {
            return _publicationRepository.GetAllPublications();
        }
        public void AddPublication(Publication publication)
        {
            _publicationRepository.AddPublication(publication);
        }

        public List<string> GetModels()
        {
            return _publicationRepository.GetModels();
        }
    }
}

