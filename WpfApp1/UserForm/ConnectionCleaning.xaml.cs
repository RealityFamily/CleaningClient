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
    /// Логика взаимодействия для ConnectionCleaning.xaml
    /// </summary>
    public partial class ConnectionCleaning : UserControl
    {
        public ConnectionCleaning()
        {
            InitializeComponent();
        }

        public List<Cleaning> Cleanings { get; set; }

        public void Show()
        {
            if (Cleanings.Count == 0)
            {
                MainBorder.Visibility = Visibility.Collapsed;
            }
            else if (Cleanings.Count == 1)
            {
                Text.Text = Cleanings[0].Place.Addres + "\n" + Cleanings[0].Place.Town + ", " + Cleanings[0].Place.Index;
            }
            else
            {
                Text.Text = "+" + Cleanings.Count;
            }
        }
    }
}
