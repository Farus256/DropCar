using Kursach.Connections_Inicialization;
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

        public MainFormClient(string login)
        {
            loginClient = login;
            var serviceFactory = new ServiceCreator();
            var userService = serviceFactory.CreateUserService();
            var clientService = serviceFactory.CreateClientService();
            var publicationService = serviceFactory.CreatePublicationService();
            InitializeComponent();
            _controller = new ControllerMainFormClient(publicationService);
            InitializeDataGridView();

            // Другие инициализации, если необходимо
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
    }
}
