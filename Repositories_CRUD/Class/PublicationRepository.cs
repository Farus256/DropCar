using Kursach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Kursach.Repositories_CRUD.Class
{
    public class PublicationRepository : ConnectionRepository
    {
        public PublicationRepository(string connectionString) : base(connectionString) { }
        public List<Publication> GetAllPublications()
        {
            var publications = Connection.Query<Publication>("SELECT * FROM Publications").ToList();
            return publications;
        }
    }
}
