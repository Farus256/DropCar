using Kursach.Logic;
using Kursach.Repositories_CRUD;
using Kursach.Repositories_CRUD.Class;
using Kursach.Services;
using Kursach.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursach
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            

           
           
            
            Application.Run(new LoginForm());

        }
    }
}
