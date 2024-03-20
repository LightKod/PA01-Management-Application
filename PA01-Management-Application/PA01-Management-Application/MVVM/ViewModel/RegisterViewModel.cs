using Microsoft.Win32;
using PA01_Management_Application.Core;
using PA01_Management_Application.MVVM.Models;
using PA01_Management_Application.MVVM.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PA01_Management_Application.MVVM.ViewModel
{
    internal class RegisterViewModel : ObservableObject
    {
        private string _username;
        private string _email;
        private DateTime _birthday;
        private string _password;
        private string _errorEmail;
        private string _errorPassword;


        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public DateTime Birthday
        {
            get => _birthday;
            set
            {
                _birthday = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                Debug.WriteLine(_password);
                OnPropertyChanged();
            }
        }


        public string ErrorEmail
        {
            get => _errorEmail;
            set
            {
                _errorEmail = value;
                OnPropertyChanged(nameof(ErrorEmail));
            }
        }
        public string ErrorPassword
        {
            get => _errorPassword;
            set
            {
                _errorPassword = value;
                OnPropertyChanged(nameof(ErrorPassword));
            }
        }
        public RelayCommand RegisterCommand { get; }

        public RegisterViewModel()
        {
            Username = "your name";
            Email = "your email";
            Birthday = DateTime.Now;
            RegisterCommand = new RelayCommand(ExcutedRegister, (object parameter) => CanRegister());

        }
        private bool CanRegister()
        {
            // Kiểm tra xem có lỗi không
            if (string.IsNullOrEmpty(ErrorEmail) && string.IsNullOrEmpty(ErrorPassword))
            {
                return true;
            }

            return false;
            // Kiểm tra xem email đã tồn tại trong cơ sở dữ liệu hay không
        }

        public bool CheckIfEmailExists(string email)
        {
            // Tạo một đối tượng context để tương tác với cơ sở dữ liệu
            using (var context = new MovieManagementContext())
            {
                // Thực hiện truy vấn để kiểm tra xem có người dùng nào có email này không
                var existingUser = context.Users.FirstOrDefault(u => u.Email == email);

                // Nếu tìm thấy người dùng có email này, trả về true
                // Ngược lại, trả về false
                return existingUser != null;
            }
        }


        private void ExcutedRegister(object parameter)
        {
            // Kiểm tra xem email đã tồn tại chưa
            if (CheckIfEmailExists(_email))
            {
                // Nếu email đã tồn tại, hiển thị thông báo lỗi hoặc thực hiện các hành động khác
                Debug.WriteLine("Email đã tồn tại trong cơ sở dữ liệu.");
                return;
            }

            // Tạo một đối tượng User mới với thông tin được cung cấp
            User newUser = new User();
            newUser.Username = _username;
            newUser.Email = _email;
            newUser.Birthday = new DateOnly(_birthday.Year, _birthday.Month, _birthday.Day);
            newUser.Password = PasswordHasher.HashPassword(_password);

            // Tạo một đối tượng context để tương tác với cơ sở dữ liệu
            using (var context = new MovieManagementContext())
            {
                try
                {
                    // Thêm người dùng mới vào cơ sở dữ liệu
                    context.Users.Add(newUser);
                    context.SaveChanges();

                    // Hiển thị thông báo hoặc thực hiện các hành động khác sau khi thêm thành công
                    Debug.WriteLine("Thêm người dùng mới thành công.");
                    Application.Current.MainWindow.Close();

                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu có
                    Debug.WriteLine("Lỗi khi thêm người dùng mới: " + ex.Message);
                }
            }
        }

    }

}
