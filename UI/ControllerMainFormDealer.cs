using Kursach.Logic;
using Kursach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.UI
{
    public class ControllerMainFormDealer
    {
        private readonly PublicationService _publicationService;

        public ControllerMainFormDealer(PublicationService publicationService)
        {
            _publicationService = publicationService;
        }

        public List<Publication> GetPublications()
        {
            return _publicationService.GetAllPublications();
        }

        public void AddPublication(string login, string nameCar, double price, string modelCar, string techState, string typeCar, byte[] photo, string status)
        {
            // Получаем текущую дату и время для DatePubl
            DateTime datePubl = DateTime.Now;

            // Создаем экземпляр модели Publication с полученными данными
            var newPublication = new Publication
            {
                Login = login,
                date_publ = datePubl,
                name_car = nameCar,
                price = price,
                Model_Car = modelCar,
                Tech_State = techState,
                Type_Car = typeCar,
                Photo = photo,
                Status = status
            };

            // Вызываем сервис для добавления публикации в БД
            _publicationService.AddPublication(newPublication);

            // Опционально: обновляем отображение на форме, если это необходимо
        }

        public List<string> GetModels()
        {
            return _publicationService.GetModels();
        }

    }
    
}
