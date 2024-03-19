using PA01_Management_Application.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for LoginPageView.xaml
    /// </summary>
    public partial class LoginPageView : Window
    {
        LoginPageViewModel viewModel = new LoginPageViewModel();
        public LoginPageView()
        {
            InitializeComponent();
            ImageBrush myBrush = new ImageBrush();

            // Tạo một Image và thiết lập nguồn hình ảnh cho nó
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("pack://application:,,,/MVVM/Images/hero-img.jpg"));

            // Thiết lập nguồn hình ảnh cho ImageBrush
            myBrush.ImageSource = image.Source;
            container.Background = myBrush;
            DataContext = viewModel;

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

            }


        }

    }
}
