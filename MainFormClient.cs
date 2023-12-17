using Kursach.Services;
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
        private readonly UserService clientService;
        public MainFormClient(string login, string password, string role)
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
           // dataGridView2.DataSource = clientService.GetAllPublications();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
