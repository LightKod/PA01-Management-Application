using PA01_Management_Application.MVVM.Model;
using PA01_Management_Application.MVVM.ViewModel;
using System.Windows;

namespace PA01_Management_Application.MVVM.View
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        private AccountViewModel _viewModel;

        public ChangePasswordWindow(AccountViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string oldPassword = txtOldPassword.Password;
            string newPassword = txtNewPassword.Password;
            string confirmPassword = txtConfirmPassword.Password;

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirm password do not match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Close();


            bool passwordChanged = _viewModel.ChangePassword(oldPassword, newPassword);

            if (passwordChanged)
            {
                MessageBox.Show("Password changed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close(); // Đóng cửa sổ sau khi thay đổi mật khẩu thành công
            }
            else
            {
                MessageBox.Show("Failed to change password. Please check your old password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
