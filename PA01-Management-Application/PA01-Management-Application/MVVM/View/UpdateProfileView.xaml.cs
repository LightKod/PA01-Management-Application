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

        private bool IsPasswordConfirmed()
        {
            return txtPassword.Password == txtConfirmPassword.Password;
        }

        private bool IsPasswordOld()
        {
            return txtOldPassword.Password == _viewModel.Users[0].Password;
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra mật khẩu đã được xác nhận chưa
            if (!IsPasswordConfirmed())
            {
                MessageBox.Show("Confirm Password không khớp với Password.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Kiểm tra mật khẩu cũ
            if (!IsPasswordOld())
            {
                MessageBox.Show("Password cũ không đúng", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Tạo đối tượng User mới từ các thông tin đã nhập
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
                Point = _viewModel.Users[0].Point // Giữ nguyên số điểm, không thay đổi từ giao diện
            };

            // Lưu các thay đổi
            _viewModel.SaveUserChanges(updatedUser);

            // Chuyển sang trang AccountPageView để xem các thay đổi
            var mainWindowViewModel = App.Current.MainWindow.DataContext as AppWindowViewModel;
            mainWindowViewModel.CurrentView = new AccountPageView();
        }

    }
}
