using Kursach.Connections_Inicialization;
using Kursach.Logic;
using Kursach.Models;
using Kursach.Repositories_CRUD;
using Kursach.Repositories_CRUD.Class;
using Kursach.Services;
using Kursach.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kursach
{
    public partial class LoginForm : Form
    {
        private readonly ControllerLoginForm _loginController;
        
        public LoginForm()
        {
            var serviceFactory = new ServiceCreator();
            var userService = serviceFactory.CreateUserService();
            var clientService = serviceFactory.CreateClientService();
            var dealerService = serviceFactory.CreateDealerService();
            InitializeComponent();
            _loginController = new ControllerLoginForm(userService, clientService, dealerService);

            InitializeComboBoxItems();
            comboBox1.SelectedIndex = 0;
        }
        public void InitializeComboBoxItems()
        {
            comboBox1.Items.Add("Client");
            comboBox1.Items.Add("Dealer");
        }

        public void NextFormClient(string login)
        {
            Form previousForm = this;
            var mainFormClient = new MainFormClient(login);
            mainFormClient.FormClosed += (s, args) =>
            {
             
                previousForm.Show(); // Показывает предыдущую форму
            };
            this.Hide(); // Скрывает текущую форму
            mainFormClient.Show();
        }
        public void NextFormDealer(string login)
        {
            Form previousForm = this;
            var mainFormDealer = new MainFormDealer(login);
            mainFormDealer.FormClosed += (s, args) =>
            {
                
                previousForm.Show(); // Показывает предыдущую форму
            };
            this.Hide(); // Скрывает текущую форму
            mainFormDealer.Show();
        }
        public void VisibilityAddInformation(string role)
        {
            linkLabel1.Visible = false;
            button3.Visible = false;
            button2.Visible = false;
            label6.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            button1.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            comboBox1.Visible = false;
            button4.Visible = false;

            textBox2.Clear();
            label7.Text = "Add some info...";

            textBox3.Visible = true;

            textBox5.Visible = true;
            label8.Visible = true;
            if (role == "Client")
            {
                textBox4.Visible = true;
                label9.Visible = true;
            }
            else
            {
                label9.Visible = true;
                label12.Visible = true;
                textBox4.Visible = true;
                textBox7.Visible = true;
            }

            
            label10.Visible = true;
            button5.Visible = true;
            textBox6.Visible = true;
            label11.Visible = true;
            
            
        }

        public void VisibilityLogin()
        {
            linkLabel1.Visible = true;
            button3.Visible = false;
            button2.Visible = false;
            label6.Visible = false;
            textBox1.Visible = true;
            textBox2.Visible = true;
            button1.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            comboBox1.Visible = true;
            button4.Visible = false;
            textBox1.Clear();
            textBox2.Clear();
            label7.Text = "Login";
            linkLabel2.Visible = false;
        }

        public void VisibilityRegistration() 
        {
            button3.Visible = false;
            button2.Visible = false;
            label6.Visible = false;
            textBox1.Visible = true;
            textBox2.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            comboBox1.Visible = true;
            button4.Visible = true;
            if (linkLabel1.Visible) { linkLabel1.Visible = false; }
            textBox1.Clear();
            textBox2.Clear();
            label7.Text = "Registration";
            linkLabel2.Visible = true;
        }
        private void Login(string login, string password, string role)
        {
            bool loginSuccess = _loginController.Login(login, password, role);

            if (loginSuccess)
            {
                // Проверка заполненности данных в базе данных перед отображением дополнительной информации
                bool userDataFilled = _loginController.CheckUserData(login, role);

                if (userDataFilled)
                {
                    
                    if (role == "Client")
                    {
                        NextFormClient(login);
                    }
                    else
                    {
                        NextFormDealer(login);
                    }
                }
                else
                {

                    VisibilityAddInformation(role);
                }
            }
            else if (!loginSuccess)
            {
                MessageBox.Show("Invalid login or password. Please try again.");
            }
        }
        private bool Registration(string login, string password, string role)
        {
            // Проверка на пустые поля
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in all fields.");
                return false;
            }

            bool registrationSuccess = _loginController.Registration(login, password, role);
            if (registrationSuccess)
            {
                VisibilityLogin();
            }
            else
            {
                MessageBox.Show("Registration failed. Please try again with different credentials.");
            }
            return registrationSuccess;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string login = textBox1.Text;
                string password = textBox2.Text;
                string role = comboBox1.Text;
                Login(login, password, role);
            }
            catch (SqlException ex)
            {
                // Обработка исключения SqlException
                MessageBox.Show($"Error connecting to the database: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Обработка других исключений
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VisibilityLogin();
        }

        private void button3_Click(object sender, EventArgs e)
        { 
            VisibilityRegistration();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string login = textBox1.Text;
                string password = textBox2.Text;
                string role = comboBox1.Text;
                Registration(login, password, role);
            }
            catch (SqlException ex)
            {
                // Обработка исключения SqlException
                MessageBox.Show($"Error connecting to the database: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Обработка других исключений
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            VisibilityRegistration();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверка на пустые поля
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(textBox5.Text) ||
                    string.IsNullOrWhiteSpace(textBox6.Text) ||
                    string.IsNullOrWhiteSpace(comboBox1.Text))
                {
                    MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Прерываем выполнение метода, так как не все поля заполнены
                }

                bool userDataFilled = _loginController.CheckUserData(textBox1.Text, comboBox1.Text);

                string login = textBox1.Text;
                string email = textBox3.Text;
                string address = textBox5.Text;
                string name = textBox6.Text;
                string role = comboBox1.Text;
                string phone1 = textBox4.Text;
                string phone2 = textBox7.Text;
                _loginController.AddPhones(login, role, phone1, phone2);
                _loginController.AddInfoUser(name, email, address, login, role);
                if (role == "Client")
                {
                    
                    NextFormClient(login);
                }
                else
                {
                    
                    NextFormDealer(login);
                }

                VisibilityAddInformation(role);
            }
            catch (SqlException ex)
            {
                // Обработка исключения SqlException
                MessageBox.Show($"Error connecting to the database: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Обработка других исключений
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            VisibilityLogin();
        }
    }
}
