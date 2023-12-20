using Kursach.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace Kursach.Repositories_CRUD.Class
{
    public class PublicationRepository : ConnectionRepository
    {
        public PublicationRepository(string connectionString) : base(connectionString) { }

        public List<Publication> GetAllPublications()
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                string query = "SELECT p.*, m.car_mark FROM Publication p INNER JOIN Model m ON p.model_car = m.model_car;";
                var publications = repo.Connection.Query<Publication>(query).ToList();
                return publications;
            }
        }
        public List<Publication> GetAllPublicationsActive()
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                string query = "SELECT p.*, m.car_mark FROM Publication p INNER JOIN Model m ON p.model_car = m.model_car WHERE p.Status = 'Active';";
                var publications = repo.Connection.Query<Publication>(query).ToList();
                return publications;
            }
        }

        public List<string> GetModels()
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                string query = "SELECT model_car FROM Model";
                var models = repo.Connection.Query<string>(query).ToList();
                return models;
            }
        }

        public void AddPublication(Publication publication)
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                string query = @"INSERT INTO Publication (Login, date_publ, name_car, price, model_car, tech_state, type_car, Photo, Status)
                    VALUES (@Login,@Date_Publ, @Name_Car, @Price, @Model_Car, @Tech_State, @Type_Car, @Photo, @Status)";
                repo.Connection.Execute(query, publication);
            }
        }

        public IEnumerable<string> GetModelsByMark(string mark)
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                string query = "SELECT model_car FROM Model WHERE car_mark = @Mark";
                var parameters = new { Mark = mark };
                var models = repo.Connection.Query<string>(query, parameters);
                return models;
            }
        }

        public IEnumerable<string> GetCarMarks()
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                string query = "SELECT DISTINCT car_mark FROM Mark";
                var carMarks = repo.Connection.Query<string>(query);
                return carMarks;
            }
        }

        public void UpdatePublication(Publication publication)
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                string query = @"
                    UPDATE Publication
                    SET Date_Publ = @Date_Publ, Name_Car = @Name_Car, Price = @Price, Model_Car = @Model_Car,
                        Tech_state = @Tech_state, Type_Car = @Type_Car, Photo = @Photo
                    WHERE id_publication = @id_publication";
                repo.Connection.Execute(query, publication);
            }
        }

        public void DeletePublication(int id_publication)
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                string query = @"DELETE FROM Publication WHERE id_publication = @id_publication";
                repo.Connection.Execute(query, new { id_publication });
            }
        }

        public Statistics GetStatistics()
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                string query = @"
                   SELECT
                   COUNT(DISTINCT p.type_car) AS TotalCarTypes,
                   COUNT(DISTINCT m.car_mark) AS TotalCarMarks,
                   COUNT(DISTINCT m.model_car) AS TotalCarModels,
                   AVG(p.price) AS AveragePrice,
                   (CAST(SUM(CASE WHEN p.Status = 'Active' THEN 1 ELSE 0 END) AS decimal) / COUNT(*)) * 100 AS ActiveAdsPercentage
                   FROM Publication p INNER JOIN Model m ON p.model_car = m.model_car;";
                var statistics = repo.Connection.QueryFirstOrDefault<Statistics>(query);
                return statistics;
            }
        }

        public void AddDeal(Deal deal)
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                string insertQuery = "INSERT INTO Deal (Id_Publication, Date_Buy, Final_Price, Login) VALUES (@IdPublication, @DateBuy, @FinalPrice, @Login);";
                repo.Connection.Execute(insertQuery, deal);

                string updateQuery = "UPDATE Publication SET Status = 'NoActive' WHERE Id_Publication = @IdPublication;";
                repo.Connection.Execute(updateQuery, new { IdPublication = deal.IdPublication });
            }
        }

        public List<Publication> GetPublicationsFilter(decimal targetPrice, string condition)
        {
            using (var repo = new ConnectionRepository(connectionString))
            {
                string sqlQuery = "SELECT p.*, m.car_mark FROM Publication p INNER JOIN Model m ON p.model_car = m.model_car WHERE p.price " +
                              (condition == "Above" ? ">=" : "<=") +
                              " @TargetPrice AND p.Status = 'Active'";
                var result = repo.Connection.Query<Publication>(sqlQuery, new { TargetPrice = targetPrice }).ToList();
                return result;
            }
        }
    }
}
