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

namespace WpfApp1.UserForm
{
    /// <summary>
    /// Логика взаимодействия для ClientCell.xaml
    /// </summary>
    public partial class EmployeeCell : UserControl
    {
        public Employee Employee { get; set; }
        public bool Choosen { get; set; }

        public delegate void ChoosingHandler(bool res);
        public event ChoosingHandler Choosing;

        public bool CanDelete { get; set; }
        private bool Deleting = false;

        public event EventHandler Reload;

        public EmployeeCell(Employee employee)
        {
            InitializeComponent();

            this.Employee = employee;

            Names.Text = Employee.Id.ToString() + ". " + Employee.LastName + " " + Employee.Name;
            EmployeePhone.Text = "Телефон: " + Employee.Phone;
            EmployeeEmail.Text = "Почта: " + Employee.Email;
            EmployeeIdNum.Text = "Лич. номер: " + Employee.IndentifyNumber;
        }

        private void MainBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CanDelete)
            {
                if (Deleting)
                {
                    Deleting = false;
                    MainBorder.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#EFDCCD"));
                }
                else
                {
                    this.Choosen = !Choosen;
                    MainBorder.Background = Choosen ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#9CC3C2")) : (SolidColorBrush)(new BrushConverter().ConvertFrom("#EFDCCD"));
                    Choosing.Invoke(Choosen);
                }
            }
            else
            {
                this.Choosen = !Choosen;
                MainBorder.Background = Choosen ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#9CC3C2")) : (SolidColorBrush)(new BrushConverter().ConvertFrom("#EFDCCD"));
                Choosing.Invoke(Choosen);
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
                        db.Employees.Remove(Employee);
                        db.SaveChanges();
                    }
                    Reload.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    this.Choosen = true;
                    MainBorder.Background = Brushes.OrangeRed;
                }
            }
        }

        public void Choosed()
        {
            this.Choosen = !Choosen;
            MainBorder.Background = Choosen ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#9CC3C2")) : (SolidColorBrush)(new BrushConverter().ConvertFrom("#EFDCCD"));
            Choosing.Invoke(Choosen);
        }
    }
}
