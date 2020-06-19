using System;
using System.Collections.Generic;
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

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class ChangeClient : Page
    {
        private Client Client = null;
        public ChangeClient(Client client)
        {
            InitializeComponent();

            Client = client;

            NameField.Text = Client.Name;
            LastField.Text = Client.LastName;
            PhoneField.Text = Client.Phone;
            EmailField.Text = Client.Email;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LastField.Text) ||
                string.IsNullOrWhiteSpace(NameField.Text) ||
                string.IsNullOrWhiteSpace(PhoneField.Text) ||
                string.IsNullOrWhiteSpace(EmailField.Text))
            {

            } else {
                using (var db = new ApplicationContext())
                {
                    Client.Name = NameField.Text;
                    Client.LastName = LastField.Text;
                    Client.Phone = PhoneField.Text;
                    Client.Email = EmailField.Text;

                    db.Clients.Update(Client);

                    db.SaveChanges();
                }
                (Window.GetWindow(this) as MainWindow).GoBack();
            }
        }
    }
}
