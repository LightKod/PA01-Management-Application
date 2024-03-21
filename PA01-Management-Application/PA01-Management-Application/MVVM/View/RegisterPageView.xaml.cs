using PA01_Management_Application.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for RegisterPageView.xaml
    /// </summary>
    public partial class RegisterPageView : UserControl
    {
        RegisterViewModel viewModel = new RegisterViewModel();
        public RegisterPageView()
        {
            InitializeComponent();
            ImageBrush myBrush = new ImageBrush();

            // Tạo một Image và thiết lập nguồn hình ảnh cho nó
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("pack://application:,,,/MVVM/Images/hero-img.jpg"));

            // Thiết lập nguồn hình ảnh cho ImageBrush
            myBrush.ImageSource = image.Source;
            container.Background = myBrush;
            //viewModel = new RegisterViewModel();
            DataContext = viewModel;
        }
        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender != null)
            {
                if (EmailErrorTextBlock != null && Email.Text != "your email")
                {
                    Debug.WriteLine(Email.Text);
                    if (!IsValidEmail(Email.Text))
                    {
                        EmailErrorTextBlock.Visibility = Visibility.Visible;
                        viewModel.ErrorEmail = "Invalid email format";
                    }
                    else
                    {
                        EmailErrorTextBlock.Visibility = Visibility.Collapsed;
                        viewModel.ErrorEmail = string.Empty;
                        if (viewModel.CheckIfEmailExists(Email.Text))
                        {
                            EmailErrorTextBlock.Visibility = Visibility.Visible;
                            viewModel.ErrorEmail = "Email have been already exists";
                        }
                    }
                }
            }
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

            if (sender != null)
            {
                PasswordBox passwordBox = sender as PasswordBox;
                if (passwordBox != null && passwordBox.Password != null)
                {
                    viewModel.Password = passwordBox.Password;
                }
                if (PasswordErrorTextBlock != null)
                {
                    if (PasswordValidationFails())
                    {
                        PasswordErrorTextBlock.Visibility = Visibility.Visible;
                        if (!ContainsNumber(PasswordBox.Password))
                        {
                            PasswordErrorTextBlock.Text = "Must contain at least one number";
                        }
                        else if (!ContainsUppercaseLetter(PasswordBox.Password))
                        {
                            PasswordErrorTextBlock.Text = "Must contain at least one uppercase letter";
                        }
                        else if (PasswordBox.Password.Length < 8)
                        {
                            PasswordErrorTextBlock.Text = "Must be at least 8 characters long";
                        }
                        viewModel.ErrorPassword = "Invalid email format";
                    }
                    else
                    {
                        PasswordErrorTextBlock.Visibility = Visibility.Collapsed;
                        PasswordErrorTextBlock.Text = string.Empty;
                        viewModel.ErrorPassword = string.Empty;
                    }
                }

            }


        }
        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password != ConfirmPasswordBox.Password)
            {
                if (ConfirmPasswordErrorTextBlock != null)
                {
                    ConfirmPasswordErrorTextBlock.Visibility = Visibility.Visible;
                    ConfirmPasswordErrorTextBlock.Text = "Passwords do not match";
                    viewModel.ErrorPassword = "Passwords do not match";
                }
            }
            else
            {
                if (ConfirmPasswordErrorTextBlock != null)
                {
                    ConfirmPasswordErrorTextBlock.Visibility = Visibility.Collapsed;
                    ConfirmPasswordErrorTextBlock.Text = string.Empty;
                    viewModel.ErrorPassword = string.Empty;

                }
            }
        }

        private bool PasswordValidationFails()
        {
            string password = PasswordBox.Password;
            // Thực hiện kiểm tra dữ liệu nhập vào
            return !Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
        }
        private bool ContainsNumber(string password)
        {
            // Kiểm tra xem mật khẩu có chứa ít nhất một số không
            return Regex.IsMatch(password, @"\d");
        }

        private bool ContainsUppercaseLetter(string password)
        {
            // Kiểm tra xem mật khẩu có chứa ít nhất một chữ in hoa không
            return Regex.IsMatch(password, @"[A-Z]");
        }
        private bool IsValidEmail(string email)
        {
            // Kiểm tra xem mật khẩu có chứa ít nhất một chữ in hoa không
            return (Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"));

        }

    }
}
