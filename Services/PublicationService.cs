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
        private readonly PublicationRepository PublicationRepository;
        public List<Publication> GetAllPublications()
        {
            return PublicationRepository.GetAllPublications();
        }
    }
}

