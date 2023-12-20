using Kursach.Connections_Inicialization;
using Kursach.Models;
using Kursach.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursach
{
    public partial class MainFormClient : Form
    {
        private readonly string loginClient;
        private readonly ControllerMainFormClient _controller;

        private int selectedPublicationId;
        private string selectedlogin;
        private int selectedprice;
        public MainFormClient(string login)
        {
            loginClient = login;
            var serviceFactory = new ServiceCreator();
            var userService = serviceFactory.CreateUserService();
            var clientService = serviceFactory.CreateClientService();
            var publicationService = serviceFactory.CreatePublicationService();
            InitializeComponent();
            _controller = new ControllerMainFormClient(publicationService, clientService);
            InitializeDataGridView();
            CheckTorg();


            this.dataGridView1.CellClick += new DataGridViewCellEventHandler(this.DataGridView1_CellClick);
        }

        private void InitializeDataGridView()
        {
            dataGridView1.AutoGenerateColumns = true;

            // Получение данных из контроллера
            var publications = _controller.GetPublications();

            // Привязка данных к DataGridView
            dataGridView1.DataSource = publications;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
        }
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string selectedPublicationName;
            string selectedTechtext;


            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                selectedlogin = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["login"].Value);
                selectedPublicationId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id_publication"].Value);
                selectedPublicationName = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["name_car"].Value);
                selectedTechtext = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["tech_state"].Value);
                selectedprice = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["price"].Value);
                UpdateLabel(selectedPublicationName, selectedTechtext, selectedprice, selectedlogin);

            }
        }
        private void UpdateLabel(string selectedPublicationName, string selectedTechtext, int selectedprice, string selectedlogin)
        {
            label2.Text = $"Selected Publication: {selectedPublicationName}";
            label6.Text = $"Tech. State: {selectedTechtext}";
            label3.Text = $"Price: {selectedprice} $";
            label5.Text = $"Name Dealer: {_controller.GetRealName(selectedlogin)}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                List<string> dealerPhones = _controller.GetDealerPhones(selectedlogin);
                DateTime DateBuy = DateTime.Now;

                
                decimal finalPrice;
                if (string.IsNullOrWhiteSpace(textBox1.Text) || !decimal.TryParse(textBox1.Text, out finalPrice))
                {
                    finalPrice = Convert.ToDecimal(selectedprice);
                }

                // Проверка на согласие дилера
                decimal initialPrice = Convert.ToDecimal(selectedprice);
                if (finalPrice < initialPrice * (decimal)0.8)
                {
                    MessageBox.Show("Dealer does not agree with the final price.", "Deal Rejected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                
                try { _controller.AddDeal(selectedPublicationId, DateBuy, finalPrice, loginClient); }
                catch 
                {
                    MessageBox.Show("Select a dealer to make deal.", "Select Dealer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                

                
                string dealMessage = $"Deal Information:\n\n";
                dealMessage += $"Selected Publication: {label2.Text}\n\n";
                dealMessage += $"{label6.Text}\n\n";
                dealMessage += $"{label3.Text}\n\n";
                dealMessage += $"Final Price: {finalPrice} $\n\n";
                dealMessage += $"Name Dealer: {_controller.GetRealName(selectedlogin)}";

                
                MessageBox.Show(dealMessage, "Deal Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                InitializeDataGridView();
            }
            
            catch 
            {
                
                //MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void CheckTorg()
        {
            if (checkBox1.Checked)
            {
                
                textBox1.Enabled = true;
            }
            else
            {
               
                textBox1.Enabled = false;
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
            CheckTorg();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (string.IsNullOrEmpty(selectedlogin))
                {
                    MessageBox.Show("Select a dealer to view phones.", "Select Dealer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                
                List<string> dealerPhones = _controller.GetDealerPhones(selectedlogin);

                string phonesMessage = "Dealer Phones:\n" + string.Join("\n", dealerPhones);
                
                MessageBox.Show(phonesMessage, "Dealer Phones", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (!decimal.TryParse(textBox2.Text, out decimal targetPrice))
                {
                    MessageBox.Show("Please enter a valid price.", "Invalid Price", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
               
                string condition = comboBox1.SelectedItem.ToString();

                var searchResults = _controller.GetPublicationsFilter(targetPrice, condition);
                
                dataGridView1.DataSource = searchResults;
               
                dataGridView1.Update();
                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
