using Microsoft.Win32;
using PA01_Management_Application.DataManagers;
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
    /// Interaction logic for AccountPageView.xaml
    /// </summary>
    public partial class AccountPageView : UserControl
    {

        private AccountViewModel _viewModel;

        public AccountPageView()
        {
            InitializeComponent();

            _viewModel = new AccountViewModel();
            DataContext = _viewModel;

            _viewModel.UpdateDataFromUserData();
        }

        public AccountPageView(AccountViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;

            _viewModel.UpdateDataFromUserData();
        }


        private void ChangeAvatarButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog.Title = "Chọn ảnh đại diện";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = openFileDialog.FileName;

                Uri imageUri = new Uri(selectedImagePath);
                BitmapImage bitmapImage = new BitmapImage(imageUri);
                avatarImage.ImageSource = bitmapImage;

            }
        }


        private void MenuItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Thiết lập màu sắc highlight khi chuột đi vào nút
            Button button = sender as Button;
            button.Background = Brushes.Gray;
        }

        private void MenuItem_MouseLeave(object sender, MouseEventArgs e)
        {
            // Thiết lập màu sắc ban đầu khi chuột rời khỏi nút
            Button button = sender as Button;
            button.Background = Brushes.Transparent;
        }

        private void updateProfile_Click(object sender, RoutedEventArgs e)
        {
            MVVM.View.UpdateProfileView updateProfileView = new MVVM.View.UpdateProfileView(_viewModel);

            (App.Current.MainWindow.DataContext as MVVM.ViewModel.AppWindowViewModel).CurrentView = updateProfileView;
        }
        private void ViewButton1_Click(object sender, RoutedEventArgs e)
        {
            MVVM.View.GiftView giftView = new MVVM.View.GiftView();

            (App.Current.MainWindow.DataContext as MVVM.ViewModel.AppWindowViewModel).CurrentView = giftView;
        }

        private void ViewButton2_Click(object sender, RoutedEventArgs e)
        {
            MVVM.View.VoucherView voucherView = new MVVM.View.VoucherView();

            (App.Current.MainWindow.DataContext as MVVM.ViewModel.AppWindowViewModel).CurrentView = voucherView;
        }

        private void ViewButton3_Click(object sender, RoutedEventArgs e)
        {
            MVVM.View.CouponView couponView = new MVVM.View.CouponView();

            (App.Current.MainWindow.DataContext as MVVM.ViewModel.AppWindowViewModel).CurrentView = couponView;
        }

        private void ViewButton4_Click(object sender, RoutedEventArgs e)
        {
            MVVM.View.PurchasedTicketView purchasedTicketView = new MVVM.View.PurchasedTicketView();

            (App.Current.MainWindow.DataContext as MVVM.ViewModel.AppWindowViewModel).CurrentView = purchasedTicketView;
        }

    }
}
