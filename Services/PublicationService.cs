using System.Collections.Generic;
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
        public List <Publication> GetAllPublicationsActive() 
        {
            return _publicationRepository.GetAllPublicationsActive();
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
        public void AddDeal(Deal deal)
        {
            _publicationRepository.AddDeal(deal);
        }
        public List <Publication> GetPublicationsFilter(decimal targetPrice, string condition)
        {
            return _publicationRepository.GetPublicationsFilter(targetPrice, condition);
        }
    }
}

