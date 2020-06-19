using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
using WpfApp1.Pages;

namespace WpfApp1.UserForm
{
    /// <summary>
    /// Логика взаимодействия для ClientCell.xaml
    /// </summary>
    public partial class CleaningPlaceCell : UserControl
    {
        public CleaningPlace CleaningPlace { get; set; }

        public bool Choosen { get; set; }
        public bool CanDelete { get; set; }
        public bool CanEdit { get; set; }
        private bool Deleting = false;

        public event EventHandler Reload;

        public CleaningPlaceCell(CleaningPlace cp)
        {
            InitializeComponent();

            this.CleaningPlace = cp;

            Addres.Text = CleaningPlace.Addres;
            Town.Text = CleaningPlace.Town + ", " + CleaningPlace.Index;
            Time.Text = CleaningPlace.WorkTime.StartTime.ToString("dd.MM.yyyy HH:mm") + " - " + CleaningPlace.WorkTime.EndTime.ToString("dd.MM.yyyy HH:mm");
            Client.Text = CleaningPlace.Client.Id + ". " + CleaningPlace.Client.LastName + " " + CleaningPlace.Client.Name;
        }

        private void MainBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CanDelete)
            {
                if (Deleting)
                {
                    this.Deleting = true;
                    MainBorder.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#EFDCCD"));
                    return;
                }
            } 
            if (CanEdit)
            {
                Cleaning cleaning = null;
                using (var db = new ApplicationContext())
                {
                    cleaning = db.Cleanings.FirstOrDefault(c => c.PlaceId == CleaningPlace.Id);
                }

                (Window.GetWindow(this) as MainWindow).GoTo(new ChangeCleaningPlace(CleaningPlace, cleaning));
                return;
            } 
            if (!CanEdit && !CanDelete) 
            {
                this.Choosen = !Choosen;
                MainBorder.Background = Choosen ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#9CC3C2")) : (SolidColorBrush)(new BrushConverter().ConvertFrom("#EFDCCD"));
                return;
            }
        }

        private void MainBorder_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CanDelete) {
                if (Deleting)
                {
                    using (var db = new ApplicationContext())
                    {
                        db.CleaningPlaces.Remove(CleaningPlace);
                        db.SaveChanges();
                    }
                    Reload.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    this.Deleting = true;
                    MainBorder.Background = Brushes.OrangeRed;
                }
            }
        }
    }
}
