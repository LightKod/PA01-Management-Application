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
using PA01_Management_Application.MVVM.ViewModel;

namespace PA01_Management_Application.MVVM.View
{
    /// <summary>
    /// Interaction logic for ShowBookingDetail.xaml
    /// </summary>
    public partial class ShowBookingDetail : UserControl
    {
        private ShowBookingDetailViewModel _viewModel;
        public ShowBookingDetail(int bookingCode, string cinema, string seats)
        {
            InitializeComponent();

            _viewModel = new ShowBookingDetailViewModel();
            DataContext = _viewModel;

            _viewModel.LoadBookingDetails(bookingCode, cinema, seats);
        }
    }
}
