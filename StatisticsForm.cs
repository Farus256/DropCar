using Kursach.Models;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using OxyPlot;
using System.Windows.Forms;
using System.Text;
using System.Drawing;

namespace Kursach
{
    public partial class StatisticsForm : Form
    {
        private Statistics _statistics;
        private PlotView _plotViewActiveAds;
        private TextBox _textBox;
        private SplitContainer _splitContainer;

        public StatisticsForm(Statistics statistics)
        {
            _statistics = statistics;

            // Устанавливаем размер окна по умолчанию
            this.Size = new Size(800, 600);

            // Создаем новый SplitContainer
            _splitContainer = new SplitContainer();
            _splitContainer.Dock = DockStyle.Fill;
            _splitContainer.Orientation = Orientation.Horizontal; // Устанавливаем ориентацию на горизонтальную
            this.Controls.Add(_splitContainer);

            // Создаем новый PlotView
            _plotViewActiveAds = CreatePlotView("Active Ads", _statistics.ActiveAdsPercentage);
            _splitContainer.Panel1.Controls.Add(_plotViewActiveAds);
            _plotViewActiveAds.Dock = DockStyle.Fill;

            // Создаем новый TextBox
            _textBox = new TextBox();
            _textBox.Multiline = true;
            _textBox.ReadOnly = true;
            _textBox.ScrollBars = ScrollBars.Vertical;
            _textBox.Font = new Font(_textBox.Font.FontFamily, 12); // Увеличиваем размер шрифта
            _splitContainer.Panel2.Controls.Add(_textBox);
            _textBox.Dock = DockStyle.Fill;

            // Показываем статистику в TextBox
            ShowStatistics();
        }

        private void ShowStatistics()
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine("Ad statistics:");
            message.AppendLine($"\n\nNumber of different types of cars: {_statistics.TotalCarTypes}");
            message.AppendLine($"\n\nNumber of car brands: {_statistics.TotalCarMarks}");
            message.AppendLine($"\n\nNumber of car models: {_statistics.TotalCarModels}");
            message.AppendLine($"\n\nAverage ad price: {_statistics.AveragePrice:C}");

            _textBox.Text = message.ToString();
        }

        private PlotView CreatePlotView(string title, decimal data)
        {
            var plotView = new PlotView();

            // Создаем новую модель диаграммы
            var model = new PlotModel { Title = title };

            // Создаем новую круговую диаграмму
            var pieSeries = new PieSeries
            {
                StrokeThickness = 2.0,
                InsideLabelPosition = 0.8,
                AngleSpan = 360,
                StartAngle = 0
            };

            // Добавляем данные в диаграмму
            pieSeries.Slices.Add(new PieSlice("Active Ads", (double)data));
            pieSeries.Slices.Add(new PieSlice("Inactive Ads", (double)(100 - data)));

            // Добавляем серию в модель
            model.Series.Add(pieSeries);

            // Присваиваем модель PlotView
            plotView.Model = model;

            return plotView;
        }
    }
}





