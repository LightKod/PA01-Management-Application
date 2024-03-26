using PA01_Management_Application.Core;
using PA01_Management_Application.DataManagers;
using PA01_Management_Application.MVVM.Models;
using PA01_Management_Application.MVVM.ViewModel.Service;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace PA01_Management_Application.MVVM.ViewModel
{
    public class AccountViewModel : BaseViewModel
    {
        
        private int _userId;
        public int UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }

        private string _fullname;
        public string Fullname
        {
            get { return _fullname; }
            set
            {
                _fullname = value;
                OnPropertyChanged(nameof(Fullname));
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
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

        private string _avatar;
        public string Avatar
        {
            get { return _avatar; }
            set
            {
                _avatar = value;
                OnPropertyChanged(nameof(Avatar));
            }
        }

        private DateTime _birthday;
        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                if (_birthday != value)
                {

                    _birthday = value;
                    OnPropertyChanged(nameof(Birthday));
                }
            }
        }

        private int? _gender;
        public int? Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        private int? _rules;
        public int? Rules
        {
            get { return _rules; }
            set
            {
                _rules = value;
                OnPropertyChanged(nameof(Rules));
            }
        }

        private int? _point;
        public int? Point
        {
            get { return _point; }
            set
            {
                _point = value;
                OnPropertyChanged(nameof(Point));
            }
        }

        private int _bookingTicketCount;
        public int BookingTicketCount
        {
            get { return _bookingTicketCount; }
            set
            {
                _bookingTicketCount = value;
                OnPropertyChanged(nameof(BookingTicketCount));
            }
        }


        public void UpdateDataFromUserData()
        {
            UserId = UserData.userData.UserId;
            Fullname = UserData.userData.Fullname;
            Email = UserData.userData.Email;
            Phone = UserData.userData.Phone;
            Username = UserData.userData.Username;
            Password = UserData.userData.Password;

            DateOnly dateOnly = UserData.userData.Birthday.Value;
            Birthday = new DateTime(dateOnly.Year, dateOnly.Month, dateOnly.Day);

            Gender = UserData.userData.Gender;
            City = UserData.userData.City;
            Rules = UserData.userData.Rules;
            Point = UserData.userData.Point;

            using (var context = new MovieManagementContext())
            {
                BookingTicketCount = context.Bookings.Count(x => x.UserId == UserData.userData.UserId && x.Price > 0);
            }
        }

        public RelayCommand LogOutCommand { get; set; }

        public AccountViewModel()
        {
            LogOutCommand = new RelayCommand(o =>
            {
                UserData.userData = null;
                (Application.Current.MainWindow.DataContext as AppWindowViewModel).CurrentView = (Application.Current.MainWindow.DataContext as AppWindowViewModel).HomeVM;
                (Application.Current.MainWindow.DataContext as AppWindowViewModel).IsAdmin = false;
            });
        }


        public bool SaveChanges()
        {
            try
            {
                using (var context = new MovieManagementContext())
                {
                    var user = context.Users.FirstOrDefault(u => u.Username == Username);

                    if (user != null)
                    {
                        user.Fullname = Fullname;
                        user.Email = Email;
                        user.Phone = Phone;
                        user.Password = Password;
                        user.Avatar = Avatar;
                        user.Birthday = DateOnly.FromDateTime(Birthday);
                        user.Gender = Gender;
                        user.City = City;
                        user.Rules = Rules;
                        user.Point = Point;

                        context.SaveChanges();
                        UserData.userData = user;

                        return true;

                    }
                    else
                    {
                        Console.WriteLine("User not found!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating profile: {ex.Message}");
            }

            return false;
        }

        public bool ChangePassword(string oldPassword, string newPassword)
        {
            try
            {
                using (var context = new MovieManagementContext())
                {
                    var user = context.Users.FirstOrDefault(u => u.Username == Username);

                    if (user != null)
                    {
                        PasswordHasher passwordHasher = new PasswordHasher();
                        Debug.WriteLine(user.Password);
                        if (passwordHasher.VerifyPassword(oldPassword, user.Password))
                        {

                            user.Password = PasswordHasher.HashPassword(newPassword);

                            context.SaveChanges();

                            UserData.userData.Password = PasswordHasher.HashPassword(newPassword);

                            return true;

                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error changing password: {ex.Message}");
                return false;
            }
            return false;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

