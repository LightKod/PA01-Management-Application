using PA01_Management_Application.MVVM.Model;
using PA01_Management_Application.MVVM.ViewModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PA01_Management_Application.MVVM.View
{
    /// <summary>
    /// Interaction logic for MovieDetailPageView.xaml
    /// </summary>
    public partial class MovieDetailPageView : UserControl
    {
        List<string> bannerUrls;
        Film film = new Film(
                "Dune 2: Part Two",
                ["Adventure", "Action"],
                1200,
                9.0,
                "https://upload.wikimedia.org/wikipedia/vi/9/94/Dune_2_VN_poster.jpg",
                "Videos/video.mp4",
                [   
                    "/MVVM/Images/1.jpg",
                    "/MVVM/Images/2.jpg",
                    "/MVVM/Images/3.jpg"
                ]
                );
        List<string> mediaUrls;
        int index = 0;
        MovieDetailViewModel viewModel;
        //TODO: parse movie through here!
        public MovieDetailPageView()
        {
            InitializeComponent();
            DataContext = viewModel = new MovieDetailViewModel();
            viewModel.Film = film;
            mediaUrls = new List<string>();
            mediaUrls.Add(film.FilmTrailer);
            mediaUrls.AddRange(film.FilmBanner);
        }

        private void PreviousImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            index--;
            if (index < 1)
                index = mediaUrls.Count - 1;

            UpdateBanner();
        }

        private void NextImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            index++;
            if (index >= mediaUrls.Count)
                index = 0;

            UpdateBanner();
        }

        void UpdateBanner()
        {
            if(index == 0)
            {
                Uri imageUri = new Uri(mediaUrls[index], UriKind.Relative);
                mediaBanner.Source = imageUri;
                mediaBanner.Visibility = System.Windows.Visibility.Visible;
                imageBanner.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                Uri imageUri = new Uri(mediaUrls[index], UriKind.Relative);
                BitmapImage image = new BitmapImage(imageUri);
                imageBanner.Source = image;
                mediaBanner.Visibility = System.Windows.Visibility.Hidden;
                imageBanner.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
