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
    public partial class AddClient : Page
    {
        public AddClient()
        {
            InitializeComponent();
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
                    db.Clients.Add(new Client
                    {
                        LastName = LastField.Text,
                        Name = NameField.Text,
                        Phone = PhoneField.Text,
                        Email = EmailField.Text,
                        StartTime = DateTime.Today
                    });

                    db.SaveChanges();
                }
                (Window.GetWindow(this) as MainWindow).GoBack();
            }
        }
    }
}
