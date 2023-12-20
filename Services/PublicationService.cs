using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kursach.Models; 
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
        public void UpdateInfo(Publication publication)
        {
            _publicationRepository.UpdatePublication(publication);
        }
        public void DeletePublication(int id_publication)
        {
            _publicationRepository.DeletePublication(id_publication);
        }
        public List<string> GetModels()
        {
            return _publicationRepository.GetModels();
        }
        public IEnumerable<string> GetModelsByMark(string mark)
        {
            return _publicationRepository.GetModelsByMark(mark);
        }
        public IEnumerable<string> GetCarMarks()
        {
            return _publicationRepository.GetCarMarks();
        }

        public Statistics GetStatistics()
        {
            return _publicationRepository.GetStatistics();  
        }
    }
}

