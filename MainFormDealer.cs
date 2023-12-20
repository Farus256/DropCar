using Kursach.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Kursach.Connections_Inicialization;
using Kursach.Models;
using System.Data;


namespace Kursach
{
    public partial class MainFormDealer : Form
    {
        private readonly string loginDealer;
        private readonly ControllerMainFormDealer _controller;
        private int selectedPublicationId;
        private int Mode = 0;
        public MainFormDealer(string login)
        {
            loginDealer = login;
            var serviceFactory = new ServiceCreator();
            var userService = serviceFactory.CreateUserService();
            var clientService = serviceFactory.CreateClientService();
            var dealerService = serviceFactory.CreateDealerService();
            var publicationService = serviceFactory.CreatePublicationService();
            InitializeComponent();
            _controller = new ControllerMainFormDealer(publicationService);
            InitializeDataGridView();

            InitializeComboBox();

            UpdateLabelPublicationId();
            this.dataGridView2.CellClick += new DataGridViewCellEventHandler(this.MainFormDealer_CellClick);

        }

        private void MainFormDealer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Проверяем, что индекс строки больше 0 и индекс столбца больше 0,
            // чтобы избежать обработки заголовков столбцов
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string loginValue = dataGridView2.Rows[e.RowIndex].Cells["Login"].Value.ToString();
                loginValue = loginValue.Replace(" ", "");

                if (loginValue == loginDealer)
                {
                    selectedPublicationId = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["id_publication"].Value);
                    UpdateLabelPublicationId();
                    if (Mode == 1) { FillControlsFromSelectedRow(dataGridView2.Rows[e.RowIndex]); }
                }
            }
        }

        private void UpdateLabelPublicationId()
        {
            Label labelPublicationId = (Label)this.Controls["label7"];

            if (Mode == 1)
            {
                labelPublicationId.Text = $"Mode: editing publication {selectedPublicationId}";
                labelPublicationId.ForeColor = Color.Blue; // Устанавливаем синий цвет текста
            }
            else if (Mode == 2)
            {
                labelPublicationId.Text = $"Mode: deleting publication {selectedPublicationId}";
                labelPublicationId.ForeColor = Color.Red; // Возвращаем цвет текста по умолчанию
            }
            else if (Mode == 0)
            {
                labelPublicationId.Text = $"Mode: adding new publication ";
                labelPublicationId.ForeColor = Color.Green; // Возвращаем цвет текста по умолчанию
            }
        }

        private void InitializeComboBox()
        {
            {
                comboBox2.Items.Add("Sedan");
                comboBox2.Items.Add("Coupe");
                comboBox2.Items.Add("Sports car");
                comboBox2.Items.Add("Hatchback");
                comboBox2.Items.Add("Minivan");
                comboBox2.Items.Add("Pickup truck");
                comboBox2.Items.Add("Off-Road");
                comboBox2.Items.Add("Convertible");
            }

            var carMarks = _controller.GetCarMarks();
            foreach (var carMark in carMarks)
            {
                comboBox4.Items.Add(carMark);
            }

            comboBox3.SelectedIndex = 0;

        }
        public byte[] Photo()
        {
            return _controller.GetPhoto();
        }

        public byte[] GetPhotoOrNull()
        {
            if (checkBox1.Checked)
            {
                return _controller.GetPhoto();
            }
            else
            {
                return null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sprice = textBox4.Text;
            string loginSend = loginDealer;
            string name_сar = textBox1.Text;
            double price;
            double.TryParse(sprice, out price);
            string model_сar = comboBox1.Text;
            string Tech_State = textBox2.Text;
            string Type_Car = comboBox2.Text;
            byte[] photo;
            string Status = comboBox3.Text;

            if (Mode == 0)
            {
                photo = GetPhotoOrNull();
                _controller.AddPublication(loginSend, name_сar, price, model_сar, Tech_State, Type_Car, photo, Status);
                InitializeDataGridView();
            }
            else if (Mode == 1)
            {
                photo = GetPhotoOrNull();
                _controller.UpdateInfo(loginSend, name_сar, price, model_сar, Tech_State, Type_Car, photo, Status, selectedPublicationId);
                InitializeDataGridView();
            }
            else if (Mode == 2)
            {
                _controller.DeletePublication(selectedPublicationId);
                InitializeDataGridView();
            }

        }


        private void InitializeDataGridView()
        {
            dataGridView2.AutoGenerateColumns = true;

            // Получение данных из контроллера
            var publications = _controller.GetPublications();

            // Привязка данных к DataGridView
            dataGridView2.DataSource = publications;

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.AutoResizeColumns();
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView2.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearFormFields();
            InitializeDataGridView();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (Mode == 2)
            {
                Mode = 0;
            }
            else
            {
                Mode++;
            }
            UpdateLabelPublicationId();
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMark = comboBox4.Text;

            // Получаем модели для выбранной марки
            var models = _controller.GetModelsByMark(selectedMark);

            // Обновляем второй ComboBox
            UpdateComboBox2(models);
        }

        private void UpdateComboBox2(IEnumerable<string> models)
        {
            // Очищаем текущие элементы второго ComboBox
            comboBox1.Items.Clear();

            // Добавляем новые элементы
            foreach (var model in models)
            {
                comboBox1.Items.Add(model);
            }
        }

        private void FillControlsFromSelectedRow(DataGridViewRow selectedRow)
        {
            // Заполнение текстбоксов и комбобоксов значениями из выбранной строки
            textBox1.Text = selectedRow.Cells["name_car"].Value.ToString();
            textBox4.Text = selectedRow.Cells["price"].Value.ToString();
            comboBox4.Text = selectedRow.Cells["car_mark"].Value.ToString();
            comboBox1.Text = selectedRow.Cells["model_car"].Value.ToString();
            textBox2.Text = selectedRow.Cells["tech_state"].Value.ToString();
            comboBox2.Text = selectedRow.Cells["type_car"].Value.ToString();
            comboBox3.Text = selectedRow.Cells["Status"].Value.ToString();


        }

        private void ClearFormFields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";

            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = -1;
            checkBox1.Checked = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //var statistics = _controller.GetStatistics();
            //_controller.ShowStatistics();
            ShowStatisticsForm();
        }

        private void ShowStatisticsForm()
        {
            // Получение статистики из контроллера
            var statistics = _controller.GetStatistics();

            // Создаем новую форму для отображения диаграмм
            StatisticsForm statisticsForm = new StatisticsForm(statistics);

            // Показываем форму
            statisticsForm.Show();
        }


    }
}
