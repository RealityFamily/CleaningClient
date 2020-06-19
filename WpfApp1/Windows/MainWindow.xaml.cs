using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Pages;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Content.Navigate(new CalendarPage());
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Content.Navigate(new CalendarPage());
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Content.Navigate(new AddCleaningPlace());
        }

        public void GoBack()
        {
            if (Content.CanGoBack)
            {
                Content.GoBack();
            }
        }

        public void GoTo(object Obj)
        {
            Content.Navigate(Obj);
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Content.Navigate(new ChooseCleaningPlaces());
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Content.Navigate(new ChooseClient());
        }
    }
}
