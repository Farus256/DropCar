using Kursach.Logic;
using Kursach.Models;
using Kursach.Repositories_CRUD.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

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
        public byte[] GetPhoto()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.gif) | *.jpg; *.jpeg; *.png; *.gif";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    return File.ReadAllBytes(filePath);
                }
            }

            return null;
        }
        public void UpdateInfo(string login, string nameCar, double price, string modelCar, string techState, string typeCar, byte[] photo, string status, int SelectedPublId)
        {
            DateTime datePubl = DateTime.Now;
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
                Status = status,
                id_publication = SelectedPublId
            };

            _publicationService.UpdateInfo(newPublication);
        }

        public void DeletePublication(int id_publication)
        {
            _publicationService.DeletePublication(id_publication);

        }

        public List<string> GetModels()
        {
            return _publicationService.GetModels();

        }
        public IEnumerable<string> GetModelsByMark(string mark)
        {
            return _publicationService.GetModelsByMark(mark);
        }
        public IEnumerable<string> GetCarMarks()
        {
            return _publicationService.GetCarMarks();
        }
        public Statistics GetStatistics()
        {
            return _publicationService.GetStatistics();
        }

        public void ShowStatistics()
        {
            // Получение статистики из контроллера
            var statistics = GetStatistics();

            // Отображение статистики в MessageBox
            StringBuilder message = new StringBuilder();
            message.AppendLine("Ad statistics:");
            message.AppendLine($"Number of different types of cars: {statistics.TotalCarTypes}");
            message.AppendLine($"Number of car brands: {statistics.TotalCarMarks}");
            message.AppendLine($"Number of car models: {statistics.TotalCarModels}");
            message.AppendLine($"Average ad price: {statistics.AveragePrice:C}");
            message.AppendLine($"Percentage of active ads: {statistics.ActiveAdsPercentage:P}");

            MessageBox.Show(message.ToString(), "Statistic", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

}
