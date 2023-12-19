using Kursach.Logic;
using Kursach.Models;
using Kursach.Repositories_CRUD.Class;
using Kursach.Repositories_CRUD;
using Kursach.Services;
using Kursach.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kursach.Connections_Inicialization;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;

namespace Kursach
{
    public partial class MainFormDealer : Form
    {
        private readonly string loginD;
        private readonly ControllerMainFormDealer _controller;

        public MainFormDealer(string login)
        {
            loginD = login;
            var serviceFactory = new ServiceCreator();
            var userService = serviceFactory.CreateUserService();
            var clientService = serviceFactory.CreateClientService();
            var dealerService = serviceFactory.CreateDealerService();
            var publicationService = serviceFactory.CreatePublicationService();
            InitializeComponent();
            _controller = new ControllerMainFormDealer(publicationService);
            InitializeDataGridView();
           
            InitializeComboBox();
            comboBox2.Items.Add("Sedan");
            comboBox2.Items.Add("Coupe");
            comboBox2.Items.Add("Sports car");
            comboBox2.Items.Add("Hatchback");
            comboBox2.Items.Add("Minivan");
            comboBox2.Items.Add("Pickup truck");
            comboBox2.Items.Add("Off-Road");
            comboBox2.Items.Add("Convertible");

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // dataGridView2.DataSource = clientService.GetAllPublications();
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void InitializeComboBox()
        {

            var models = _controller.GetModels();
            comboBox1.DataSource = models;

            // Заполнение ComboBox значениями из списка моделей






            // Добавьте ComboBox на форму


            // Добавьте обработчик события выбора элемента в ComboBox
            //comboBox1.SelectedIndexChanged += comboBox1.SelectedIndexChanged;
        }
        public byte[] Photo()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.gif) | *.jpg; *.jpeg; *.png; *.gif";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    // Прочитайте байты из файла изображения и верните их
                    return File.ReadAllBytes(filePath);
                }
            }

            return null; // Возвращайте null, если не выбран файл
        }

        public void button2_Click(object sender, EventArgs e)
        {
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string sprice = textBox4.Text;
            string loginSend = loginD;
            string name_сar = textBox1.Text;
            double price;
            double.TryParse(sprice, out price); 
            string model_сar = comboBox1.Text;
            string Tech_State = textBox2.Text;
            string Type_Car = comboBox2.Text;
            byte[] photo = Photo();
            string Status = "Active";
            _controller.AddPublication(loginSend, name_сar, price, model_сar, Tech_State, Type_Car, photo, Status);
        }

        
        private void InitializeDataGridView()
        {
            dataGridView2.AutoGenerateColumns = true;

            // Получение данных из контроллера
            var publications = _controller.GetPublications();

            // Привязка данных к DataGridView
            dataGridView2.DataSource = publications;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InitializeDataGridView();
        }
    }
}
