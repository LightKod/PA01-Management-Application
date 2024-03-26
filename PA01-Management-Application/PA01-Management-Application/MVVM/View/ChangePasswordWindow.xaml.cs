using PA01_Management_Application.MVVM.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

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

            // Perform additional password validation
            if (!PasswordValidationFails(newPassword))
            {
                MessageBox.Show("New password must contain at least one number, one uppercase letter, and be at least 8 characters long.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Close();

            bool passwordChanged = _viewModel.ChangePassword(oldPassword, newPassword);

            if (passwordChanged)
            {
                MessageBox.Show("Password changed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close(); // Close the window after successfully changing the password
            }
            else
            {
                MessageBox.Show("Failed to change password. Please check your old password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool PasswordValidationFails(string password)
        {
            
            return !Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
        }
    }
}
