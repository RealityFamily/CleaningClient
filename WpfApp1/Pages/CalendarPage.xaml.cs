using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class CalendarPage : Page
    {
        private DateTime Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

        public CalendarPage()
        {
            InitializeComponent();

            MonthInit(Date);
        }

        private void MonthInit(DateTime firstDayOfMonth)
        {
            Calendar.Children.Clear();

            for (int i = 0; i < Calendar.ColumnDefinitions.Count; i++)
            {
                if (i % 2 == 0)
                {
                    string text;
                    switch (i)
                    {
                        case 0:
                            text = "Понедельник";
                            break;
                        case 2:
                            text = "Вторник";
                            break;
                        case 4:
                            text = "Среда";
                            break;
                        case 6:
                            text = "Четверг";
                            break;
                        case 8:
                            text = "Пятница";
                            break;
                        case 10:
                            text = "Суббота";
                            break;
                        case 12:
                            text = "Воскресенье";
                            break;
                        default:
                            text = "";
                            break;
                    }

                    TextBlock tb = new TextBlock()
                    {
                        FontSize = 20,
                        Text = text,
                        VerticalAlignment = VerticalAlignment.Center,
                        TextAlignment = TextAlignment.Center
                    };
                    Grid.SetColumn(tb, i);
                    Grid.SetRow(tb, 0);
                    Calendar.Children.Add(tb);
                }
                else if (i % 2 == 1) {
                    Rectangle r = new Rectangle()
                    {
                        Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9CC3C2")),
                        Stretch = Stretch.Fill
                    };
                    Grid.SetColumn(r, i);
                    Grid.SetRow(r, 0);
                    Grid.SetRowSpan(r, Calendar.RowDefinitions.Count);
                    Calendar.Children.Add(r);
                }
            }

            for (int i = 1; i < Calendar.RowDefinitions.Count; i += 2)
            {
                Rectangle r = new Rectangle()
                {
                    Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9CC3C2")),
                    Stretch = Stretch.Fill
                };
                Grid.SetRow(r, i);
                Grid.SetColumn(r, 0);
                Grid.SetColumnSpan(r, Calendar.ColumnDefinitions.Count);
                Calendar.Children.Add(r);
            }

            int week = 2;
            for (DateTime date = firstDayOfMonth;
                    date <= new DateTime(firstDayOfMonth.Year, firstDayOfMonth.Month, DateTime.DaysInMonth(firstDayOfMonth.Year, firstDayOfMonth.Month));
                    date = date.AddDays(1))
            {
                Border b = new Border()
                {
                    Padding = new Thickness(5),
                    Background = date.Date == DateTime.Today ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#EFDCCD")) : Brushes.Transparent
                };
                Grid g = new Grid();

                g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20) });
                g.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10, GridUnitType.Star) });

                g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(20) });
                g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

                TextBlock tb = new TextBlock()
                {
                    FontSize = 15,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = date.ToString("dd")
                };

                Grid.SetRow(tb, 0);
                Grid.SetColumn(tb, 0);

                Grid.SetRow(b, week);

                switch (date.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        Grid.SetColumn(b, 12);
                        break;
                    case DayOfWeek.Monday:
                        Grid.SetColumn(b, 0);
                        break;
                    case DayOfWeek.Tuesday:
                        Grid.SetColumn(b, 2);
                        break;
                    case DayOfWeek.Wednesday:
                        Grid.SetColumn(b, 4);
                        break;
                    case DayOfWeek.Thursday:
                        Grid.SetColumn(b, 6);
                        break;
                    case DayOfWeek.Friday:
                        Grid.SetColumn(b, 8);
                        break;
                    case DayOfWeek.Saturday:
                        Grid.SetColumn(b, 10);
                        break;
                }

                g.Children.Add(tb);
                b.Child = g;
                Calendar.Children.Add(b);

                if (date.DayOfWeek == DayOfWeek.Sunday) { week += 2; }


                connectCleanings(date, g);
            }

            MonthText.Text = firstDayOfMonth.ToString("MMMM yyyy", CultureInfo.GetCultureInfo("ru-ru"));
        }

        private void connectCleanings(DateTime date, Grid g)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Date = Date.AddMonths(1);
            MonthInit(Date);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Date = Date.AddMonths(-1);
            MonthInit(Date);
        }
    }
}
