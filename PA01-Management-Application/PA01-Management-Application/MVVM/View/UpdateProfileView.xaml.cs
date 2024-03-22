using PA01_Management_Application.DataManagers;
using PA01_Management_Application.MVVM.Model;
using PA01_Management_Application.MVVM.ViewModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;
using System;
using PA01_Management_Application.MVVM.Models;


namespace PA01_Management_Application.MVVM.View
{
    /// <summary>
    /// Interaction logic for UpdateProfileView.xaml
    /// </summary>
    public partial class UpdateProfileView : UserControl
    {
        private AccountViewModel _viewModel;

        public UpdateProfileView(AccountViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;

            txtEmail.IsEnabled = false;
            txtUsername.IsEnabled = false;
            txtPassword.IsEnabled = false;

            _viewModel.UpdateDataFromUserData();
            SetGender();

        }

        private void SetGender()
        {

            if (_viewModel.Gender == 0)
            {
                rbMale.IsChecked = true;
            }
            else
            {
                rbFemale.IsChecked = true;
            }
        }



        private void PasswordChangedHandler(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is AccountViewModel viewModel)
            {
                var passwordBox = sender as PasswordBox;
                if (passwordBox != null)
                {
                    var securePassword = passwordBox.SecurePassword;

                    // Chuyển đổi mật khẩu sang văn bản để hiển thị
                    var passwordString = new System.Net.NetworkCredential(string.Empty, securePassword).Password;

                    viewModel.Password = passwordString;
                }
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SaveChanges())
            {
                MessageBox.Show("Your profile has been updated successfully.", "Profile Updated", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            MVVM.View.AccountPageView accountProfileView = new MVVM.View.AccountPageView(_viewModel);

            (App.Current.MainWindow.DataContext as MVVM.ViewModel.AppWindowViewModel).CurrentView = accountProfileView;
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow(_viewModel);
            changePasswordWindow.ShowDialog();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MVVM.View.AccountPageView accountPageView = new MVVM.View.AccountPageView();

            (App.Current.MainWindow.DataContext as MVVM.ViewModel.AppWindowViewModel).CurrentView = accountPageView;
        }

    }
}
