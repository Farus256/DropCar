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
            string query = "SELECT p.*, m.car_mark FROM Publication p INNER JOIN Model m ON p.model_car = m.model_car;";
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
        public IEnumerable<string> GetModelsByMark(string mark)
        {
            // Выполните SQL-запрос, чтобы получить модели для выбранной марки
            string query = "SELECT model_car FROM Model WHERE car_mark = @Mark";
            var parameters = new { Mark = mark };

            var models = Connection.Query<string>(query, parameters);
            return models;

        }
        public IEnumerable<string> GetCarMarks()
        {
            // Выполните SQL-запрос, чтобы получить уникальные марки автомобилей
            string query = "SELECT DISTINCT car_mark FROM Mark";

            var carMarks = Connection.Query<string>(query);
            return carMarks;
            
        }

        public void UpdatePublication(Publication publication)
        {
            string query = @"
                UPDATE Publication
                SET Date_Publ = @Date_Publ, Name_Car = @Name_Car, Price = @Price, Model_Car = @Model_Car,
                    Tech_state = @Tech_state, Type_Car = @Type_Car, Photo = @Photo
                WHERE id_publication = @id_publication
            ";

            Connection.Execute(query, publication);
        }

        public void DeletePublication(int id_publication)
        {
            string query = @"DELETE FROM Publication WHERE id_publication = @id_publication";

            Connection.Execute(query, new { id_publication });
        }

        public Statistics GetStatistics()
        {
            // Получение статистики
            string query = @"
                SELECT
                COUNT(DISTINCT p.type_car) AS TotalCarTypes,
                COUNT(DISTINCT m.car_mark) AS TotalCarMarks,
                COUNT(DISTINCT m.model_car) AS TotalCarModels,
                AVG(p.price) AS AveragePrice,
                CAST(SUM(CASE WHEN p.Status = 'Active' THEN 1 ELSE 0 END) AS decimal) / COUNT(*) AS ActiveAdsPercentage
                FROM Publication p INNER JOIN Model m ON p.model_car = m.model_car;
            ";

            var statistics = Connection.QueryFirstOrDefault<Statistics>(query);
            return statistics;
        }

    }
}
