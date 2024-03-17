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
        private int currentImageIndex = 1;
        private const int MaxImageIndex = 8;
        private DispatcherTimer timer;

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
                currentImageIndex = 1;

            UpdateSlideshowImage();
        }

        private void UpdateSlideshowImage()
        {
            string imagePath = $"/MVVM/Images/{currentImageIndex}.jpg"; // Định dạng tên file ảnh
            Uri imageUri = new Uri(imagePath, UriKind.Relative);
            BitmapImage bitmapImage = new BitmapImage(imageUri);
            SlideshowImage.Source = bitmapImage;
        }

        private void PreviousImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            currentImageIndex--;
            if (currentImageIndex < 1)
                currentImageIndex = MaxImageIndex;

            UpdateSlideshowImage();
        }

        private void NextImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            currentImageIndex++;
            if (currentImageIndex > MaxImageIndex)
                currentImageIndex = 1;

            UpdateSlideshowImage();
        }
    }
}
