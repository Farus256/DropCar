using Kursach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace Kursach.Repositories_CRUD.Class
{
    public class PublicationRepository : ConnectionRepository
    {
        public PublicationRepository(string connectionString) : base(connectionString) { }
        public List<Publication> GetAllPublications()
        {
            string query = "SELECT * FROM Publication";
            var publications = Connection.Query<Publication>(query).ToList();
            return publications;
        }
        public List<string> GetModels()
        {
            string query = "SELECT model_car FROM Model";
            var models = Connection.Query<string>(query).ToList();
            return models;
        }

        public void AddPublication(Publication publication)
        {
            string query = @"INSERT INTO Publication (Login, date_publ, name_car, price, model_car, tech_state, type_car, Photo, Status)
                VALUES (@Login,@Date_Publ, @Name_Car, @Price, @Model_Car, @Tech_State, @Type_Car, @Photo, @Status)";

            Connection.Execute(query, publication);
        }

        public void UpdatePublication(Publication publication)
        {
            string query = @"
                UPDATE Publication
                SET DatePubl = @DatePubl, NameCar = @NameCar, Price = @Price, ModelCar = @ModelCar,
                    TechState = @TechState, TypeCar = @TypeCar, Photo = @Photo
                WHERE IdPublication = @IdPublication
            ";

            Connection.Execute(query, publication);
        }

        public void DeletePublication(int idPublication)
        {
            string query = "DELETE FROM Publication WHERE IdPublication = @IdPublication";
            Connection.Execute(query, new { IdPublication = idPublication });
        }
    }
}
