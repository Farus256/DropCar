using Kursach.Logic;
using Kursach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.UI
{
    public class ControllerMainFormClient
    {
        private readonly PublicationService _publicationService;

        public ControllerMainFormClient(PublicationService publicationService)
        {
            _publicationService = publicationService;
        }

        public List<Publication> GetPublications()
        {
            return _publicationService.GetAllPublications();
        }
    }
}
