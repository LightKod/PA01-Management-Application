using MaterialDesignThemes.Wpf;
using PA01_Management_Application.Core;
using PA01_Management_Application.DataManagers;
using PA01_Management_Application.MVVM.Models;
using PA01_Management_Application.MVVM.View;
using PA01_Management_Application.MVVM.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PA01_Management_Application.MVVM.ViewModel
{
    internal class LoginPageViewModel : ObservableObject
    {
        private string _emailOrPhoneNumber;
        public string EmailOrPhoneNumber
        {
            get { return _emailOrPhoneNumber; }
            set
            {
                _emailOrPhoneNumber = value;
                OnPropertyChanged(nameof(EmailOrPhoneNumber));
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public RelayCommand LoginCommand { get; }
        public RelayCommand RegisterCommand { get; }
        public LoginPageViewModel()
        {
            EmailOrPhoneNumber = "Please enter your email or phone number";
            LoginCommand = new RelayCommand(ExecuteLogin, (object parameter) => CanLogin());
            RegisterCommand = new RelayCommand(ExecuteRegisterCommand);
        }
        private bool CanLogin()
        {
            // Kiểm tra điều kiện để cho phép đăng nhập, ví dụ: không để trống email và mật khẩu
            if (string.IsNullOrWhiteSpace(EmailOrPhoneNumber)) { ErrorMessage = "please fill email"; return false; }
            if (string.IsNullOrWhiteSpace(Password)) { ErrorMessage = "please fill password"; return false; }
            return true;
        }
        private void ExecuteRegisterCommand(object parameter)
        {

            (Application.Current.MainWindow.DataContext as AppWindowViewModel).CurrentView = (Application.Current.MainWindow.DataContext as AppWindowViewModel).RegisterVM;

        }
        private void ExecuteLogin(object parameter)
        {
            // Thực hiện đăng nhập ở đây
            // Đưa logic xử lý đăng nhập vào đây, ví dụ:
            Debug.WriteLine(EmailOrPhoneNumber);
            Debug.WriteLine(Password);

            if (AuthenticateUser(EmailOrPhoneNumber, Password))
            {

                // Đăng nhập thành công
                // Reset các trường
                // lấy user sau khi thành công
                (Application.Current.MainWindow.DataContext as AppWindowViewModel).CurrentView = (Application.Current.MainWindow.DataContext as AppWindowViewModel).AccountVM;



            }
            else
            {
                // Đăng nhập thất bại
                ErrorMessage = "Incorrect email or password";
            }
        }
        private bool AuthenticateUser(string emailOrPhoneNumber, string password)
        {
            using (var context = new MovieManagementContext())
            {
                // Tìm kiếm user trong cơ sở dữ liệu theo email hoặc số điện thoại
                var user = context.Users.FirstOrDefault(u => u.Email == emailOrPhoneNumber || u.Phone == emailOrPhoneNumber);

                if (user != null)
                {
                    PasswordHasher passwordHasher = new PasswordHasher();
                    Debug.WriteLine(user.Password);
                    // Nếu user được tìm thấy, thực hiện kiểm tra mật khẩu
                    if (passwordHasher.VerifyPassword(password, user.Password))
                    {
                        Debug.WriteLine("checkeeeeeeeeeee");
                        UserData.userData = user;
                        if (user.Rules == 1)
                        {
                            (Application.Current.MainWindow.DataContext as AppWindowViewModel).IsAdmin = true;
                        }
                        else
                        {
                            (Application.Current.MainWindow.DataContext as AppWindowViewModel).IsAdmin = false;
                        }
                        // Mật khẩu hợp lệ
                        return true;
                    }
                    ErrorMessage = "password wrong";
                    return false;

                }
                Debug.WriteLine("laalaaa");
                ErrorMessage = "user not found";
                // Nếu không tìm thấy user hoặc mật khẩu không hợp lệ, trả về false
                return false;
            }
        }


    }
}
