using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Models;
using WpfApp1.UserForm;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChooseCleaningPlaces.xaml
    /// </summary>
    public partial class ChooseClient : Page
    {
        public ChooseClient()
        {
            InitializeComponent();

            Loaded += Cell_Reload;
        }

        private void Cell_Reload(object sender, EventArgs e)
        {
            ClientsContainer.Children.Clear();

            using (var db = new ApplicationContext())
            {
                foreach (Client client in db.Clients.ToList())
                {
                    var Cell = new ClientCell(client);
                    Cell.CanEdit = true;
                    Cell.CanDelete = true;
                    Cell.Reload += Cell_Reload;
                    ClientsContainer.Children.Add(Cell);
                }
            }
        }
    }
}
