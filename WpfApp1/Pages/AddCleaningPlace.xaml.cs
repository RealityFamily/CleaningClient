﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
    public partial class AddCleaningPlace : Page
    {
        private ApplicationContext db = new ApplicationContext();

        public AddCleaningPlace()
        {
            InitializeComponent();

            RepeatUnit.ItemsSource = new List<string> { "нет", "раз в день", "раз в неделю", "раз в месяц", "раз в год" };
            RepeatUnit.SelectedIndex = 0;
            RepeatValue.Visibility = Visibility.Collapsed;

            WayUnit.ItemsSource = new List<string> { "нет", "мин.", "часов" };
            WayUnit.SelectedIndex = 0;
            WayValue.Visibility = Visibility.Collapsed;

            Loaded += GetClients;
            Loaded += GetEmployees;
        }

        private void GetClients(object sender, RoutedEventArgs e)
        {
            ClientsContainer.Children.Clear();

            foreach (Client client in db.Clients.ToList())
            {
                ClientsContainer.Children.Add(new ClientCell(client));
            }
        }

        private void GetEmployees(object sender, RoutedEventArgs e)
        {
            EmployeeContainer.Children.Clear();

            foreach (Employee employee in db.Employees.ToList())
            {
                EmployeeCell ec = new EmployeeCell(employee);
                ec.Choosing += Ec_Choosing;
                EmployeeContainer.Children.Add(ec);
            }
        }

        private void Ec_Choosing(bool res)
        {
            EmployeeInfo.Visibility = res ? Visibility.Visible : Visibility.Hidden;
        }

        private void PFCField_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !double.TryParse(((TextBox)sender).Text + e.Text, out _);
        }

        private void RepeatUnit_Selected(object sender, RoutedEventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex == 0)
            {
                RepeatValue.Visibility = Visibility.Collapsed;
            } else
            {
                RepeatValue.Visibility = Visibility.Visible;
            }
        }

        private void Minute_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int i;
            e.Handled = !(int.TryParse(((TextBox)sender).Text + e.Text, out i) && i >= 0 && i <= 60);
        }

        private void Hour_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int i;
            e.Handled = !(int.TryParse(((TextBox)sender).Text + e.Text, out i) && i >= 0 && i <= 24);
        }

        private void WayUnit_Selected(object sender, RoutedEventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex == 0)
            {
                WayValue.Visibility = Visibility.Collapsed;
            }
            else
            {
                WayValue.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).GoBack();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CleaningPlace cleaningPlace = null;

            List<Client> selectedClients = new List<Client>();
            foreach(ClientCell cc in ClientsContainer.Children)
            {
                if (cc.Choosen) { selectedClients.Add(cc.Client); }
            }

            if (string.IsNullOrWhiteSpace(TownField.Text) ||
                string.IsNullOrWhiteSpace(AdressField.Text) ||
                string.IsNullOrWhiteSpace(IndexField.Text) ||
                string.IsNullOrWhiteSpace(PFCField.Text))
            {

            } else if (string.IsNullOrWhiteSpace(BeginHour.Text) ||
                string.IsNullOrWhiteSpace(BeginMinute.Text) ||
                string.IsNullOrWhiteSpace(EndHour.Text) ||
                string.IsNullOrWhiteSpace(EndMinute.Text) ||
                !BeginDate.SelectedDate.HasValue ||
                !EndDate.SelectedDate.HasValue)
            {

            } else if (selectedClients.Count != 1) {

            } else {
                cleaningPlace = new CleaningPlace()
                {
                    Town = TownField.Text,
                    Addres = AdressField.Text,
                    Index = IndexField.Text,
                    PriceFromClient = double.Parse(PFCField.Text),
                    WorkTime = new WorkTime()
                    {
                        StartTime = new DateTime(BeginDate.SelectedDate.Value.Year, BeginDate.SelectedDate.Value.Month, BeginDate.SelectedDate.Value.Day, int.Parse(BeginHour.Text), int.Parse(BeginMinute.Text), 0),
                        EndTime = new DateTime(EndDate.SelectedDate.Value.Year, EndDate.SelectedDate.Value.Month, EndDate.SelectedDate.Value.Day, int.Parse(EndHour.Text), int.Parse(EndMinute.Text), 0)
                    },
                    ClientId = selectedClients[0].Id,
                    Client = selectedClients[0]
                };
                db.CleaningPlaces.Add(cleaningPlace);
            }

            db.SaveChanges();

            List<Employee> selectedEmployees = new List<Employee>();
            foreach (EmployeeCell ec in EmployeeContainer.Children)
            {
                if (ec.Choosen) { selectedEmployees.Add(ec.Employee); }
            }

            if (selectedEmployees.Count > 1)
            {

            } else if (selectedEmployees.Count == 1)
            {
                db.Cleanings.Add(new Cleaning {
                    Place = cleaningPlace,
                    PlaceId = cleaningPlace.Id,
                    Worker = selectedEmployees[0],
                    WorkerId = selectedEmployees[0].Id,

                });

                db.SaveChanges();
            }

            (Window.GetWindow(this) as MainWindow).GoTo(new CalendarPage());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).GoTo(new AddClient());
        }
    }
}
