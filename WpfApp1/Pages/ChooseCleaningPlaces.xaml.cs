using Microsoft.EntityFrameworkCore;
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
    public partial class ChooseCleaningPlaces : Page
    {
        public ChooseCleaningPlaces()
        {
            InitializeComponent();

            Loaded += ChooseCleaningPlaces_Loaded;
        }

        private void ChooseCleaningPlaces_Loaded(object sender, EventArgs e)
        {
            CleaningPlaecesContainer.Children.Clear();

            using (var db = new ApplicationContext())
            {
                foreach (CleaningPlace cp in db.CleaningPlaces.Include(cp => cp.Client).ToList())
                {
                    var cpc = new CleaningPlaceCell(cp);
                    cpc.CanDelete = true;
                    cpc.CanEdit = true;
                    cpc.Reload += ChooseCleaningPlaces_Loaded;

                    CleaningPlaecesContainer.Children.Add(cpc);
                }
            }
        }
    }
}
