using PA01_Management_Application.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PA01_Management_Application.MVVM.View
{

    /// <summary>
    /// Interaction logic for BookingScreeningTime.xaml
    /// </summary>
    public partial class BookingScreeningTime : UserControl
    {
        private bool isButtonPressed = false;
        Button DateClickedButton = null;
        Button CityClickedButton = null;
        public class DateInfo
        {
            public string Day { get; set; }
            public string DayOfWeek { get; set; }
            public string Month { get; set; }
        }
        public class CinemaDetailsViewModel
        {
            public string CinemaName { get; set; }
            public string TheaterName { get; set; }
            public ObservableCollection<string> TimeButtons { get; set; }
        }
        BookingScreeningTimeViewModel ViewModel;
        public BookingScreeningTime()
        {
            InitializeComponent();
            ViewModel = new BookingScreeningTimeViewModel();
            DataContext = ViewModel;
        }

        private void BtnDate_Click(object sender, RoutedEventArgs e)
        {

            if (DateClickedButton != null)
            {
                DateClickedButton.Foreground = Brushes.Black; // Đặt màu chữ của nút về màu ban đầu
                Border otherBorder = DateClickedButton.Template.FindName("PART_Border", DateClickedButton) as Border;
                if (otherBorder != null)
                {
                    otherBorder.Background = Brushes.Transparent; // Đặt màu nền của border về màu ban đầu
                }
            }
            DateClickedButton = sender as Button;


            if (DateClickedButton != null)
            {
                // Thiết lập màu của nút được click
                DateClickedButton.Foreground = Brushes.Black;
                Border border = DateClickedButton.Template.FindName("PART_Border", DateClickedButton) as Border;
                if (border != null)
                {
                    border.Background = Brushes.LightGray; // Đặt màu của border
                }

            }
        }
        private void BtnCity_Click(object sender, RoutedEventArgs e)
        {

            if (CityClickedButton != null)
            {
                CityClickedButton.Foreground = Brushes.Black; // Đặt màu chữ của nút về màu ban đầu
                Border otherBorder = CityClickedButton.Template.FindName("CityBorder", CityClickedButton) as Border;
                if (otherBorder != null)
                {
                    otherBorder.Background = Brushes.Transparent; // Đặt màu nền của border về màu ban đầu
                }
            }
            CityClickedButton = sender as Button;


            if (CityClickedButton != null)
            {
                // Thiết lập màu của nút được click
                CityClickedButton.Foreground = Brushes.Black;
                Border border = CityClickedButton.Template.FindName("CityBorder", CityClickedButton) as Border;
                if (border != null)
                {
                    border.Background = Brushes.LightGray; // Đặt màu của border
                }

            }
        }
    }
}
