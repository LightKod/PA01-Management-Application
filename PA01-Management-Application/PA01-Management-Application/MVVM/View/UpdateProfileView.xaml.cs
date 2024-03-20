using PA01_Management_Application.MVVM.Model;
using PA01_Management_Application.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace PA01_Management_Application.MVVM.View
{
    /// <summary>
    /// Interaction logic for UpdateProfileView.xaml
    /// </summary>
    public partial class UpdateProfileView : UserControl
    {
        private UserViewModel _viewModel;

        public UpdateProfileView(UserViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;

            SetProfile();

            txtEmail.IsEnabled = false;
            txtUsername.IsEnabled = false;
        }

        private void SetProfile()
        {
            if (_viewModel.Users != null && _viewModel.Users.Count > 0)
            {
                txtFullname.Text = _viewModel.Users[0].Fullname;
                txtEmail.Text = _viewModel.Users[0].Email;
                txtPhone.Text = _viewModel.Users[0].Phone;
                txtUsername.Text = _viewModel.Users[0].Username;
                txtCity.Text = _viewModel.Users[0].City;
                txtPassword.Password = _viewModel.Users[0].Password;
                if (_viewModel.Users[0].Birthday != null)
                {
                    dpBirthday.SelectedDate = _viewModel.Users[0].Birthday;
                }

                if (_viewModel.Users[0].Gender == 1)
                {
                    rbMale.IsChecked = true;
                }
                else
                {
                    rbFemale.IsChecked = true;
                }

            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            User updatedUser = new User
            {
                UserId = _viewModel.Users[0].UserId,
                Username = txtUsername.Text,
                Password = txtPassword.Password,
                Fullname = txtFullname.Text,
                Birthday = dpBirthday.SelectedDate.Value,
                Gender = rbMale.IsChecked == true ? 1 : 2,
                Email = txtEmail.Text,
                City = txtCity.Text,
                Phone = txtPhone.Text,
                Point = _viewModel.Users[0].Point
            };
            _viewModel.SaveUserChanges(updatedUser);

            var mainWindowViewModel = App.Current.MainWindow.DataContext as AppWindowViewModel;
            mainWindowViewModel.CurrentView = new AccountPageView();
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
