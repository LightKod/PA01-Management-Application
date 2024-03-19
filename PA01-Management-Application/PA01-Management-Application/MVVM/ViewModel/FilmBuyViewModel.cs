using PA01_Management_Application.MVVM.Model;
using System.Collections.ObjectModel;

namespace PA01_Management_Application.MVVM.ViewModel
{
    internal class FilmBuyViewModel
    {
        private ObservableCollection<FilmBuy> _films;

        public ObservableCollection<FilmBuy> Films
        {
            get { return _films; }
            set { _films = value; }
        }

        public FilmBuyViewModel()
        {
            Films = new ObservableCollection<FilmBuy>();

            // Tạo dữ liệu giả cho FilmBuyViewModel
            Films.Add(new FilmBuy("Film 1", new string[] { "Genre 1", "Genre 2" }, 120, 8.0, "https://upload.wikimedia.org/wikipedia/vi/b/bb/Spy_×_Family_Code_White_Movie_Teaser_Visual.png", "Videos/video.mp4", new string[] { "banner1_1.jpg", "banner1_2.jpg" }, new string[] { "Director 1" }, new string[] { "Writer 1" }, new string[] { "Star 1" }));
            Films.Add(new FilmBuy("Film 2", new string[] { "Genre 3", "Genre 4" }, 150, 7.5, "https://upload.wikimedia.org/wikipedia/vi/b/bb/Spy_×_Family_Code_White_Movie_Teaser_Visual.png", "Videos/video.mp4", new string[] { "banner2_1.jpg", "banner2_2.jpg" }, new string[] { "Director 2" }, new string[] { "Writer 2" }, new string[] { "Star 2" }));
            Films.Add(new FilmBuy("Film 3", new string[] { "Genre 5", "Genre 6" }, 180, 7.8, "https://upload.wikimedia.org/wikipedia/vi/b/bb/Spy_×_Family_Code_White_Movie_Teaser_Visual.png", "Videos/video.mp4", new string[] { "banner3_1.jpg", "banner3_2.jpg" }, new string[] { "Director 3" }, new string[] { "Writer 3" }, new string[] { "Star 3" }));
        }
    }
}
