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
        Film film;
        List<string> mediaUrls;
        int index = 0;
        MovieDetailViewModel viewModel;
        //TODO: parse movie through here!
        public MovieDetailPageView()
        {
            InitializeComponent();
            DataContext = viewModel = new MovieDetailViewModel();
            LoadImage();
        }

        async void LoadImage()
        {
            await viewModel.GetMovieDetail();
            film = viewModel.Film;

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
                mediaBanner.Play();
            }
            else
            {
                Uri imageUri = new Uri(mediaUrls[index]);
                BitmapImage image = new BitmapImage(imageUri);
                mediaBanner.Stop();
                imageBanner.Source = image;
                mediaBanner.Visibility = System.Windows.Visibility.Hidden;
                imageBanner.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
