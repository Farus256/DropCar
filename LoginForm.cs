using Kursach.Logic;
using Kursach.Models;
using Kursach.Repositories_CRUD;
using Kursach.Services;
using Kursach.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
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
        public LoginForm(UserService userService, ClientService clientService, DealerService dealerService)
        {
            InitializeComponent();
            _loginController = new ControllerLoginForm(userService, clientService, dealerService);

            InitializeComboBoxItems();
            comboBox1.SelectedIndex = 0;
        }
        private void InitializeComboBoxItems()
        {
            comboBox1.Items.Add("Client");
            comboBox1.Items.Add("Dealer");
            comboBox1.Items.Add("Dealer/Client");
            comboBox1.Items.Add("Admin");
        }

        public void NextFormClient(string login, string password, string role)
        {

            /*MainFormClient programForm = new MainFormClient(login, password, role);
            programForm.Show();
            //programForm.FormClosed += (s, args) => this.Close(); 
            this.Hide(); */
        }
        public void NextFormDealer(string login, string password, string role)
        {
            /*MainForm programForm = new MainForm(login, password, role);
            programForm.Show();
            //programForm.FormClosed += (s, args) => this.Close(); 
            this.Hide();*/
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
        }
        private void Login(string login, string password, string role)
        {
            _loginController.Login(login, password, role);
        }
        private void Registration(string login, string password, string role)
        {
            _loginController.Registration(login, password, role);
            VisibilityLogin();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            string role = comboBox1.Text;
            Login(login, password, role);
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
            string login = textBox1.Text;
            string password = textBox2.Text;
            string role = comboBox1.Text;
            Registration(login, password, role);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            VisibilityRegistration();
        }

    }
}
