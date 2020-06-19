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
using WpfApp1.Pages;

namespace WpfApp1.UserForm
{
    /// <summary>
    /// Логика взаимодействия для ClientCell.xaml
    /// </summary>
    public partial class ClientCell : UserControl
    {
        public Client Client { get; set; }
        public bool Choosen { get; set; }

        public bool CanDelete { get; set; }
        public bool CanEdit { get; set; }
        private bool Deleting = false;

        public event EventHandler Reload;

        public ClientCell(Client client)
        {
            InitializeComponent();

            this.Client = client;

            Names.Text = client.Id.ToString() + ". " + Client.LastName + " " + Client.Name;
            ClientPhone.Text = "Телефон: " + Client.Phone;
            ClientEmail.Text = "Почта: " + Client.Email;
        }

        private void MainBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CanDelete)
            {
                if (Deleting)
                {
                    Deleting = false;
                    MainBorder.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#EFDCCD"));
                    return;
                }
            }
            if (CanEdit)
            {
                (Window.GetWindow(this) as MainWindow).GoTo(new ChangeClient(Client));
                return;
            }
            if (!CanDelete && !CanEdit)
            {
                this.Choosen = !Choosen;
                MainBorder.Background = Choosen ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#9CC3C2")) : (SolidColorBrush)(new BrushConverter().ConvertFrom("#EFDCCD"));
                return;
            }
        }

        private void MainBorder_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CanDelete)
            {
                if (Deleting)
                {
                    using (var db = new ApplicationContext())
                    {
                        foreach(CleaningPlace cp in db.CleaningPlaces.Where(cp => cp.ClientId == Client.Id))
                        {
                            db.CleaningPlaces.Remove(cp);
                        }

                        db.Clients.Remove(Client);
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

        public void Choosed()
        {
            this.Choosen = !Choosen;
            MainBorder.Background = Choosen ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#9CC3C2")) : (SolidColorBrush)(new BrushConverter().ConvertFrom("#EFDCCD"));
        }
    }
}
