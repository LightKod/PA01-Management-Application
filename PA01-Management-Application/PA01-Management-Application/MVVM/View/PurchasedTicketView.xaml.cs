using PA01_Management_Application.MVVM.Models;
using PA01_Management_Application.MVVM.ViewModel;
using PA01_Management_Application.MVVM.ViewModel.Service;
using System;
using System.Collections.Generic;
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

namespace PA01_Management_Application.MVVM.View
{
    /// <summary>
    /// Interaction logic for PurchasedTicketView.xaml
    /// </summary>
    public partial class PurchasedTicketView : UserControl
    {
        private AccountViewModel _viewModel;
        public PurchasedTicketView(AccountViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = new PurchasedTicketViewModel(new PurchasedTicketService(new MovieManagementContext()), _viewModel.UserId);
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as PurchasedTicketViewModel)?.NextPage(_viewModel.UserId);
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as PurchasedTicketViewModel)?.PreviousPage(_viewModel.UserId);
        }

        private void ViewDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy booking code của vé được chọn
            Button button = sender as Button;

            int bookingCode = (button.DataContext as PurchasedTicket).BookingCode;
            TimeOnly? time = (button.DataContext as PurchasedTicket).Time;
            string cinemaName = (button.DataContext as PurchasedTicket).Cinema;
            string Seats = (button.DataContext as PurchasedTicket).Seat;

            Debug.WriteLine(bookingCode);

            MVVM.View.ShowBookingDetail showBookingView = new MVVM.View.ShowBookingDetail(bookingCode, cinemaName, Seats);

            (App.Current.MainWindow.DataContext as MVVM.ViewModel.AppWindowViewModel).CurrentView = showBookingView;
        }

    }
}
