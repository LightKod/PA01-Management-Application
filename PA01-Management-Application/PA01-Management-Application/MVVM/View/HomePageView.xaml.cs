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
using System.Windows.Threading;

namespace PA01_Management_Application.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomePageView.xaml
    /// </summary>
    public partial class HomePageView : UserControl
    {
        // Chỗ này đã xóa code xử lý slider film nổi bật :">
        private int currentImageIndex = 0;
        private const int MaxImageIndex = 5;
        private DispatcherTimer timer;

        string[] posters = new string[]
      {
    "https://image.tmdb.org/t/p/original/cAOaUTJzUG8vfXcThFNIC5tUMig.jpg",
    "https://image.tmdb.org/t/p/original/2q3B90h2hZ6xJTvna9CIFDNaIr4.jpg",
    "https://image.tmdb.org/t/p/original/d1RHScaZc7I8j0lDke1c4AxI435.jpg",
    "https://image.tmdb.org/t/p/original/7BdxZXbSkUiVeCRXKD3hi9KYeWm.jpg",
    "https://image.tmdb.org/t/p/original/8rpDcsfLJypbO6vREc0547VKqEv.jpg",
    "https://image.tmdb.org/t/p/original/uT5G4fA7jKxlJNfwYPMm353f5AI.jpg",
      };
        int[] id = new int[]
        {
         841,
         9473,
         9502,
         49444,
         76600,
         140300,
        };

        public HomePageView()
        {
            InitializeComponent();

            UpdateSlideshowImage();
            Loaded += HomePageView_Loaded;
        }

        private void HomePageView_Loaded(object sender, RoutedEventArgs e)
        {
            StartSlideshow();
        }

        private void StartSlideshow()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5); // Thay đổi ảnh sau mỗi 5 giây
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            currentImageIndex++;
            if (currentImageIndex > MaxImageIndex)
                currentImageIndex = 0;

            UpdateSlideshowImage();
        }

        private void UpdateSlideshowImage()
        {
            string imagePath = posters[currentImageIndex]; // Định dạng tên file ảnh
            Uri imageUri = new Uri(imagePath);
            BitmapImage bitmapImage = new BitmapImage(imageUri);
            SlideshowImage.Source = bitmapImage;
        }

        private void PreviousImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            currentImageIndex--;
            if (currentImageIndex < 0)
                currentImageIndex = MaxImageIndex;

            UpdateSlideshowImage();
        }

        private void NextImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            currentImageIndex++;
            if (currentImageIndex > MaxImageIndex)
                currentImageIndex = 0;

            UpdateSlideshowImage();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is HomeViewModel viewModel)
            {
                if (viewModel.MovieDetailFromSliderCommand.CanExecute(id[currentImageIndex]))
                {
                    viewModel.MovieDetailFromSliderCommand.Execute(id[currentImageIndex]);
                }
            }
        }
    }
}
