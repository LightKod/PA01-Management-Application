using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using PA01_Management_Application.MVVM.Model;

namespace PA01_Management_Application.MVVM.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public UserViewModel()
        {
            Users = new ObservableCollection<User>();
            LoadData();
        }

        private void LoadData()
        {
            // Add sample users to the collection
            Users.Add(new User
            {
                UserId = 1,
                Username = "user1",
                Password = "password1",
                Avatar = "/Images/avatar1.jpg",
                Fullname = "Tôi là User 1",
                Birthday = new DateTime(1990, 1, 1),
                Gender = 1,
                Email = "user1@example.com",
                City = "City 1",
                Phone = "123456789",
                Point = 100
            });

            Users.Add(new User
            {
                UserId = 2,
                Username = "user2",
                Password = "password2",
                Avatar = "/Images/avatar2.jpg",
                Fullname = "Tôi là User 2",
                Birthday = new DateTime(1995, 5, 5),
                Gender = 2,
                Email = "user2@example.com",
                City = "City 2",
                Phone = "987654321",
                Point = 200
            });
        }

        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public void DeleteUser(User user)
        {
            if (Users.Contains(user))
            {
                Users.Remove(user);
            }
        }

        public void SaveUserChanges(User updatedUser)
        {
            // Find the user in the collection
            var existingUser = Users.FirstOrDefault(u => u.UserId == updatedUser.UserId);
            if (existingUser != null)
            {
                // Update user information
                existingUser.Username = updatedUser.Username;
                existingUser.Password = updatedUser.Password;
                existingUser.Fullname = updatedUser.Fullname;
                existingUser.Birthday = updatedUser.Birthday;
                existingUser.Gender = updatedUser.Gender;
                existingUser.Email = updatedUser.Email;
                existingUser.City = updatedUser.City;
                existingUser.Phone = updatedUser.Phone;
                existingUser.Point = updatedUser.Point;

                OnPropertyChanged(nameof(Users));
            }
            else
            {
                AddUser(updatedUser);
            }

            Console.WriteLine("User changes saved.");
        }

    }
}
